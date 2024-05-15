using Newtonsoft.Json;

namespace QueryApp2.Models
{
    public class PersonalInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; } // Partition key

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Nationality")]
        public string Nationality { get; set; }

        [JsonProperty("currentResidence")]
        public string CurrentResidence { get; set; }

        [JsonProperty("idNumber")]
        public string IdNumber { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; } 
    /// <summary>
    /// Navigation property for the custom questions associated with the personal info.
    /// </summary>
        public virtual ICollection<CustomQuestion> CustomQuestions { get; set; }
    }
}
