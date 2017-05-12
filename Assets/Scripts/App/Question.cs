using UnityEngine;
using UnityEngine.UI;

// Struktura symboliczna
// analogiczna do klasy Anwser
// nie dziedziczy po MonoBehaviour w przeciwięstniwie do Anwser
// dzięki czemu można korzystać z konstruktora
public struct Ans
{
    public bool correct;
    public string text;

    public Ans(bool c, string t)
    {
        text = t;
        correct = c;
    }
};

// Klasa operuje na pytaniach, wykonywane są tu podstawowe operacje programu takie jak:
// tworzenie pytań, sprawdzanie poprawności udzielonych odp., usuwanie starych pytań
public class Question : MonoBehaviour {

    public Button anwser;       // guzik z odpowiedzią
    public Canvas GameCanvas;   // UI canvas w którym są guziki

    private Ans[] ans;          // tablica odpowiedzi, zawiera info o poprawności i treści
    private int anwsers;        // liczba odpowiedzi

    private string text;        // treść pytania
 

    // ============================== Tworzenie Pytań ==============================

    // Metoda tworzy pytanie,
    // robi substringi z wczytanego stringa oraz odpowiednio je dzieli,
    // nastepnie tworzy obiekt typu Ans który jest strukturą podobną do klasy Anwser 
    // ale nie dziedziczy z MonoBehaviour co pozwala na korzystanie z konstruktora
    // na koniec wywołuję metody 'Randomize()' i 'Spawn()'
    public void InitQuestion(string readed)
    {
        string[] cut = readed.Split('\n');
        if (cut[0] != string.Empty && cut[0][0] == 'X')
        {
            if (cut.Length <= 1) // jezeli w pliku jest tylko jedna linijka
            {
                SetText("Uwaga!\nPlik zawieral tylko jedna linijke danych.\nZobacz jak tworzyc pytania w zakladce 'Pomoc'.");
                return;
            }
            text = cut[1];
            SetText(text);     // wyswietl pytanie
            anwsers = cut.Length - 2; // liczba pytan w pliku

            //usun bledne koncowki (entery)
            for (int i = cut.Length - 1; i > 1; i--)
                if (cut[i] == string.Empty || cut[i][0] == '\r' || cut[i][0] == '\n')
                    anwsers--;
                else
                    break;

            //zabezpieczenie przed wprowadzeniem zbyt wielu pytan
            if (anwsers > 5)
                anwsers = 5;

            ans = new Ans[anwsers];
            for (int i = 0; i < anwsers; i++)
            {
                if (cut[0].Length > i + 1)
                    ans[i] = new Ans(CharToBool(cut[0][i + 1]), cut[i + 2]);
                else //jezeli nie jest jasno okreslona poprawnosc odpowiedzi
                    ans[i] = new Ans(false, cut[i + 2]); //stwierdz ze jest ona nie poprawna
            }

            Randomize();
            Spawn();
        }
        else
        {
            SetText("Uwaga!\nPytanie powinno zaczynac sie od 'X'.\nZobacz jak tworzyc pytania w zakladce 'Pomoc'.");
        }
    }
    // Metoda tworzy guziki z prefaba
    public void Spawn()
    {
        float posY = 650.0f;
        Button temp;
        foreach (Ans a in ans)
        {
            temp = (Button)Instantiate(anwser, new Vector3(0.0f, posY, 0.0f), Quaternion.identity);
            temp.GetComponent<Anwser>().SetAnwser(a.correct, a.text);
            temp.transform.SetParent(GameCanvas.transform, false);
            posY = posY - 400.0f;
        }
    }
    // Metoda zmienia kolejność pytań na losową
    public void Randomize()
    {
        Ans temp;
        int k;
        for (int i = 0; i < anwsers - 1; i++)
        {
            k = Random.Range(1, anwsers);
            temp = ans[i];
            ans[i] = ans[k];
            ans[k] = temp;

        }

    }
    // Metoda edytuje tekst na guziku odpowiedzi
    public void SetText(string s)
    {
        gameObject.GetComponent<LogControl>().Set(s);
    }
    // Metoda zamienia dane o poprawności z chara na boola
    // wykorzystywana przez 'InitQuestion()'
    private bool CharToBool(char i)
    {
        if (i == '1')
            return true;
        else
            return false;
    }

    // =============================================================================

    // ==================== Czyszczenie i poprawność ====================

    // Metoda sprawdza poprawność zaznaczonych pytań,
    // odpowiednio ustawia kolor zaznaczonych guzików,
    // oraz blokuje możliwość ich naciśnięcia po wykonaniu metody
    public bool Check()
    {
        GameObject [] ansList = GameObject.FindGameObjectsWithTag("Anwser");
        bool correct = true;
        foreach(GameObject a in ansList)
        {
            
            if (a.GetComponent<Anwser>().Correctness() < 3)
            {
                correct = false; // zła odpowiedź
                if (a.GetComponent<Anwser>().Correctness() == 1)
                    a.GetComponent<Image>().color = Color.green;
                if (a.GetComponent<Anwser>().Correctness() == 2)
                    a.GetComponent<Image>().color = Color.red;
            } else { 
                if (a.GetComponent<Anwser>().Correctness() == 3)
                    a.GetComponent<Image>().color = Color.green;
            }
            a.GetComponent<Button>().enabled = false;
        }
        // gdy wszystkie odpowiedzi są zaznaczone poprawnie to zwróci prawdę,
        // w każdym innym przypadku będzie false.
        return correct;
    }
    // Metoda usuwa guziki, wywoływana jest gdy użytkownik chce
    // przejść do następnego pytania
    public void Clear()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Anwser");
        foreach(GameObject t in temp)
        {
            Destroy(t.gameObject);
        }
    }

    // ==================================================================
}
