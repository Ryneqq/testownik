using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Base : MonoBehaviour
{
    private List<string> baseQ = new List<string>();    // w liście przetrzymuje tyle tablic Q ile mam ustawionych powtórzen
    private int repetitions = 3;                        // powtórzenia
    private Question question;                          // obiekt, którym ustawiamy text, przsyłemy pytania etc.
    private Check check;                                // tekst na guziku 'sprawdz'

    void Awake()
    {
        this.Setup();
        this.CheckForSave();
    }

    private void Setup()
    {
        Variables.Clear();

        this.question    = GameObject.FindGameObjectWithTag("Question").GetComponent<Question>();
        this.check       = GameObject.FindGameObjectWithTag("Check").GetComponent<Check>();

        Load.Setup();
        Save.Setup();
        question.Setup();
    }

    private void CheckForSave()
    {
        if(Load.CheckForSave())
        {
            question.SetQuestionValue("Czy chcesz wczytać zapis?");
            check.SetText("Ok");
            check.Loading();
            GetComponent<Spawn>().SpawnYesNo();
        }
        else
        {
            LoadBase();
        }
    }

    public void LoadBase()
    {
        if (Load.Count() > 0)
        {
            InitBase();
            SetQuestion();
        }
        else
        {
            question.SetQuestionLabel("Brak pytań!\nUmieść bazę pytań w odpowiednim miejscu na swoim urządzeniu.");
            check.SetText("Ok");
        }
    }

    public void LoadBase(List<string> b)
    {
        baseQ = b;
    }

    private void InitBase()
    {
        var Q         = Load.ReadPath();
        var questions = Q.GetLength(0);

        for (int i = 0; i < repetitions; i++)
        {
            this.baseQ.AddRange(new List<string>(SetQueue(Q)));
        }
    }

    private string [] SetQueue(string[] Q)
    {
        int questions = Q.Length;
        int rand;
        string temp;

        for (int i = 0; i < questions - 1; i++){
            rand = Random.Range(i, questions);
            temp = Q[i];
            Q[i] = Q[rand];
            Q[rand] = temp;
        }

        return Q;
    }

    public void SaveBase()
    {
        Save.SaveProgress(baseQ);
    }

    public void NewQuestion()
    {
        question.Clear();

        if (!Learned())
        {
            SetQuestion();
        }
        else
        {
            LearningSucceded(); 
        }
    }

    public void SetQuestion()
    {
        string read = Load.Read(LastQuestion());
        this.SetQuestionName();
        question.InitQuestion(read);
    }

    private void LearningSucceded()
    {
        string text = "Wszystkie pytania zostały opanowane";
        question.SetQuestionValue(text);
        question.ResetQuestionValue();
        check.SetText("Ok!");
        SetText("fin");
        GameObject.Find("Piwo").GetComponent<Image>().enabled = true;
    }

    public void RemoveQuestion()
    {
        baseQ.RemoveAt(Qs() - 1);
    }

    public bool Learned()
    {
        if (Qs() > 0)
            return false;
        return true;
    }

    private void SetQuestionName()
    {
        var pathSplited = LastQuestion().Split('/');
        var fileName    = pathSplited[pathSplited.GetLength(0) - 1];

        SetText(fileName);
    }
    public void SetText(string s)
    {
        gameObject.GetComponent<LogControl>().Set(s);
    }

    public string LastQuestion()
    {
        return baseQ[Qs() - 1];
    }

    public int Qs()
    {
        return baseQ.Count; 
    }
}