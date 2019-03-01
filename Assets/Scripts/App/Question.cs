using UnityEngine;
using UnityEngine.UI;

// Klasa operuje na pytaniach, wykonywane są tu podstawowe operacje programu takie jak:
// tworzenie pytań, sprawdzanie poprawności udzielonych odp., usuwanie starych pytań
public class Question : MonoBehaviour {

    const string ERROR_MESSAGE_FILE_TO_SHORT = "Uwaga!\nPlik zawierał tylko jedna linijke danych.\nZobacz jak tworzyć pytania w zakladce 'Pomoc'.";
    const string ERROR_MESSAGE_WRONG_FORMAT = "Uwaga!\nPytanie powinno zaczynać się od 'X'.\nZobacz jak tworzyć pytania w zakładce 'Pomoc'.";
    private int anwsers;        // liczba odpowiedzi
    private string text;        // treść pytania
    private Spawn spawn;        // obiekt do spawnowania guzików
    private Check check;        // obiekt na guziku 'Sprawdź', słóży do ustawiania tekstu
    public void Setup(){
        spawn = Camera.main.GetComponent<Spawn>();
        check = GameObject.FindGameObjectWithTag("Check").GetComponent<Check>();
    }
 
    // ============================== Tworzenie Pytań ==============================
    // Metoda tworzy pytanie,
    // robi substringi z wczytanego stringa oraz odpowiednio je dzieli,
    // nastepnie tworzy obiekt typu Ans który jest strukturą podobną do klasy Anwser 
    // ale nie dziedziczy z MonoBehaviour co pozwala na korzystanie z konstruktora 
    // i za każdym razem stworzenie nowego obiektu
    // na koniec wywołuję metody 'Randomize()' i 'Spawn()'
    public void InitQuestion(string read)
    {
        var cut = read.Split('\n');
        if (cut[0] != string.Empty && cut[0][0] == 'X')
        {
            if (cut.Length <= 1) // jezeli w pliku jest tylko jedna linijka
            {
                ErrorOccured(ERROR_MESSAGE_FILE_TO_SHORT);
                return;
            }
            text = cut[1];
            SetQuestionLabel(text);     // wyswietl pytanie
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

            Ans [] ans = new Ans[anwsers];
            for (int i = 0; i < anwsers; i++)
            {
                if (cut[0].Length > i + 1)
                    ans[i] = new Ans(CharToBool(cut[0][i + 1]), cut[i + 2]);
                else //jezeli nie jest jasno okreslona poprawnosc odpowiedzi
                    ans[i] = new Ans(false, cut[i + 2]); //stwierdz ze jest ona nie poprawna
            }

            ans = Randomize(ans);
            spawn.SpawnAnwsers(ans);
        }
        else
        {
            ErrorOccured(ERROR_MESSAGE_WRONG_FORMAT);
        }
    }
    // Metoda odpowiednie konwertuje chara na boola
    private bool CharToBool(char i)
    {
        return i == '1';
    }

    private void ErrorOccured(string message) {
        SetQuestionLabel(message);
        check.SetText("Ok");
    }

    // Metoda zmienia kolejność pytań na losową
    // IN: tablica z wczytanąa kolejnością pytań
    // OUT: tablica z losową kolejnością pytań
    public Ans[] Randomize(Ans[] ans)
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
        return ans;
    }
    // =============================================================================

    // =========================== Napisy na górnym pasku ===========================
    // Metoda słóży do nadpisania zmiennej text, która jest wyświetlana w pasku pytania 
    public void SetQuestionValue(string t){
        text = t;
        SetQuestionLabel(text);
    }
    // Metoda wpisuje text pytania gdy chcesz przywrócić treść zmiennej 'text'
    public void ResetQuestionValue()
    {
        SetQuestionLabel(text);
    }
    // Metoda edytuje tekst na guziku odpowiedzi - zmiana chwilowa
    public void SetQuestionLabel(string s)
    {
        gameObject.GetComponent<LogControl>().Set(s);
    }
    // ==============================================================================

    // ========================== Czyszczenie ===========================
    // Metoda usuwa guziki, wywoływana jest gdy użytkownik chce
    // przejść do następnego pytania,
    public void Clear()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Anwser");
        foreach(GameObject t in temp)
        {
            Destroy(t.gameObject);
        }
    }
    // Włącz/Wyłącz wszystkie guziki w scenie
    public void Turn(bool b)
    {
        check.GetComponent<Button>().enabled = b;
        gameObject.GetComponent<Button>().enabled = b;

        GameObject[] temp = GameObject.FindGameObjectsWithTag("Anwser");
        foreach (GameObject t in temp)
        {
            t.GetComponent<Button>().enabled = b;
        }
    }
    // ==================================================================
}
