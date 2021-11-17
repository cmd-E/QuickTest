using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickTest.Classes
{
    internal static class DocumentParser
    {
        /// <summary>
        /// Collects all questions and it's variants to list of type questions
        /// </summary>
        /// <param name="pathToDoc">Path to document with test</param>
        /// <param name="questionTag">Prefix of the string that marks question</param>
        /// <param name="variantTag">Prefix of the string that marks variant</param>
        /// <returns></returns>
        public static List<Question> ParseTest(string pathToDoc, string questionTag, string variantTag)
        {
            questionTag = questionTag.Trim();
            variantTag = variantTag.Trim();
            List<Question> questions = new List<Question>();
            Question localQuestion = new Question
            {
                Variants = new List<string>()
            };
            WordDocument document;
            try
            {
                document = new WordDocument(pathToDoc);
            }
            catch(IOException e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return questions;
            }
            foreach (WSection section in document.Sections)
            {
                foreach (WParagraph paragraph in section.Body.Paragraphs)
                {
                    var p = paragraph.Text;
                    p = p.Trim();
                    if (p.ToLower().StartsWith(questionTag))
                    {
                        localQuestion = new Question
                        {
                            QuestionText = p.Remove(0, questionTag.Length).Trim(),
                            Variants = new List<string>()
                        };
                    }
                    else if (p.Trim().ToLower().StartsWith(variantTag))
                    {
                        if (string.IsNullOrEmpty(localQuestion.CorrectVariant))
                        {
                            localQuestion.CorrectVariant = p.Remove(0, variantTag.Length).Trim();
                        }
                        localQuestion.Variants.Add(p.Remove(0, variantTag.Length).Trim());
                    }
                    if (localQuestion.Variants.Count == 5)
                    {
                        questions.Add(localQuestion);
                        localQuestion = new Question
                        {
                            Variants = new List<string>()
                        };
                    }
                }
            }
            return questions;
        }
    }
}
