using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AddQuestionControl : MonoBehaviour {

    public InputField question;
    public InputField[] answers;
    public Toggle[] areAnsweCorrect;

    public Button submitAnswer;

    private void Start()
    {
        submitAnswer.onClick.AddListener(onAddQuestionButtonClicked);
    }

    public void onAddQuestionButtonClicked()
    {
        string correctAnswersCode="X";
        string selectedDataBase = PlayerPrefs.GetString(Variables.LearningBaseKey);
        string[] fileNames = System.IO.Directory.GetFiles(selectedDataBase);
        int newQuestionNameInt = -1;
        string newQuestionName;

        string content = "";

        //Create correct answers code:
        for (int i=0;i<6;i++)
        {
            if (areAnsweCorrect[i].isOn && answers[i].text != "")
            {
                correctAnswersCode += "1";
            }
            else if (!areAnsweCorrect[i].isOn && answers[i].text != "")
            {
                correctAnswersCode += "0";
            }
        }

        //Find highest value of questions to determine new question name:
        for (int i = 0; i < fileNames.Length;i++)
        {
            string num = (fileNames[i].Remove(0, selectedDataBase.Length));
            num = num.Substring(1, num.Length - 5);

            if((Int32.Parse(num)) > newQuestionNameInt)
            {
                newQuestionNameInt = (Int32.Parse(num));
            }
        }

        //Create question name:
        newQuestionNameInt += 1;
        newQuestionName = selectedDataBase + "/" + newQuestionNameInt + ".txt";

        //Fill content of question:
        content += correctAnswersCode;
        content += "\r\n";
        content += question.text;

        for (int i = 0; i < answers.Length; i++)
        {
            if (answers[i].text != "")
            {
                content += "\r\n";
                content += answers[i].text;
            }
        }

        System.IO.File.WriteAllText(newQuestionName, OldFormat.TryParse(content).IntoJson());
        Application.LoadLevel("LocalQBExplorator");
    }

}
