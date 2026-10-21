using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickTest.Classes
{
    public sealed class SessionQuestion
    {
        public Question Question { get; private set; }
        public IList<int> Order { get; private set; }
        public int? Selected { get; set; }
        internal SessionQuestion(Question question, Random random)
        {
            Question = question;
            var order = Enumerable.Range(0, 5).ToArray();
            TestSession.Shuffle(order, random);
            Order = Array.AsReadOnly(order);
        }
    }
    public sealed class ResultRow
    {
        public int Number { get; set; }
        public string Question, Selected, Correct;
        public bool IsCorrect;
    }
    public sealed class TestSession
    {
        public IList<SessionQuestion> Questions { get; private set; }
        public bool Complete
        {
            get {
                return Questions.All(q => q.Selected.HasValue);
            }
        }
        public TestSession(IList<Question> bank, int count, Random random)
        {
            if (count < 1 || count > bank.Count)
                throw new ArgumentOutOfRangeException("count");
            int[] indices = Enumerable.Range(0, bank.Count).ToArray();
            Shuffle(indices, random);
            Questions = indices.Take(count).Select(i => new SessionQuestion(bank[i], random)).ToList().AsReadOnly();
        }
        internal static void Shuffle<T>(T[] values, Random random)
        {
            for (int i = values.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                T value = values[i];
                values[i] = values[j];
                values[j] = value;
            }
        }
        public IList<ResultRow> Results()
        {
            return Questions
                .Select((q, i) =>
                            new ResultRow { Number = i + 1, Question = q.Question.QuestionText,
                                            Selected = q.Selected.HasValue ? q.Question.Variants[q.Selected.Value] : "",
                                            Correct = q.Question.CorrectVariant, IsCorrect = q.Selected == 0 })
                .ToList()
                .AsReadOnly();
        }
    }
}
