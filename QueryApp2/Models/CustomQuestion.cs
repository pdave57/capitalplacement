using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueryApp2.Models
{
    public class CustomQuestion
    {
        /// <summary>
        /// Gets or sets the unique identifier for the question.
        /// </summary>

        [JsonProperty("id")]
        public string Id { get; set; } // Primary Key

        /// <summary>
        /// Gets or sets the text of the question.
        /// </summary>
        /// 

        [JsonProperty("questionText")]
        public string QuestionText { get; set; } // Question Text

        /// <summary>
        /// Gets or sets the type of the question which determines the response format.
        /// </summary>
        [NotMapped]
        public Object OptionType
         { get; set; } // Type of Question (Paragraph, YesNo, etc.)

        /// <summary>
        /// Gets or sets the options for the question, applicable if the type is Dropdown or MultipleChoice.
        /// </summary>
        /// 
        [JsonProperty("options")]
        public string[] Options { get; set; } // Options for the question if applicable
    }

    public enum QuestionType
    {
        Paragraph,
        YesNo,
        Dropdown,
        MultipleChoice,
        Date,
        Number
    }

}
