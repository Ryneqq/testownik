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

        //Save question file:
        System.IO.File.WriteAllText(newQuestionName, correctAnswersCode);
        System.IO.File.AppendAllText(newQuestionName, "\r\n");
        System.IO.File.AppendAllText(newQuestionName, question.text);
    
        for (int i = 0; i < answers.Length; i++)
        {
            if (answers[i].text != "")
            {
                System.IO.File.AppendAllText(newQuestionName, "\r\n");
                System.IO.File.AppendAllText(newQuestionName, answers[i].text);
            }
        }

        Application.LoadLevel("LocalQBExplorator");
    }

}
