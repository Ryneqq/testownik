using UnityEngine;
using UnityEngine.UI;

public enum PossibleAnwsers
{
    correctChosen, incorrectChosen, correctNotChosen, incorrectNotChosen
}

public struct AnserDto
{
    public bool correct;
    public string text;

    public AnserDto(bool c, string t)
    {
        text = t;
        correct = c;
    }
};

public class Anwser : MonoBehaviour
{
    private bool correct;           // czy odpowiedź jest poprawna
    private bool chosen = false;    // czy ta odpowiedź została zaznaczona przez użytkownika

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

    public void SetAnwser(bool c, string t)
    {
        this.correct = c;
        this.SetText(t);
    }

    public void SetText(string s)
    {
        gameObject.GetComponent<LogControl>().Set(s);
    }

    public bool IncorrectAnwser()
    {
        var correctness = Correctness();

        return correctness != PossibleAnwsers.correctChosen
            && correctness != PossibleAnwsers.incorrectNotChosen;
    }

    public PossibleAnwsers Correctness()
    {
        if (!this.chosen && this.correct)
            return PossibleAnwsers.correctNotChosen;
        else if (this.chosen && !this.correct)
            return PossibleAnwsers.incorrectChosen;
        else if (this.chosen && this.correct)
            return PossibleAnwsers.correctChosen;
        else
            return PossibleAnwsers.incorrectNotChosen;
    }
}
