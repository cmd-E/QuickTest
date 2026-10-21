using System;
using System.Collections.Generic;
using System.Linq;
using QuickTest.Classes;
using Xunit;

namespace QuickTest.Tests
{
    public class QuestionParserTests
    {
        internal static string[] Paragraphs(string text = "Question")
        {
            return new[] { "<q>" + text, "<a>Correct", "<a>B", "<a>C", "<a>D", "<a>E" };
        }

        internal static Question Question(string text = "Question")
        {
            return new Question(text, new[] { "Correct", "B", "C", "D", "E" });
        }

        [Theory]
        [InlineData(null, "<a>")]
        [InlineData("", "<a>")]
        [InlineData(" ", "<a>")]
        [InlineData("<q>", null)]
        [InlineData("<q>", "")]
        [InlineData("<q>", " ")]
        [InlineData("<q>", "<Q>")]
        [InlineData("q", "question")]
        [InlineData("question", "q")]
        [InlineData(" <Q> ", "<q>more")]
        public void RejectsMissingAndOverlappingTags(string questionTag, string variantTag)
        {
            Assert.Throws<ArgumentException>(() => TaggedQuestionParser.ValidateTags(questionTag, variantTag));
        }

        [Fact]
        public void ParsesMultipleQuestionsAndPreservesCorrectAnswerIdentity()
        {
            var input = Paragraphs("First").Concat(Paragraphs("Second")).ToArray();
            var before = input.ToArray();

            var questions = TaggedQuestionParser.Parse(input, "<q>", "<a>");

            Assert.Equal(new[] { "First", "Second" }, questions.Select(q => q.QuestionText));
            Assert.All(questions, q => Assert.Equal("Correct", q.CorrectVariant));
            Assert.Equal(before, input);
            Assert.NotSame(questions[0].Variants, questions[1].Variants);
        }

        [Fact]
        public void TrimsTagsParagraphsAndTextAndIgnoresBlankAndUntaggedParagraphs()
        {
            var input = new[] { null, "", " ", "Title", " <Q>  Question  ", "<A> Correct ",
                "ignored note", "<a>B", "<a>C", "<a>D", "<a>E" };

            var question = Assert.Single(TaggedQuestionParser.Parse(input, " <q> ", " <a> "));

            Assert.Equal("Question", question.QuestionText);
            Assert.Equal("Correct", question.CorrectVariant);
        }

        public static IEnumerable<object[]> MalformedParagraphs()
        {
            yield return new object[] { new string[0] };
            yield return new object[] { new[] { "untagged", "", null } };
            yield return new object[] { new[] { "<a>orphan" } };
            yield return new object[] { new[] { "<q>" } };
            yield return new object[] { new[] { "<q>Q", "<a> " } };
            yield return new object[] { new[] { "<q>Q", "<a>A" } };
            yield return new object[] { new[] { "<q>Q", "<a>A", "<q>Next" } };
            yield return new object[] { Paragraphs().Concat(new[] { "<a>extra" }).ToArray() };
            yield return new object[] { Paragraphs().Concat(new[] { "<q>incomplete" }).ToArray() };
        }

        [Theory]
        [MemberData(nameof(MalformedParagraphs))]
        public void RejectsMalformedQuestionBlocks(string[] paragraphs)
        {
            Assert.Throws<FormatException>(() => TaggedQuestionParser.Parse(paragraphs, "<q>", "<a>"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void QuestionRejectsEmptyText(string text)
        {
            Assert.Throws<FormatException>(() => new Question(text, new[] { "A", "B", "C", "D", "E" }));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        [InlineData(6)]
        public void QuestionRequiresExactlyFiveVariants(int count)
        {
            Assert.Throws<FormatException>(() => new Question("Q", Enumerable.Repeat("A", count)));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void QuestionRejectsEmptyVariants(string variant)
        {
            Assert.Throws<FormatException>(() => new Question("Q", new[] { "A", "B", variant, "D", "E" }));
        }

        [Fact]
        public void QuestionCopiesInputAndExposesReadOnlyVariants()
        {
            var variants = new[] { "A", "B", "C", "D", "E" };
            var question = new Question("Q", variants);
            variants[0] = "changed";

            Assert.Equal("A", question.CorrectVariant);
            Assert.Throws<NotSupportedException>(() => ((IList<string>)question.Variants)[0] = "changed");
        }
    }
}
