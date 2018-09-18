using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DebugTime.Domain.Model
{
    public class QuizQuestion
    {
        public QuizQuestion()
        {
            QuestionOptions = new HashSet<QuestionOption>();
        }

        public string QuizId { get; set; }

        public string Key
        {
            get
            {
                var hash = SHA256.Create().ComputeHash(Encoding.Default.GetBytes(Title));
                return Convert.ToBase64String(hash);
            }
            set { Title = value; }
        }

        public string Title { get; set; }

        public string Answer { get; set; }

        public int Order { get; set; }

        public Quiz Quiz { get; set; }
        public ICollection<QuestionOption> QuestionOptions { get; set; }

    }
}