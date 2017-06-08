using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <Save>
/// Metoda pozwala na zapisanie aktualnej bazy do pliku tekstowego
/// nie jest ona kompatybilna z testownikiem na komputery
/// progress jest tylko na telefonie
/// </Save>
public class Save : MonoBehaviour {
    private string path;
    private string fileName = "save.txt";

    void Start()
    {
        path = Application.persistentDataPath + "/" + fileName; // android
    }

    private string ListToString(List<string> baseQ)
    {
        string contents = string.Empty;

        for (int i = 0; i < baseQ.Count; i++)
        {
            if(i!=0)
                contents +="|";
            contents += baseQ[i];
            
        }
        return contents;
    }
    
    public void SaveProgress(List<string> baseQ)
    {
        string contents = ListToString(baseQ);

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }

        using (StreamWriter file = new StreamWriter(path, true))
        {
            file.Write(contents);
            file.Close();
        }
    }
}
