using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class Load
{
    private static string basePath;        // ścieżka do bazy na androidzie
    private static string savePath;        // ścieżka do pliku z savem

    public static void Setup()
    {
        basePath = Application.persistentDataPath + "/baza/";
        savePath = Application.persistentDataPath + "/save.txt";

        Debug.Log("Here: '" + basePath + "', put your folder 'baza' with question inside it");

        // TODO: Remove this crapy code...
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

    public static string[] ReadPath()
    {
        return System.IO.Directory.GetFiles(basePath);
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

    private static List<string> StringToList(string read)
    {
        List<string> baseQ = new List<string>();
        string[] contents = read.Split('|');
        for(int i = 0; i < contents.Length; i++)
        {
            baseQ.Add(contents[i]);
        }
        return baseQ;
    }

    public static List<string> LoadSave()
    {
        List<string> baseQ = new List<string>();
        string read = System.IO.File.ReadAllText(savePath);
        if (read != null)
        {
            baseQ = StringToList(read);
        }
        return baseQ;
    }
}
