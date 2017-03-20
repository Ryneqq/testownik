using UnityEngine;
using System.Collections;

public class Load : MonoBehaviour
{
    TextAsset loaded;
    // Funkcja liczy ile pytan jest w bazie, slozy do z
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
    // Otworz plik o zadanej nazwie i zwroc string z zawartoscia pliku
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
}
