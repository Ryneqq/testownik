using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LocalExplButtonControl : MonoBehaviour {

	[SerializeField]
	private GameObject fileTemplate;

    List<GameObject> fileList;

    string path;
    string baseName;
    Text QuestionText = null;


    void Start()
	{
        //Set text:
        Transform child = transform.Find("QuestionView");
        QuestionText = child.GetComponent<Text>();

        //Create list for buttons storage:
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Load bases list
        {

            //Clear question field
            QuestionText.text = "";

            if (path == Application.persistentDataPath + "/QuestionBases/")
            {
                SceneManager.LoadScene(4); //load Menu scene
            }
            else
            {
                path = Application.persistentDataPath + "/QuestionBases/";

                //Clear view:
                foreach (GameObject file in fileList)
                {
                    Destroy(file);
                }
                fileList.Clear();

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

        }
    }

    public void ButtonClicked(string fullPath)
    {
        //Check if user want to view question 
        if (fullPath.Remove(0, fullPath.Length - 4) == ".txt")
        {
            //Destroy all the elements
            foreach (GameObject file in fileList)
            {
                Destroy(file);
            }
            fileList.Clear();

            //Fill text field under Question list with data:
            QuestionText.text = File.ReadAllText(fullPath);
            return;
        }

        path = fullPath;
        //Clear view:
        foreach (GameObject file in fileList)
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
