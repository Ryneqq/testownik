using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Base : MonoBehaviour {
    private List<string> qBase = new List<string>();
    private int repetitions = 3;
    private Question question;
    private Check check;

    void Awake() {
        this.Setup();
        this.CheckForSave();
    }

    private void Setup() {
        Variables.Clear();

        this.question = GameObject.FindGameObjectWithTag("Question").GetComponent<Question>();
        this.check = GameObject.FindGameObjectWithTag("Check").GetComponent<Check>();

        Load.Setup();
        Save.Setup();
        this.question.Setup();
    }

    private void CheckForSave() {
        if (Load.CheckForSave()) {
            question.SetQuestionValue("Czy chcesz wczytać zapis?");
            check.SetText("Ok");
            check.Loading();
            GetComponent<Spawn>().SpawnYesNo();
        } else {
            LoadBase();
        }
    }

    public void LoadBase() {
        if (Load.Count() > 0) {
            InitBase();
            SetQuestion();
        }
        else
        {
            question.ErrorOccured("Brak pytań!\nUmieść bazę pytań w odpowiednim miejscu na swoim urządzeniu.");
        }
    }

    public void LoadBase(List<string> qBase) {
        this.qBase = qBase;
    }

    private void InitBase() {
        var readBase = Load.ReadBase();
        var repetitions = Enumerable.Range(0, this.repetitions);

        foreach (var i in repetitions) {
            var queue = new List<string>(SetQueue(readBase));
            this.qBase.AddRange(queue);
        }
    }

    private string [] SetQueue(string[] qBase) {
        var questions = Enumerable.Range(0, qBase.Length - 1);

        foreach (var i in questions) {
            var rand = Random.Range(i, qBase.Length);
            var temp = qBase[i];
            qBase[i] = qBase[rand];
            qBase[rand] = temp;
        }

        return qBase;
    }

    public void SaveBase() {
        Save.SaveProgress(this.qBase);
    }

    public void NewQuestion() {
        question.Clear();

        if (!Learned()) {
            SetQuestion();
        } else {
            LearningSucceded();
        }
    }

    public void SetQuestion() {
        var read = Load.Read(LastQuestion());
        this.SetQuestionFileName();
        question.InitQuestion(read);
    }

    private void LearningSucceded() {
        string text = "Wszystkie pytania zostały opanowane";
        question.SetQuestionValue(text);
        question.ResetQuestionValue();
        check.SetText("Ok!");
        this.SetText("fin");
        GameObject.Find("Piwo").GetComponent<Image>().enabled = true;
    }

    public void RemoveQuestion() {
        this.qBase.RemoveAt(Qs() - 1);
    }

    public bool Learned() {
        return !(Qs() > 0);
    }

    private void SetQuestionFileName() {
        var pathSplited = LastQuestion().Split('/');
        var fileName = pathSplited[pathSplited.Length - 1];

        SetText(fileName);
    }
    public void SetText(string s) {
        gameObject.GetComponent<LogControl>().Set(s);
    }

    public string LastQuestion() {
        return this.qBase[Qs() - 1];
    }

    public int Qs() {
        return this.qBase.Count;
    }
}
