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

        // zabezpieczenie przed wczytaniem save'a gdyby zmieniono bazy
        int qs = Count();
        if(!PlayerPrefs.HasKey("questions")){
            PlayerPrefs.SetInt("questions", qs);
        } else { 
             if(qs != PlayerPrefs.GetInt("questions")){
                //gameObject.GetComponent<Load>().DeleteSave();
                DeleteSave();
                PlayerPrefs.SetInt("questions", qs);
             }
        }
    }

    // ========== Ile pytań jest w bazie ==========
    public static int Count()
    {
        for (int i = 1; ; i++)
        {
            // Sprawdz czy istnieje plik o zadanej nazwie
            if (i < 10 && System.IO.File.Exists(basePath + "00" + i.ToString() + ".txt"))
            { }
            else if (i < 100 && System.IO.File.Exists(basePath + "0" + i.ToString() + ".txt"))
            { }
            else if (i >= 100 && System.IO.File.Exists(basePath + i.ToString() + ".txt"))
            { }
            else
                return i - 1;
        }
    }
    // ============================================

    // =============== Wczytaj plik ===============
    public static string Read(string name)
    {
        string path = basePath + name + ".txt";
        string read = System.IO.File.ReadAllText(path, Encoding.Default);

        if (read != null)
        {
            return read;
        }

        return string.Empty;
    }
    // ============================================

    // =============== Wczytaj Save =============== 
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
    // ============================================
}
