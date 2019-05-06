using System;
using UnityEngine;

[Serializable]
public class BasesDto {
    public BaseDto[] json_dbs;

    public BasesDto(BaseDto[] json_dbs) {
        this.json_dbs = json_dbs;
    }

    public string IntoJson() {
        return JsonUtility.ToJson(this);
    }

    public static BasesDto FromJson(string json) {
        return JsonUtility.FromJson<BasesDto>(json);
    }
}
