using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PhaticDialogue
{
    public enum AnswerTypes
    {
        GeneralAnswer,
        AnswerToQuestion,
        Question,
        Hello,
        Bye
    };

    public enum QuestionWords
    {
        What,
        When,
        Why,
        Which,
        Who,
        How,
        Whose,
        Whom,
        Am,
        Are,
        Will,
        Would,
        Shall,
        Should,
        Does
    }

    public enum Persons
    {
        You,
        Me,
        I,
        Gen
    }
    public interface IDatabase
    {
        [JsonPropertyName("type")]
        AnswerTypes AnswerTypes { get; set; }
            
        [JsonPropertyName("values")]
        Dictionary<Persons,List<string>> Answers { get; set; }
    }
}