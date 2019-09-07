using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Save {
    private static string path;
    private static string fileName = "save.txt";

    public static void Setup()
    {
        path = Application.persistentDataPath + "/" + fileName;
    }

    private static string ListToString(List<string> qBase) {
        List<QuestionDto> questions = new List<QuestionDto>();

        foreach (var item in qBase) {
            questions.Add(QuestionDto.FromString(item));
        }

        return new BaseDto("save", questions.ToArray()).ToString();
    }

    public static void SaveProgress(QuestionDto[] questions) {
        var qBase = new BaseDto("save", questions);
        var content = qBase.IntoJson();

        if (System.IO.File.Exists(path)) {
            System.IO.File.Delete(path);
        }

        using (StreamWriter file = new StreamWriter(path, true)) {
            file.Write(content);
            file.Close();
        }
    }
}
