using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace QuickTest.Classes
{
    public sealed class Question
    {
        public string QuestionText { get; private set; }
        public ReadOnlyCollection<string> Variants { get; private set; }
        public string CorrectVariant
        {
            get {
                return Variants[0];
            }
        }
        public Question(string text, IEnumerable<string> variants)
        {
            var values = variants.ToList();
            if (string.IsNullOrWhiteSpace(text) || values.Count != 5 || values.Any(string.IsNullOrWhiteSpace))
                throw new FormatException("Каждый вопрос должен содержать текст и пять непустых вариантов");
            QuestionText = text;
            Variants = values.AsReadOnly();
        }
    }
}
