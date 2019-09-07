using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class QBasesExplSelect : MonoBehaviour {

	public void LocalBase()
    {
        Variables.basePath = Variables.localBase;
        Application.LoadLevel("LocalQBExplorator");
    }

	public void RemoteBase()
    {
        Variables.basePath = Variables.remoteBase;
        Application.LoadLevel("LocalQBExplorator");
    }
}
