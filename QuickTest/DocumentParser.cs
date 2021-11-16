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
        public static List<Question> ParseTest(string pathToDoc)
        {
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
                    if (p.ToLower().StartsWith("<question>"))
                    {
                        localQuestion = new Question
                        {
                            QuestionText = p,
                            Variants = new List<string>()
                        };
                    }
                    else if (p.Trim().ToLower().StartsWith("<variant>"))
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
