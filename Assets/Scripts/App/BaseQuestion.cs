using UnityEngine;
using System.Collections;

public class BaseQuestion : MonoBehaviour {
    private Anwser[] aList;
    private string text;
    private int repetitions;

    public void SetRep(int r)
    {
        repetitions = r;
    }
    public int Repetitions()
    {
        return repetitions;
    }
    public static BaseQuestion operator ++ (BaseQuestion q)
    {
        ++q.repetitions;
        return q;
    }
    public static BaseQuestion operator -- (BaseQuestion q)
    {
        --q.repetitions;
        return q;
    }
}
