using System;
using System.Collections.Generic;
using System.Linq;
using QuickTest.Classes;
using Xunit;

namespace QuickTest.Tests
{
    public class SessionTests
    {
        private static List<Question> Bank(int count = 10)
        {
            return Enumerable.Range(1, count).Select(i => QuestionParserTests.Question("Question " + i)).ToList();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(11)]
        public void RejectsInvalidQuestionCounts(int count)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TestSession(Bank(), count, new Random(17)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void SamplesWithoutRepeatingQuestionsOrVariants(int count)
        {
            var bank = Bank();

            var session = new TestSession(bank, count, new Random(17));

            Assert.Equal(count, session.Questions.Count);
            Assert.Equal(count, session.Questions.Select(q => q.Question).Distinct().Count());
            Assert.All(session.Questions, q => Assert.Equal(new[] { 0, 1, 2, 3, 4 }, q.Order.OrderBy(i => i)));
            Assert.All(session.Questions, q => Assert.Contains(q.Question, bank));
            Assert.False(session.Complete);
        }

        [Fact]
        public void SeededSessionsAreRepeatableAndDoNotMutateTheQuestionBank()
        {
            var bank = Bank();
            var before = bank.ToArray();
            var first = new TestSession(bank, 5, new Random(17));
            var second = new TestSession(bank, 5, new Random(17));

            Assert.Equal(before, bank);
            Assert.Equal(first.Questions.Select(q => q.Question), second.Questions.Select(q => q.Question));
            for (int i = 0; i < first.Questions.Count; i++)
                Assert.Equal(first.Questions[i].Order, second.Questions[i].Order);
        }

        [Fact]
        public void SessionsHaveIndependentSelectionsAndReadOnlyCollections()
        {
            var bank = Bank(1);
            var first = new TestSession(bank, 1, new Random(17));
            var order = first.Questions[0].Order.ToArray();
            first.Questions[0].Selected = 3;
            var second = new TestSession(bank, 1, new Random(20));

            Assert.Null(second.Questions[0].Selected);
            Assert.Equal(3, first.Questions[0].Selected);
            Assert.Equal(order, first.Questions[0].Order);
            Assert.Throws<NotSupportedException>(() => first.Questions.Add(first.Questions[0]));
            Assert.Throws<NotSupportedException>(() => first.Questions[0].Order[0] = 4);
        }

        [Theory]
        [InlineData(null, false, "")]
        [InlineData(0, true, "Correct")]
        [InlineData(1, false, "B")]
        [InlineData(2, false, "C")]
        [InlineData(3, false, "D")]
        [InlineData(4, false, "E")]
        public void ScoresOriginalAnswerIdentityRegardlessOfDisplayOrder(int? selected, bool correct, string text)
        {
            var session = new TestSession(Bank(1), 1, new Random(17));
            session.Questions[0].Selected = selected;

            var row = Assert.Single(session.Results());

            Assert.Equal(correct, row.IsCorrect);
            Assert.Equal(text, row.Selected);
            Assert.Equal("Correct", row.Correct);
            Assert.Equal("Question 1", row.Question);
            Assert.Equal(1, row.Number);
            Assert.Equal(selected.HasValue, session.Complete);
        }

        [Fact]
        public void DuplicateAnswerTextDoesNotTurnAWrongAnswerIntoACorrectOne()
        {
            var bank = new[] { new Question("Q", new[] { "same", "same", "C", "D", "E" }) };
            var session = new TestSession(bank, 1, new Random(17));
            session.Questions[0].Selected = 1;

            Assert.False(Assert.Single(session.Results()).IsCorrect);
        }

        [Fact]
        public void ResultsAreIndependentSnapshotsWithSequentialNumbers()
        {
            var session = new TestSession(Bank(), 3, new Random(17));
            var before = session.Results();
            foreach (var question in session.Questions) question.Selected = 0;

            var after = session.Results();

            Assert.True(session.Complete);
            Assert.All(before, row => Assert.False(row.IsCorrect));
            Assert.All(after, row => Assert.True(row.IsCorrect));
            Assert.Equal(new[] { 1, 2, 3 }, after.Select(r => r.Number));
            Assert.Throws<NotSupportedException>(() => after.Add(after[0]));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        public void ShufflePreservesTheInputMultiset(int count)
        {
            var values = Enumerable.Range(0, count).ToArray();

            TestSession.Shuffle(values, new Random(17));

            Assert.Equal(Enumerable.Range(0, count), values.OrderBy(i => i));
        }
    }
}
