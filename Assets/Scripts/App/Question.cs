using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour {

    private int anwsersCount;
    private string questionLabel;
    private Spawn spawn;
    private Check check;

    public void Setup(){
        spawn = Camera.main.GetComponent<Spawn>();
        check = GameObject.FindGameObjectWithTag("Check").GetComponent<Check>();
    }

    public void InitQuestion(string read)
    {
        try
        {
            var questionDto = OldFormat.TryParse(read);

            this.SetQuestionValue(questionDto.q);
            this.SetNumberOfAnwsers(questionDto.AnwsersCount());
            this.spawn.SpawnAnwsers(this.Randomize(questionDto.GetAnwsers()));

        }
        catch (System.FormatException ex)
        {
            ErrorOccured(ex.Message);
        }
    }

    public void ErrorOccured(string message) {
        SetQuestionLabel(message);
        check.SetText("Ok");
    }

    private void SetNumberOfAnwsers(int anwsers) {
        this.anwsersCount = Mathf.Min(anwsers, 5);
    }

    public AnserDto[] Randomize(AnserDto[] ans)
    {
        AnserDto temp;

        for (int i = 0; i < anwsersCount - 1; i++)
        {
            var rand = Random.Range(1, anwsersCount);
            temp = ans[i];
            ans[i] = ans[rand];
            ans[rand] = temp;
        }

        return ans;
    }

    public void SetQuestionValue(string label){
        this.questionLabel = label;
        SetQuestionLabel(this.questionLabel);
    }

    public void ResetQuestionValue()
    {
        SetQuestionLabel(this.questionLabel);
    }

    public void SetQuestionLabel(string label)
    {
        this.gameObject.GetComponent<LogControl>().Set(label);
    }

    public void Clear()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Anwser");
        foreach(GameObject t in temp)
        {
            Destroy(t.gameObject);
        }
    }

    public void Turn(bool b)
    {
        check.GetComponent<Button>().enabled = b;
        gameObject.GetComponent<Button>().enabled = b;

        GameObject[] temp = GameObject.FindGameObjectsWithTag("Anwser");
        foreach (GameObject t in temp)
        {
            t.GetComponent<Button>().enabled = b;
        }
    }
}
