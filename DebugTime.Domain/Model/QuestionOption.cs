using System;
using System.Security.Cryptography;
using System.Text;

namespace DebugTime.Domain.Model
{
    public class QuestionOption
    {
        public string Key
        {
            get
            {
                var hash=SHA256.Create().ComputeHash(Encoding.Default.GetBytes(Value));
                return Convert.ToBase64String(hash);
            }
            set { Value = value; }
        }

        public string Value { get; set; }
        public string QuestionId { get; set; }
        public string QuestionTitle { get; set; }

        public string QuestionKey
        {
            get
            {
                var hash = SHA256.Create().ComputeHash(Encoding.Default.GetBytes(QuestionTitle));
                return Convert.ToBase64String(hash);
            }
            set { QuestionTitle = value; }
        }

        public QuizQuestion Question { get; set; }
    }
}