using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalExplButtonControl : MonoBehaviour {

	[SerializeField]
	private GameObject fileTemplate;

    List<GameObject> fileList;

    string path;

    void Start()
	{

        fileList = new List<GameObject>();

        path = Application.persistentDataPath + "/QuestionBases/";

        if (!System.IO.Directory.Exists(path))
        {
            try
            {
                System.IO.Directory.CreateDirectory(path);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }

        try
        {
            string[] fileNames = System.IO.Directory.GetDirectories(path);
            for (int k = 0; k < fileNames.Length; k++)
            {
                GameObject Butt = Instantiate(fileTemplate) as GameObject;
                
                Butt.SetActive(true);
                Butt.GetComponent<LocalExplButton>().SetText(fileNames[k]);

                fileList.Add(Butt);

                //Set parent of new button that we just created (Butt) to be the child object of 
                //fileTemplate
                Butt.transform.SetParent(fileTemplate.transform.parent, false);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }

    public void ButtonClicked(string fullPath)
    {
        path = fullPath;
        //Clear view:
        foreach(GameObject file in fileList)
        {
            Destroy(file);
        }
        fileList.Clear();

        //View all txt files inside database
        try
        {
            string[] fileNames = System.IO.Directory.GetFiles(path);
            for (int k = 0; k < fileNames.Length; k++)
            {
                GameObject Butt = Instantiate(fileTemplate) as GameObject;

                Butt.SetActive(true);
                Butt.GetComponent<LocalExplButton>().SetText(fileNames[k]);

                fileList.Add(Butt);

                //Set parent of new button that we just created (Butt) to be the child object of 
                //fileTemplate
                Butt.transform.SetParent(fileTemplate.transform.parent, false);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}
