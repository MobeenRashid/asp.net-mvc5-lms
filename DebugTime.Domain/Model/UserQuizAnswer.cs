namespace DebugTime.Domain.Model
{
    public class UserQuizAnswer
    {
        public string UserId { get; set; }
        public string QuizId { get; set; }
        public string QuestionKey { get; set; }
        public string Answer { get; set; }
        public string AnswerKey { get; set; }
        public bool IsCorrect { get; set; }
    }
}