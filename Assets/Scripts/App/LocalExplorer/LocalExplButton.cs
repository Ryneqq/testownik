using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LocalExplButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    const string LearningBaseKey = "LearningBase";

    [SerializeField]
	private Text fileName;
    [SerializeField]
    private LocalExplButtonControl buttonControl;

    public string fullFilePath;

    //Button reactions:
    private bool buttonDown = false;
    private long bottonDownTimer = 0;

	public void SetText(string textString)
	{
        fullFilePath = textString;
        fileName.text = textString.Remove(0, (Application.persistentDataPath + "/QuestionBases/").Length);
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp for: " + bottonDownTimer);

        //Normal click: go forward
        if (bottonDownTimer < 100 && buttonDown)
        {
            buttonControl.ButtonClicked(fullFilePath);
            Reset();
        }
    }


    private void Start()
    {    
    }

    private void Update()
    {
        if (buttonDown)
        {
            bottonDownTimer += 1;

            //After 2 seconds set base as the one to learn from(if it is the base!!!):
            if (bottonDownTimer > 100 && fullFilePath.Remove(0, fullFilePath.Length - 4) != ".txt")
            {
                SSTools.ShowMessage("Wybrano baze do nauki!", SSTools.Position.bottom, SSTools.Time.threeSecond);
                PlayerPrefs.SetString(LearningBaseKey, fullFilePath);
                buttonControl.UpdateColors(fullFilePath);
                Reset();
            }
        }
    }

    private void Reset()
    {
        buttonDown = false;
        bottonDownTimer = 0;
    }
    
}
