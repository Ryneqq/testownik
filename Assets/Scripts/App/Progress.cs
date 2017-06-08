using System;
using UnityEngine;

/// <Progress>
/// Klasa odpowiada za wyświetlanie postępów użytkownikowi
/// - wyświetla pasek stosunku poprawnych odpowiedzi
/// - liczy czas nauki i potrafi go wyświetlić w okienku pytania po naciśnięciu
/// </Progress>

public class Progress : MonoBehaviour {

    public RectTransform bar;           // zielony bar - przeciagnij
    private Question question;          // obiekt pytania, do ustawiania tekstu
    private bool clicked;               // czy użytkownik klikną
    private float time;                 // liczenie jak długo minęło od kliknięcia
    private float displayTime = 3.0f;   // jak długo mają się wyświetlać statystyki


    void Start()
    {
        question = GameObject.FindGameObjectWithTag("Question").GetComponent<Question>();
    }

    void Update()
    {
        Variables.studyTime += Time.deltaTime;
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

    public void CountPercentage()
    {
        float percentage;
        float anwsered = (float)Variables.anwsered;
        float correct = (float)Variables.correct;

        percentage = correct / anwsered;

        Resize(percentage);
    }

    private void Resize(float percentage)
    {
        // p * 2 ponieważ skala startowa to 2, wiec jezeli p == max == 1, to p*2 == 2 
        bar.localScale = new Vector3(percentage * 2, bar.localScale.y, bar.localScale.z);
    }

    // Wywoływane przez guzik Question
    public void Clicked()
    {
        if (!clicked)
        {
            // clicked
            clicked = true;
            time = 0.0f;
        }
        else
        {
            // unclicked
            clicked = false;
            question.SetText();
        }
    }

    // Metoda przycina stringa
    private string CutStr(string s)
    {
        string temp = String.Empty;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '.')
            {
                break;
            }
            temp += s[i];
        }
        return temp;
    }

    // Metoda słóży do wyswietlania czasu w formacie 'x'h 'x'm 'x's
    private string FormatTime(float t)
    {
        float hours, minutes, seconds;
        string temp = String.Empty;

        if (t / 60 > 1) // uczy sie dłużej niz minuta
        {
            minutes = t / 60;
            seconds = t % 60;

            if (minutes / 60 > 1) // uczy się dłużej niż godzina
            {
                hours = minutes / 60;
                minutes = minutes % 60; // korzysta z poprzedniej wartości
                seconds = seconds % 60; // korzysta z poprzedniej wartości

                if (hours >= 24)
                {
                    temp += "Zrób przerwę :D\n\r";
                    temp += "Idź na piwo czy coś";
                    return temp;
                }

                temp += CutStr(hours.ToString()) + "h ";
            }

            temp += CutStr(minutes.ToString()) + "m ";
            temp += CutStr(seconds.ToString()) + "s ";
            return temp;
        }
        else
        {
            seconds = t;
            return CutStr(seconds.ToString()) + "s ";
        }

    }

    // Metoda wypisuje statystyki
    public void ShowStats()
    {
        string text = String.Empty;
        text += "Czas nauki: " + FormatTime(Variables.studyTime) + "\n\r";
        text += "Odpowiedzi: " + Variables.anwsered + "\n\r";
        text += "Poprawne: " + Variables.correct;
        question.SetText(text);
    }
}
