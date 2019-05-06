using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RequestControl
{
    readonly string url = "some_url";
    Requester requester;
    string token;

    RequestControl()
    {
        this.requester = new Requester();
    }

    public void Authorize(string username, string passhash)
    {
        // make a request with json
        this.token = string.Empty;
    }

    public void GetQuestionBase()
    {
        // form list of questions from dto list and save it on the disc
        // read name
    }

    public void PostQuestionBase()
    {
        // form json string of questions from dto list
    }
}
