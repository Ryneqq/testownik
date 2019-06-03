using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestionDto {
    public string q;
    public string[] ca;
    public string[] wa;

    public QuestionDto(string question, string[] correct_anwsers, string[] incorrect_anwsers)
    {
        this.q   = question;
        this.ca  = correct_anwsers;
        this.wa  = incorrect_anwsers;
    }

    public int AnwsersCount()
    {
        return ca.Length + wa.Length;
    }

    public AnserDto[] GetAnwsers()
    {
        var anwsers = new List<AnserDto>();
        foreach (var anwser in this.ca)
        {
            anwsers.Add(new AnserDto(true, anwser));
        }
        foreach (var anwser in this.wa)
        {
            anwsers.Add(new AnserDto(false, anwser));
        }

        return anwsers.ToArray();
    }

    public string IntoJson() {
        return JsonUtility.ToJson(this);
    }

    public static QuestionDto FromJson(string json) {
        return JsonUtility.FromJson<QuestionDto>(json);
    }

    public static QuestionDto FromString(string str) {
        try
        {
            return OldFormat.TryParse(str);
        }
        catch (System.FormatException ex)
        {
            return QuestionDto.FromJson(str);
        }
    }
}
