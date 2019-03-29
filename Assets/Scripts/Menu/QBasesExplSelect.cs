using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

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
