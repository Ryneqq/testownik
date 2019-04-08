using UnityEngine;

public class Quit : MonoBehaviour {
    private bool clicked = false;
    private bool saving = false;
    private float time = 0.0f;
    private float resetTime = 1.3f;
    private Question question;
    private Base questionBase;
    private Check check;

    void Start()
    {
        question        = GameObject.FindGameObjectWithTag("Question").GetComponent<Question>();
        check           = GameObject.FindGameObjectWithTag("Check").GetComponent<Check>();
        questionBase    = gameObject.GetComponent<Base>();
    }

    void LateUpdate () {
        if (Input.GetKey(KeyCode.Escape) && !questionBase.Learned())
        {
            if (!clicked)
            {
                ClickedOnce();
            }
            else if (time > 0.2f && !saving)
            {
                ClickedTwice();
                saving = true;
            }
        }
        if (clicked)
        {
            time += Time.deltaTime;
            if(time > resetTime)
            {
                ResetClick();
            }
        }
        if (Input.GetKey(KeyCode.Escape) && questionBase.Learned()){
            Application.LoadLevel("Menu");
        }
    }

    private void ClickedOnce()
    {
        clicked = true;
        question.SetQuestionLabel("Naciśnij wstecz by wyjść");
        question.Turn(false);
    }

    public void ClickedTwice()
    {
        question.Turn(true);
        question.SetQuestionValue("Czy chcesz zapisać postęp?");
        question.Clear();

        GetComponent<Spawn>().SpawnYesNo();

        check.Saving();
        check.SetText("Ok");
    }

    private void ResetClick()
    {
        time = 0.0f;
        clicked = false;
        question.ResetQuestionValue();
        question.Turn(true);
    }
}
