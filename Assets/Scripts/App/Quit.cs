using UnityEngine;

/// <BackButton>
/// Metoda słóży do zczytyania naciśnięć na hardware'owy guzik 'cofnij'
/// Po double clicku, program powinien zakończyć swoje działanie, ale najpierw zada ostatnie pytanie:
/// Czy użytkownik chce zapisać aktualny stan? po czym zapisze/niezapisze i wyjdzie
/// </BackButton>
public class Quit : MonoBehaviour {

    private bool clicked = false;       // czy został kliknięty przycisk hardware'owy 'back'
    private bool saving = false;        // czy nacisnieto 2 razy guzik cofinj
    private float time = 0.0f;          // czas od naklikniecia w/w guzika
    private float resetTime = 1.3f;     // czas resetu naciśniecia guzika
    private Question question;          // obiekt 'Question' do wyświetlania komunikatów, interakcji z użytkownikiem
    private Base b;
    private Check check;

    void Start()
    {
        question = GameObject.FindGameObjectWithTag("Question").GetComponent<Question>();   
        check = GameObject.FindGameObjectWithTag("Check").GetComponent<Check>();
        b = gameObject.GetComponent<Base>();
    }
    void LateUpdate () {
        if (Input.GetKey(KeyCode.Escape) && !b.Learned())
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
        if (Input.GetKey(KeyCode.Escape) && b.Learned()){
            Application.LoadLevel("Menu");
        }
    }

    private void ClickedOnce()
    {
        clicked = true;
        question.SetText("Naciśnij wstecz by wyjść");
        question.Turn(false);
    }
    public void ClickedTwice()
    {
        question.Turn(true);
        question.Set("Czy chcesz zapisać postęp?");
        question.SetText();
        question.Clear();
        
        GetComponent<Spawn>().SpawnYesNo();

        check.Saving();
        check.SetText("Ok");
    }
    private void ResetClick()
    {
        time = 0.0f;
        clicked = false;
        question.SetText();
        question.Turn(true);
    }
}
