using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	public void Study()
    {
        Application.LoadLevel("App");
    }
    public void Test()
    {
        Application.LoadLevel("Test");
    }
    public void MainMenu()
    {
        Application.LoadLevel("Menu");
    }
    public void Help()
    {
        Application.LoadLevel("Help");
    }
    public void Credits()
    {
        Application.LoadLevel("Credits");
    }
}
