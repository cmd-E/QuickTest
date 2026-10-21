using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using QuickTest.Classes;
using Syncfusion.DocIO.DLS;
class DocumentChecks
{
    static void Check(bool condition, string message)
    {
        if (!condition)
            throw new Exception(message);
    }
    static List<Question> Parse(string path)
    {
        return (List<Question>)typeof(Question)
            .Assembly.GetType("QuickTest.Classes.DocumentParser")
            .GetMethod("ParseTest")
            .Invoke(null, new object[] { path, "<QUESTION>", "<VARIANT>" });
    }
    static void Write(string path, bool tagged)
    {
        using (var document = new WordDocument())
        {
            document.EnsureMinimal();
            foreach (string value in new[] { "Q", "A", "B", "C", "D", "E" })
            {
                bool question = value == "Q";
                document.LastSection.AddParagraph().AppendText((tagged ? (question ? "<question>" : "<variant>") : "") +
                                                               value);
            }
            document.Save(path);
        }
    }
    static void Main()
    {
        string directory = Path.Combine(Path.GetTempPath(), "quicktest-document-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        try
        {
            foreach (string extension in new[] { ".doc", ".docx" })
            {
                string path = Path.Combine(directory, "fixture" + extension);
                Write(path, true);
                var questions = Parse(path);
                Check(questions.Count == 1 && questions[0].CorrectVariant == "A", "Word adapter parse " + extension);
                using (var exclusive = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                }
                string plain = Path.Combine(directory, "plain" + extension);
                Write(plain, false);
                string converted = (string) typeof(Question)
                                       .Assembly.GetType("QuickTest.Classes.DocumentParser")
                                       .GetMethod("CreateTest")
                                       .Invoke(null, new object[] { plain, "<question>", "<variant>" });
                Check(Parse(converted).Count == 1, "conversion roundtrip");
                byte[] before = File.ReadAllBytes(converted);
                bool failed = false;
                using (var locked = File.Open(converted, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) {
                    try {
                        typeof(Question).Assembly.GetType("QuickTest.Classes.DocumentParser").GetMethod("CreateTest").Invoke(null, new object[] { plain, "<question>", "<variant>" });
                    } catch (TargetInvocationException) { failed = true; }
                }
                Check(failed && before.SequenceEqual(File.ReadAllBytes(converted)), "failed conversion replaced existing output");
                Check(Directory.GetFiles(directory, ".*" + extension).Length == 0, "conversion temporary file leaked");
            }
            string corrupt = Path.Combine(directory, "corrupt.docx");
            File.WriteAllText(corrupt, "not a Word document");
            bool rejected = false;
            try { Parse(corrupt); } catch (TargetInvocationException) { rejected = true; }
            Check(rejected, "corrupt document accepted");
            Console.WriteLine("QuickTest actual .doc/.docx parsing, disposal and conversion checks passed");
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }
}
