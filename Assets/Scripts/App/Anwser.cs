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
    }
    public void Choose()
    {
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
