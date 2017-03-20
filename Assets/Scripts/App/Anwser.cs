using UnityEngine;
using System.Collections;

public class Anwser : MonoBehaviour {
    private bool correct;
    private bool chosen = false;
    private string text;

    public void SetAnwser(bool c, string t)
    {
        correct = c;
        text = t;
        gameObject.GetComponent<LogControl>().Set(text);
    }
    public void Choose()
    { // TODO: Zrobic zmiane kolorow buttona jak jest wybrany;
        if (!chosen)
            chosen = true;
        else
            chosen = false;
    }
    public bool Chosen()
    {
        return chosen;
    }
    public bool Correct()
    {
        return correct;
    }

}
