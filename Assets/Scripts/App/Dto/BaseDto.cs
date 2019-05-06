using System;
using UnityEngine;

[Serializable]
public class BaseDto {
    public string name;
    public QuestionDto[] json_db;

    public BaseDto(string name, QuestionDto[] json_db) {
        this.name = name;
        this.json_db = json_db;
    }

    public string IntoJson() {
        return JsonUtility.ToJson(this);
    }

    public static BaseDto FromJson(string json) {
        return JsonUtility.FromJson<BaseDto>(json);
    }
}
