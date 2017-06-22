using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
/// Obiekt opiekujący się obsługą guzika 'Sprawdź'
///</summary>
public class Check : MonoBehaviour {
	private Base baseObj;
	private Question question;
	private Progress progress;
	private bool anwsered = false; 
    private bool loading= false;
    private bool saving = false;


	void Awake(){
		baseObj = Camera.main.GetComponent<Base>();
        progress = Camera.main.GetComponent<Progress>();        
        question = GameObject.FindGameObjectWithTag("Question").GetComponent<Question>();
	}
    // ====================================== Obsługa guzika ======================================
    // metoda wywoływana przez 'Ask()'
    // wywołuje metodę 'Check()' obiektu 'Question' która sprawdza poprawność zazn. odp.
    // jeżeli odpowiedzi były poprawne, usuwa pytanie z listy, jeżeli nie
    // to zostanie zadane to samo pytanie
    public void SetText(string text){
        gameObject.GetComponent<LogControl>().Set(text);
    }
    public void Saving(){
        saving = true;
    }
    public void Loading(){
        loading = true;
    }
    public void Clicked()
    {
        if(loading) {
            SetText("Ok");
            CheckYesNo();
            return;
        } else if(saving) {
            SetText("Ok");
            CheckYesNo();
            return;
        }

        if (!anwsered)
        {
            if(baseObj.Learned()){
                Application.LoadLevel("Menu");
            } else {
				anwsered = true;
                SetText("Dalej");
                Checked();          // Sprawdzono odpowiedź
            }
        } else {
            SetText("Sprawdź");
            baseObj.NewQuestion();
            anwsered = false;
        }
    }
    public void Checked()
    {
        //sprawdz czy odpowiedzi byly poprawne
        if (CheckCorectness())
        {
            Variables.correct++;
            baseObj.RemoveQuestion();
        }
        Variables.anwsered++;
        progress.CountPercentage();
    }
    public void CheckYesNo(){
         if(saving){
            if (EasyCheck())
                {
                   baseObj.SaveBase();
                }
                Application.LoadLevel("Menu");
        } else {
            if(EasyCheck())
            {
                baseObj.LoadBase(Camera.main.GetComponent<Load>().LoadSave());
            } else {
                baseObj.LoadBase();
            }
            loading = false;
            saving = false;
            SetText("Sprawdź");
            baseObj.NewQuestion();
        }
    }
    public bool CheckCorectness()
    {
        GameObject [] ansList = GameObject.FindGameObjectsWithTag("Anwser");
        bool correct = true;
        foreach(GameObject a in ansList)
        {
            
            if (a.GetComponent<Anwser>().Correctness() < 3)
            {
                correct = false; // zła odpowiedź
                if (a.GetComponent<Anwser>().Correctness() == 1)
                    a.GetComponent<Image>().color = Color.green;
                if (a.GetComponent<Anwser>().Correctness() == 2)
                    a.GetComponent<Image>().color = Color.red;
            } else { 
                if (a.GetComponent<Anwser>().Correctness() == 3)
                    a.GetComponent<Image>().color = Color.green;
            }
            a.GetComponent<Button>().enabled = false;
        }
        // gdy wszystkie odpowiedzi są zaznaczone poprawnie to zwróci prawdę,
        // w każdym innym przypadku będzie false.
        return correct;
    }
    public bool EasyCheck()
    {
        GameObject [] ansList = GameObject.FindGameObjectsWithTag("Anwser");
        bool correct = true;
        foreach(GameObject a in ansList)
        {
            
            if (a.GetComponent<Anwser>().Correctness() < 3)
            {
                correct = false; // zła odpowiedź
            }
        }
        return correct;
    }
    // ============================================================================================
}
