using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
/// Obiekt zajmujący się obsługą guzika 'Sprawdź'
/// Posiada metody do sprawdzania poprawności zaznaczonych odpowiedzi
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
    public void CheckAnwsers()
    {
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
                baseObj.LoadBase(Load.LoadSave());
            } else {
                baseObj.LoadBase();
            }
            loading = false;
            saving = false;
            SetText("Sprawdź");
            baseObj.NewQuestion();
        }
    }
    // metoda wywoływana przez guzik 'Sprawdź'
    public void Clicked()
    {
        if(loading || saving) {
            CheckYesNo();
            return;
        }

        if (!anwsered) {
            if(baseObj.Learned()){
                Application.LoadLevel("Menu");
            } else {
				anwsered = true;
                SetText("Dalej");
                CheckAnwsers();
            }
        } else {
            SetText("Sprawdź");
            baseObj.NewQuestion();
            anwsered = false;
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
        // Gdy wszystkie odpowiedzi są zaznaczone poprawnie to zwróci prawdę.
        // W każdym innym przypadku będzie false.
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
    public void Saving(){
        saving = true;
    }
    public void Loading(){
        loading = true;
    }
    public void SetText(string text){
        gameObject.GetComponent<LogControl>().Set(text);
    }
}
