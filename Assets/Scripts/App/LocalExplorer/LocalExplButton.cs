using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalExplButton : MonoBehaviour {
	[SerializeField]
	private Text fileName;
    [SerializeField]
    private LocalExplButtonControl buttonControl;

    private string fullFilePath;

	public void SetText(string textString)
	{
        fullFilePath = textString;

        fileName.text = textString.Remove(0, (Application.persistentDataPath + "/QuestionBases/").Length);
	}

	public void OnClick()
	{
        buttonControl.ButtonClicked(fullFilePath);
    }

}
