using System;
using System.IO;
using System.Linq;
using QuickTest.Classes;
using Syncfusion.DocIO.DLS;
using Xunit;

namespace QuickTest.Tests
{
    [Trait("Category", "Integration")]
    public class DocumentTests : IDisposable
    {
        private readonly string root = Path.Combine(Path.GetTempPath(), "QuickTest.Tests-" + Guid.NewGuid().ToString("N"));

        public DocumentTests() { Directory.CreateDirectory(root); }

        private string Write(string name, string[] paragraphs)
        {
            string path = Path.Combine(root, name);
            using (var document = new WordDocument())
            {
                document.EnsureMinimal();
                foreach (string text in paragraphs) document.LastSection.AddParagraph().AppendText(text);
                document.Save(path);
            }
            return path;
        }

        [Theory]
        [InlineData(".doc")]
        [InlineData(".docx")]
        public void ParsesActualWordDocumentsAndReleasesTheFile(string extension)
        {
            string path = Write("tagged" + extension, QuestionParserTests.Paragraphs("Вопрос"));

            var question = Assert.Single(DocumentParser.ParseTest(path, "<q>", "<a>"));

            Assert.Equal("Вопрос", question.QuestionText);
            Assert.Equal("Correct", question.CorrectVariant);
            using (File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) { }
        }

        [Theory]
        [InlineData(".doc")]
        [InlineData(".docx")]
        public void ConversionRoundTripsWithoutChangingTheSource(string extension)
        {
            string path = Write("plain" + extension, new[] { "Q", "A", "B", "C", "D", "E", "", "Q2", "A2", "B2", "C2", "D2", "E2" });
            byte[] original = File.ReadAllBytes(path);

            string converted = DocumentParser.CreateTest(path, " <q> ", " <a> ");

            Assert.Equal(Path.Combine(root, "plain-modified" + extension), converted);
            Assert.Equal(original, File.ReadAllBytes(path));
            var questions = DocumentParser.ParseTest(converted, "<q>", "<a>");
            Assert.Equal(new[] { "Q", "Q2" }, questions.Select(q => q.QuestionText));
            Assert.Equal(new[] { "A", "A2" }, questions.Select(q => q.CorrectVariant));
            Assert.Empty(Directory.GetFiles(root, ".*" + extension));
            using (File.Open(converted, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) { }
        }

        [Theory]
        [InlineData(".doc")]
        [InlineData(".docx")]
        public void SuccessfulConversionReplacesExistingOutput(string extension)
        {
            string path = Write("plain" + extension, new[] { "First", "A", "B", "C", "D", "E" });
            string output = DocumentParser.CreateTest(path, "<q>", "<a>");
            Write("plain" + extension, new[] { "Replacement", "A", "B", "C", "D", "E" });

            Assert.Equal(output, DocumentParser.CreateTest(path, "<q>", "<a>"));

            Assert.Equal("Replacement", Assert.Single(DocumentParser.ParseTest(output, "<q>", "<a>")).QuestionText);
            Assert.Empty(Directory.GetFiles(root, ".*" + extension));
        }

        [Theory]
        [InlineData(".doc")]
        [InlineData(".docx")]
        public void LockedOutputIsPreservedAndStagingFilesAreRemoved(string extension)
        {
            string path = Write("plain" + extension, new[] { "Q", "A", "B", "C", "D", "E" });
            string output = DocumentParser.CreateTest(path, "<q>", "<a>");
            byte[] before = File.ReadAllBytes(output);
            using (File.Open(output, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                Assert.ThrowsAny<IOException>(() => DocumentParser.CreateTest(path, "<q>", "<a>"));

            Assert.Equal(before, File.ReadAllBytes(output));
            Assert.Empty(Directory.GetFiles(root, ".*" + extension));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(7)]
        public void InvalidConversionPreservesExistingOutput(int paragraphCount)
        {
            string path = Write("invalid.docx", Enumerable.Range(0, paragraphCount).Select(i => "Line " + i).ToArray());
            string output = Path.Combine(root, "invalid-modified.docx");
            File.WriteAllText(output, "existing output");

            Assert.Throws<FormatException>(() => DocumentParser.CreateTest(path, "<q>", "<a>"));

            Assert.Equal("existing output", File.ReadAllText(output));
            Assert.Empty(Directory.GetFiles(root, ".*.docx"));
        }

        [Theory]
        [InlineData("notes.txt")]
        [InlineData("notes.pdf")]
        [InlineData("notes.docx.bak")]
        public void UnsupportedExtensionsAreRejectedBeforeOpening(string name)
        {
            string path = Path.Combine(root, name);

            Assert.Throws<ArgumentException>(() => DocumentParser.ParseTest(path, "<q>", "<a>"));
            Assert.Throws<ArgumentException>(() => DocumentParser.CreateTest(path, "<q>", "<a>"));
        }

        [Fact]
        public void InvalidTagsAreRejectedBeforeOpeningAFile()
        {
            string path = Path.Combine(root, "does-not-exist.docx");

            Assert.Throws<ArgumentException>(() => DocumentParser.ParseTest(path, "q", "question"));
            Assert.Throws<ArgumentException>(() => DocumentParser.CreateTest(path, "q", "question"));
        }

        [Fact]
        public void InvalidTaggedDocumentIsRejectedAndReleased()
        {
            string path = Write("invalid.docx", new[] { "<q>Incomplete", "<a>A" });

            Assert.Throws<FormatException>(() => DocumentParser.ParseTest(path, "<q>", "<a>"));

            using (File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) { }
        }

        [Fact]
        public void CorruptDocumentCannotProduceQuestionsOrReplaceExistingOutput()
        {
            string path = Path.Combine(root, "corrupt.docx");
            File.WriteAllText(path, "not a Word document");
            string output = Path.Combine(root, "corrupt-modified.docx");
            File.WriteAllText(output, "existing");

            Assert.ThrowsAny<Exception>(() => DocumentParser.ParseTest(path, "<q>", "<a>"));
            Assert.ThrowsAny<Exception>(() => DocumentParser.CreateTest(path, "<q>", "<a>"));

            Assert.Equal("existing", File.ReadAllText(output));
            Assert.Empty(Directory.GetFiles(root, ".*.docx"));
        }

        public void Dispose() { Directory.Delete(root, true); }
    }
}
