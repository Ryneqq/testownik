using UnityEngine;
using UnityEngine.UI;

public class Anwser : MonoBehaviour
{
    private bool correct;           // czy odpowiedź jest poprawna
    private string text;            // treść odpowiedzi

    private bool chosen = false;    // czy ta odpowiedź została zaznaczona przez użytkownika

    // ============== Ustaw odpowiedź ==============

    // nadaj wartości odpowiedzi
    public void SetAnwser(bool c, string t)
    {
        correct = c;
        text = t;
        SetText(text);
    }
    // zmień tekst na guziku odpowiedzi
    public void SetText(string s)
    {
        gameObject.GetComponent<LogControl>().Set(s);
    }

    // ==============================================

    // ============== Zarządzaj odpowiedzią ==============

    // Metoda wywoływana kliknięciem guzika na daną odpowiedź
    // zmienia kolor guzika i pamięta czy był wybrany czy nie
    public void Choose()
    { 
        if (!chosen)
        {
            chosen = true;
            gameObject.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            chosen = false;
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    // Metoda wywoływana przy określaniu poprawności
    // zwraca int, ponieważ są cztery warianty
    public int Correctness()
    {
        if (!chosen && correct) // wybrany zly
            return 1;
        else if (chosen && !correct) // nie wybrany dobry
            return 2;
        else if (chosen && correct) // wybrany dobry
            return 3;
        else // nie wybrany zly
            return 4;
    }

    // ===================================================
}
