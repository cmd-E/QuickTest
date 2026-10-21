using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuickTest.Classes
{
    internal static class DocumentParser
    {
        private static void ValidatePath(string path)
        {
            string extension = Path.GetExtension(path);
            if (!string.Equals(extension, ".doc", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(extension, ".docx", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Выберите файл .doc или .docx");
        }
        public static List<Question> ParseTest(string path, string questionTag, string variantTag)
        {
            ValidatePath(path);
            TaggedQuestionParser.ValidateTags(questionTag, variantTag);
            using (var document = new WordDocument(path))
            {
                var paragraphs = new List<string>();
                foreach (WSection section in document.Sections)
                    foreach (WParagraph paragraph in section.Body.Paragraphs)
                        paragraphs.Add(paragraph.Text);
                return TaggedQuestionParser.Parse(paragraphs, questionTag, variantTag);
            }
        }
        public static string CreateTest(string path, string questionTag, string variantTag)
        {
            ValidatePath(path);
            TaggedQuestionParser.ValidateTags(questionTag, variantTag);
            questionTag = questionTag.Trim();
            variantTag = variantTag.Trim();
            string target = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) +
                                                                          "-modified" + Path.GetExtension(path));
            string temporary = Path.Combine(Path.GetDirectoryName(target),
                                            "." + Guid.NewGuid().ToString("N") + Path.GetExtension(path));
            try
            {
                using (var document = new WordDocument(path))
                {
                    int index = 0;
                    foreach (WSection section in document.Sections)
                        foreach (WParagraph paragraph in section.Body.Paragraphs)
                        {
                            if (string.IsNullOrWhiteSpace(paragraph.Text))
                                continue;
                            paragraph.Text = (index % 6 == 0 ? questionTag : variantTag) + paragraph.Text;
                            index++;
                        }
                    if (index == 0 || index % 6 != 0)
                        throw new FormatException("Документ должен содержать блоки: вопрос и пять вариантов");
                    document.Save(temporary);
                }
                if (File.Exists(target))
                    File.Replace(temporary, target, null);
                else
                    File.Move(temporary, target);
                return target;
            }
            finally
            {
                if (File.Exists(temporary))
                    File.Delete(temporary);
            }
        }
    }
}
