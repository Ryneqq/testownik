using System.Collections;
using System.Collections.Generic;

public class QuestionDto {
    public string question;
    public string[] correct_anwsers;
    public string[] incorrect_anwsers;

    public QuestionDto(string question, string[] correct_anwsers, string[] incorrect_anwsers)
    {
        this.question           = question;
        this.correct_anwsers    = correct_anwsers;
        this.incorrect_anwsers  = incorrect_anwsers;
    }

    public int AnwsersCount()
    {
        return correct_anwsers.Length + incorrect_anwsers.Length;
    }

    public Ans[] GetAnwsers()
    {
        var anwsers = new List<Ans>();
        foreach (var anwser in this.correct_anwsers)
        {
            anwsers.Add(new Ans(true, anwser));
        }
        foreach (var anwser in this.incorrect_anwsers)
        {
            anwsers.Add(new Ans(false, anwser));
        }

        return anwsers.ToArray();
    }
}
