using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickTest
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
                    var par = LeaveTextOnly(paragraph);
                    var sb = new StringBuilder();
                    foreach (var ce in par.ChildEntities)
                    {
                        var txtRange = ce as WTextRange;
                        sb.Append(txtRange.Text);
                    }
                    var p = sb.ToString();
                    p = p.Trim();
                    if (p.ToLower().StartsWith(questionTag))
                    {
                        localQuestion = new Question
                        {
                            QuestionText = p,
                            Variants = new List<string>()
                        };
                    }
                    else if (p.Trim().ToLower().StartsWith(variantTag))
                    {
                        if (string.IsNullOrEmpty(localQuestion.CorrectVariant))
                        {
                            localQuestion.CorrectVariant = p;
                        }
                        localQuestion.Variants.Add(p);
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

        private static WParagraph LeaveTextOnly(WParagraph par)
        {
            for (int i = 0; i < par.ChildEntities.Count; i++)
            {
                if (!(par.ChildEntities[i] is WTextRange))
                {
                    par.ChildEntities.RemoveAt(i--);
                }
            }
            return par;
        }
    }
}
