using System;
using System.Collections.Generic;
using UnityEngine;

public class Synchronizer: MonoBehaviour {
    void Start() {
        Synchronize();
    }

    private void Synchronize() {
        var paths = Load.ReadDirectories(Variables.localBase);

        foreach (var path in paths)
        {
            SynchronizeBase(path);
        }
    }

    private void SynchronizeBase(string path) {
        var requester = new Requester();
        var questions = new List<QuestionDto>();
        var basePath  = Load.ReadFiles(path);

        foreach (var filePath in basePath)
        {
            var file = Load.Read(filePath);
            questions.Add(QuestionDto.FromString(file));
        }

        var baseJson = new BaseDto(path, questions.ToArray()).IntoJson();
        requester.PostRequest("stubbed_url", baseJson);
    }
}
