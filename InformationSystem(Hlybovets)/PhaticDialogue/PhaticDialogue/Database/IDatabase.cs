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
        what,
        when,
        why,
        which,
        who,
        how,
        whose,
        whom,
        am,
        are,
        will,
        would,
        shall,
        should,
        does
    }

    public enum Persons
    {
        you,
        me,
        i,
        gen
    }
    public interface IDatabase
    {
        [JsonPropertyName("type")]
        AnswerTypes answerTypes { get; set; }
            
        [JsonPropertyName("values")]
        Dictionary<Persons,List<string>> answers { get; set; }
    }
}