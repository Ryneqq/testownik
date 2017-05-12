using UnityEngine;

public class Load : MonoBehaviour
{
    TextAsset loaded;       // wczytane dane
    string basePath;        // ścieżka do bazy na androidzie

    void Start()
    {
        basePath = Application.persistentDataPath + "/baza/";
    }

    // ========== Ile pytań jest w bazie ==========
    // o zadanej ścieżce - na androida
    public int CountQpath()
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
    // z folderu Resources - do testowania
    public int CountQ()
    {
        for (int i = 1; ; i++)
        {
            // Otworz plik o zadanej nazwie
            if(i < 10) 
                loaded = Resources.Load<TextAsset>("00" + i.ToString());
            else if (i < 100)
                loaded = Resources.Load<TextAsset>("0" + i.ToString());
            else
                loaded = Resources.Load<TextAsset>(i.ToString());
            // Jezeli jest pusty to zwroc liczbe otwartych plikow
            if (loaded == null)
                return i - 1;
        }
    }
    // ============================================
    // =============== Wczytaj plik ===============
    // o zadanej ścieżce - na androida
    public string ReadQpath(string name)
    {
        string readed = System.IO.File.ReadAllText(basePath + name + ".txt");
        if (readed != null)
        {
            return readed;
        }
        // Jezeli plik jest pusty to zwroc pusty string
        return string.Empty;
    }
    // z folderu Resources - do testowania
    public string ReadQ(string name)
    {
        loaded = Resources.Load<TextAsset>(name);
        if (loaded != null)
        {
            return loaded.text;
        }
        // Jezeli plik jest pusty to zwroc pusty string
        return string.Empty;
    }
    // ============================================
}
