using System;
using System.Collections.Generic;

namespace QuickTest.Classes
{
    public static class TaggedQuestionParser
    {
        public static void ValidateTags(string questionTag, string variantTag)
        {
            if (string.IsNullOrWhiteSpace(questionTag) || string.IsNullOrWhiteSpace(variantTag) ||
                questionTag.Trim().StartsWith(variantTag.Trim(), StringComparison.OrdinalIgnoreCase) ||
                variantTag.Trim().StartsWith(questionTag.Trim(), StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Теги должны быть непустыми и не совпадать или перекрывать друг друга");
        }
        public static List<Question> Parse(IEnumerable<string> paragraphs, string questionTag, string variantTag)
        {
            ValidateTags(questionTag, variantTag);
            questionTag = questionTag.Trim();
            variantTag = variantTag.Trim();
            var questions = new List<Question>();
            string text = null;
            var variants = new List<string>();
            int line = 0;
            foreach (var paragraph in paragraphs)
            {
                line++;
                string value = (paragraph ?? "").Trim();
                if (value.Length == 0)
                    continue;
                if (value.StartsWith(questionTag, StringComparison.OrdinalIgnoreCase))
                {
                    if (text != null)
                    {
                        if (variants.Count != 5)
                            throw new FormatException("Неполный вопрос перед абзацем " + line);
                        questions.Add(new Question(text, variants));
                    }
                    text = value.Substring(questionTag.Length).Trim();
                    variants = new List<string>();
                    if (text.Length == 0)
                        throw new FormatException("Пустой вопрос в абзаце " + line);
                }
                else if (value.StartsWith(variantTag, StringComparison.OrdinalIgnoreCase))
                {
                    if (text == null || variants.Count == 5)
                        throw new FormatException("Лишний вариант в абзаце " + line);
                    value = value.Substring(variantTag.Length).Trim();
                    if (value.Length == 0)
                        throw new FormatException("Пустой вариант в абзаце " + line);
                    variants.Add(value);
                }
            }
            if (text != null)
            {
                if (variants.Count != 5)
                    throw new FormatException("Последний вопрос не содержит пять вариантов");
                questions.Add(new Question(text, variants));
            }
            if (questions.Count == 0)
                throw new FormatException("Вопросы не найдены");
            return questions;
        }
    }
}
