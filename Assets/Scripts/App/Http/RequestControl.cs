using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RequestControl: MonoBehaviour
{
    readonly string url = "http://145.239.31.166:8000/bases";
    Requester requester;
    string token;

    void Start() {
        this.requester =  new Requester();
        this.GetQuestionBase();
    }

    // RequestControl()
    // {
    //     this.requester = new Requester();
    // }

    public void Authorize(string username, string passhash)
    {
        // make a request with json
        this.token = string.Empty;
    }

    public void GetQuestionBase()
    {
        StartCoroutine(this.requester.GetRequest(this.url));
    }

    public void PostQuestionBase()
    {
        // form json string of questions from dto list
    }
}
