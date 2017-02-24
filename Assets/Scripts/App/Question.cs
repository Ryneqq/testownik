using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Question : MonoBehaviour {
    public Button aButt;
    private Anwser[] aList;
    private string text;

    public Question(int a, string t)
    {
        aList = new Anwser[a];
    }
    public void Spawn()
    {
        Button temp;
        foreach (Anwser a in aList)
        {
            // spawnuj guziki
        }
    }
    public void Check()
    {
        foreach(Anwser a in aList)
        {
            // sprawdz czy zaznaczone są poprawne
        }
    }
}
