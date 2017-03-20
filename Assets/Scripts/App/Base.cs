using UnityEngine;
using System.Collections;

public struct BaseQ
{
    public string name;
    public int repetitions;
};

public class Base : MonoBehaviour {
    private BaseQ[] Q;
    private int questions = 0, index = 0;
    private Question q;
    public string question;

    void Start()
    {
        q = GameObject.FindGameObjectWithTag("Question").GetComponent<Question>();
        questions = gameObject.GetComponent<Load>().CountQ();
        InitBase();
    }
    void Update()
    {
        Variables.studyTime += Time.deltaTime;
        if (q.Anwsered == true)
        {
            q.Clear();
            if (index < questions)
            {
                question = gameObject.GetComponent<Load>().ReadQ(Q[index].name);
                gameObject.GetComponent<LogControl>().Set(Q[index].name + ".txt");
                q.InitQuestion(question);
                q.Anwsered = false;
                index++;
            }
        }
    }
    public void SetQueue()
    {
        int rand;
        BaseQ temp;
        for (int i = 0; i < questions - 1; i++)
        {
            rand = Random.Range(i, questions);
            temp = Q[i];
            Q[i] = Q[rand];
            Q[rand] = temp;
        }
    }
    public void InitBase()
    {
        Q = new BaseQ[questions];
        for (int i=1; i<=questions; i++)
        {
            if (i < 10)
                Q[i - 1].name = "00" + i.ToString();
            else if (i < 100)
                Q[i - 1].name = "0" + i.ToString();
            else
                Q[i - 1].name = i.ToString();

            Q[i - 1].repetitions = 3;

        }
        SetQueue();
    }
    public void Ask()
    {
        q.Anwsered = true;
    }
}
