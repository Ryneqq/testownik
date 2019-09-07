using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Check : MonoBehaviour {
    private Base baseObj;
    private Progress progress;
    private bool anwsered = false;
    private bool loading= false;
    private bool saving = false;


    void Awake(){
        baseObj  = Camera.main.GetComponent<Base>();
        progress = Camera.main.GetComponent<Progress>();
    }

    public void Clicked()
    {
        if (this.loading || this.saving) {
            CheckYesNo();
            return;
        }

        if (!this.anwsered) {
            if (baseObj.Learned()) {
                Application.LoadLevel("Menu");
            } else {
                this.anwsered = true;
                this.SetText("Dalej");
                this.CheckAnwsers();
            }
        } else {
            this.SetText("Sprawdź");
            this.baseObj.NewQuestion();
            this.anwsered = false;
        }
    }
    public void CheckAnwsers() {
        if (CheckCorectness()) {
            Variables.correct++;
            this.baseObj.RemoveQuestion();
        }

        Variables.anwsered++;
        this.progress.CountPercentage();
    }

    private void CheckYesNo() {
         if (this.saving) {
            if (CheckCorrectnessSimple()) {
                baseObj.SaveBase();
            }

            Application.LoadLevel("Menu");
        } else {
            if (CheckCorrectnessSimple()) {
                baseObj.LoadBase(Load.LoadSave());
            } else {
                baseObj.LoadBase();
            }

            this.loading = false;
            this.saving = false;
            this.SetText("Sprawdź");
            this.baseObj.NewQuestion();
        }
    }

    private bool CheckCorectness() {
        GameObject [] ansList = GameObject.FindGameObjectsWithTag("Anwser");
        bool correct = true;

        foreach(GameObject a in ansList) {
            var anwser = a.GetComponent<Anwser>();
            var image  = a.GetComponent<Image>();
            var button = a.GetComponent<Button>();

            if (anwser.IncorrectAnwser()) {
                correct = false;
                if (anwser.Correctness() == PossibleAnwsers.correctNotChosen) {
                    image.color = Color.green;
                }
                if (anwser.Correctness() == PossibleAnwsers.incorrectChosen) {
                    image.color = Color.red;
                }
            }
            else if (anwser.Correctness() == PossibleAnwsers.correctChosen) {
                    image.color = Color.green;
            }

            button.enabled = false;
        }

        return correct;
    }

    private bool CheckCorrectnessSimple()
    {
        GameObject [] ansList = GameObject.FindGameObjectsWithTag("Anwser");
        bool correct = true;

        foreach(GameObject a in ansList)
        {
            var anwser = a.GetComponent<Anwser>();
            if (anwser.IncorrectAnwser())
            {
                correct = false;
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
