using QueryApp2.Models;

namespace QueryApp2.DTOs
{
    public class CreateQuestionDto
    {
        public string QuestionText { get; set; } // Question Text

        /// <summary>
        /// Gets or sets the type of the question which determines the response format.
        /// </summary>
        public Object OptionType
         { get; set; } // Type of Question (Paragraph, YesNo, etc.)

        /// <summary>
        /// Gets or sets the options for the question, applicable if the type is Dropdown or MultipleChoice.
        /// </summary>
        public string[] Options { get; set; } // Options for the question if applicable
    }
}
