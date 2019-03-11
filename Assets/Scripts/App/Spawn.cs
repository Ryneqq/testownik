using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour {

    public Button anwser;
    public Canvas GameCanvas;

    public void SpawnAnwsers(AnserDto[] ans)
    {
        float posY = 700.0f;
        foreach (AnserDto a in ans)
        {
            SpawnAnwser(a, posY);
            posY = posY - 400.0f;
        }
    }

    public void SpawnYesNo()
    {
        float posY = 700.0f;
        AnserDto a = new AnserDto(true, "Tak");
        SpawnAnwser(a,posY);

        posY = posY - 400.0f;
        a = new AnserDto(false, "Nie");
        SpawnAnwser(a, posY);
    }

    private void SpawnAnwser(AnserDto a, float pos)
    {
        Button temp;

        temp = (Button)Instantiate(anwser, new Vector3(0.0f, pos, 0.0f), Quaternion.identity);
        temp.GetComponent<Anwser>().SetAnwser(a.correct, a.text);
        temp.transform.SetParent(GameCanvas.transform, false);
    }
}
