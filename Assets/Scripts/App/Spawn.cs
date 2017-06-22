using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour {

	public Button anwser;       // guzik z odpowiedzią
    public Canvas GameCanvas;   // UI canvas w którym są guziki

	private void SpawnAnwser(Ans a, float pos)
    {
        Button temp;

        temp = (Button)Instantiate(anwser, new Vector3(0.0f, pos, 0.0f), Quaternion.identity);
        temp.GetComponent<Anwser>().SetAnwser(a.correct, a.text);
        temp.transform.SetParent(GameCanvas.transform, false);
    }
    // Metoda wywołuje SpawnOne na podstawie tablicy odpowiedzi
    public void SpawnAnwsers(Ans[] ans)
    {
        float posY = 700.0f;
        foreach (Ans a in ans)
        {
            SpawnAnwser(a, posY);
            posY = posY - 400.0f;
        }
    }
    // Metoda spawnuje guziki 'Tak', 'Nie' służące do interakcji z użytkownikiem
    public void SpawnYesNo()
    {
        float posY = 700.0f;
        Ans a = new Ans(true, "Tak");
        SpawnAnwser(a,posY);

        posY = posY - 400.0f;
        a = new Ans(false, "Nie");
        SpawnAnwser(a, posY);
        GameObject.FindGameObjectWithTag("Check").GetComponent<Check>().SetText("Ok");
    }
}
