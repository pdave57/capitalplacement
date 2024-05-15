using QueryApp2.Models;

namespace QueryApp2.DTOs
{
    public class UpdateQuestionDto
    {
        public string Id { get; set; } // Required for update
        public string Text { get; set; } 
        public QuestionType Type { get; set; } 
        public string Options { get; set; } 
    }
}
