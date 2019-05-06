using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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

    public AnserDto[] GetAnwsers()
    {
        var anwsers = new List<AnserDto>();
        foreach (var anwser in this.correct_anwsers)
        {
            anwsers.Add(new AnserDto(true, anwser));
        }
        foreach (var anwser in this.incorrect_anwsers)
        {
            anwsers.Add(new AnserDto(false, anwser));
        }

        return anwsers.ToArray();
    }

    public string IntoJson() {
        return JsonUtility.ToJson(this);
    }

    public QuestionDto FromJson(string json) {
        return JsonUtility.FromJson<QuestionDto>(json);
    }
}
