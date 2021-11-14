using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest
{
    public class Question
    {
        public string QuestionText { get; set; }
        public List<string> Variants { get; set; }
        public string CorrectVariant { get; set; }
    }
}
