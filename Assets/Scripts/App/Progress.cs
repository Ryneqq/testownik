using System;
using UnityEngine;

public class Progress : MonoBehaviour {
    private RectTransform bar;
    private Question question;
    private bool clicked;
    private float time;
    private float displayTime = 3.0f;

    void Start()
    {
        this.bar        = GameObject.FindGameObjectWithTag("Bar").GetComponent<RectTransform>();
        this.question   = GameObject.FindGameObjectWithTag("Question").GetComponent<Question>();
    }

    void Update()
    {
        Variables.studyTime += Time.deltaTime;
        CheckShowingStatistics();
    }

    public void CountPercentage()
    {
        float percentage;
        float anwsered = (float)Variables.anwsered;
        float correct  = (float)Variables.correct;

        percentage = correct / anwsered;

        Resize(percentage);
    }

    private void CheckShowingStatistics()
    {
        if (clicked)
        {
            ShowStats();
            time += Time.deltaTime;
            if(time > displayTime)
            {
                Clicked();
            }
        }
    }

    private void Resize(float percentage)
    {
        this.bar.localScale = new Vector3(percentage * 2, this.bar.localScale.y, this.bar.localScale.z);
    }

    public void Clicked()
    {
        if (!this.clicked)
        {
            this.clicked = true;
            this.time    = 0.0f;
        }
        else
        {
            this.clicked = false;
            this.question.ResetQuestionValue();
        }
    }

    // Format time hh:mm:ss
    private string FormatTime(float t)
    {
        var learning_time = TimeSpan.FromSeconds(t)
            .ToString()
            .Split('.')[0];

        return learning_time;
    }

    public void ShowStats()
    {
        string text = String.Empty;
        text += "Czas nauki: " + FormatTime(Variables.studyTime) + "\n\r";
        text += "Odpowiedzi: " + Variables.anwsered + "\n\r";
        text += "Poprawne: " + Variables.correct;
        question.SetQuestionLabel(text);
    }
}
