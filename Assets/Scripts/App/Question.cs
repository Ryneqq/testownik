using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Struktura symboliczna do klasy Anwser
public struct Ans
{
    public bool correct;
    public string text;

    public void Set(bool c, string t)
    {
        text = t;
        correct = c;
    }
};

public class Question : MonoBehaviour {
    public Button anwser;
    public Canvas GameCanvas;
    private Ans[] ans;
    private string text;
    public bool Anwsered = false;

    private bool CharToBool(char i)
    {
        if (i == '1')
            return true;
        else
            return false;
    }
    public void InitQuestion(string readed)
    {
        int qs;
        string[] cut = readed.Split('\n');
        if (cut[0] != string.Empty && cut[0][0] == 'X')
        {
            if(cut.Length <= 1) // jezeli w pliku jest tylko jedna linijka
            {
                gameObject.GetComponent<LogControl>().Set("Uwaga!\nPlik zawieral tylko jedna linijke danych.\nZobacz jak tworzyc pytania w zakladce 'Pomoc'.");
                return;
            }
            gameObject.GetComponent<LogControl>().Set(cut[1]);
            qs = cut.Length - 2; //liczba pytan w pliku

            //usun bledne koncowki (entery)
            for (int i = cut.Length - 1; i > 1; i--)
                if (cut[i] == string.Empty || cut[i][0] == '\r' || cut[i][0] == '\n')
                    qs--;
                else
                    break;
            
            //zabezpieczenie przed wprowadzeniem zbyt wielu pytan
            if (qs > 5)
                qs = 5;

            ans = new Ans[qs];
            for(int i=0; i<qs; i++)
            {
                if (cut[0].Length > i+1)
                    ans[i].Set(CharToBool(cut[0][i + 1]), cut[i + 2]);
                else //jezeli nie jest jasno okreslona poprawnosc odpowiedzi
                    ans[i].Set(false, cut[i + 2]); //stwierdz ze jest ona nie poprawna
            }
            Spawn();
        } else {
            gameObject.GetComponent<LogControl>().Set("Uwaga!\nPytanie powinno zaczynac sie od 'X'.\nZobacz jak tworzyc pytania w zakladce 'Pomoc'.");
        }
    }
    public void Spawn()
    {
        float posY = 650.0f;
        Button temp;

        foreach (Ans a in ans)
        {
            temp = (Button)Instantiate(anwser, new Vector3(0.0f, posY, 0.0f), Quaternion.identity);
            temp.GetComponent<Anwser>().SetAnwser(a.correct, a.text);
            temp.transform.SetParent(GameCanvas.transform, false);
            posY = posY - 400.0f ;
            Debug.Log(posY);
        }
    }
    public void SetAnwserText(int i, Anwser a)
    {
       // ansList[i] = a;
    }
    public void Check()
    {
        /*foreach(Anwser a in ansList)
        {
            // sprawdz czy zaznaczone są poprawne
        }*/
    }
    public void Clear()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Anwser");
        foreach(GameObject t in temp)
        {
            Destroy(t.gameObject);
        }
    }
}
