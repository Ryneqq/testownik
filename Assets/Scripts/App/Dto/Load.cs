﻿using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Load
{
    private static string basePath;        // ścieżka do bazy na androidzie
    private static string savePath;        // ścieżka do pliku z savem

    public static void Setup()
    {
        basePath = PlayerPrefs.GetString(Variables.LearningBaseKey);

        savePath = Application.persistentDataPath + "/save.txt";

        Debug.Log("Here: '" + basePath + "', put your folder 'baza' with question inside it");

        //If the base is not set, then load local bases explorator:
        if(basePath == "")
        {
            SceneManager.LoadScene(6);
        }

        // Tu mozna wygenerowac hasha z nazw plikow w celu zabezpieczenia save'a

        // zabezpieczenie przed wczytaniem save'a gdyby zmieniono bazy
        int qs = Count();
        if(!PlayerPrefs.HasKey("questions")){
            PlayerPrefs.SetInt("questions", qs);
        } else {
             if(qs != PlayerPrefs.GetInt("questions")){
                DeleteSave();
                PlayerPrefs.SetInt("questions", qs);
             }
        }
    }

    public static int Count()
    {
        return System.IO.Directory.GetFiles(basePath).Length;
    }

    public static string Read(string path)
    {
        string read = System.IO.File.ReadAllText(path, Encoding.Default);

        if (read != null)
        {
            return read;
        }

        return string.Empty;
    }

    public static string[] ReadBase()
    {
        return ReadFiles(basePath);
    }

    public static string[] ReadFiles(string path)
    {
        return System.IO.Directory.GetFiles(path);
    }

    public static string[] ReadDirectories(string path)
    {
        return System.IO.Directory.GetDirectories(path);
    }

    private static void DeleteSave(){
        if(System.IO.File.Exists(savePath)){
            System.IO.File.Delete(savePath);
        }
    }

    public static bool CheckForSave(){
        if(System.IO.File.Exists(savePath)){
            return true;
        }
        return false;
    }

    public static QuestionDto[] LoadSave() {
        string read = System.IO.File.ReadAllText(savePath);

        return BaseDto.FromJson(read).json_db;
    }
}
