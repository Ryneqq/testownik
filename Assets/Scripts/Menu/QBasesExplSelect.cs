using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QBasesExplSelect : MonoBehaviour {
	
	public void LocalBase()
    {
        Application.LoadLevel("LocalQBExplorator");
    }
	
	public void RemoteBase()
    {
        Application.LoadLevel("RemoteQBExplorator");
    }
}
