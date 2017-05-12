using UnityEngine;
using System.Collections.Generic;


// TODO:
// - dodać wywswietlanie stosunku poprawnych odpowiedzi
// - wyswietlic czas
// - save
// - zastanow sie co jezeli uzytkownik usunie pytania w trakcie dzialania programu

// Obiekt do obsługi bazy pytań, zajmuje się koordynacją zadawania pytań, serce programu
public class Base : MonoBehaviour
{
    private string[] Q;                                 // tablica w ktorej generuje pytania i ustawiam losową kolejność
    private List<string> baseQ = new List<string>();    // w liście przetrzymuje tyle tablic Q ile mam ustawionych powtórzen
    private int repetitions = 1;                        // powtórzenia

    private int questions = 0;                          // liczba pytan w bazie
    private string question;                            // przeczytane pytanie z pliku: pytanie + odpowiedzi + poprawność odp.

    private Question q;                                 // obiekt, który otrzymuje wczytane pytanie
    private LogControl check;                           // tekst na guziku 'sprawdz'

    private bool anwsered = false;


    void Awake()
    {
        // wyczyść zmienne statyczne
        Variables.Clear();

        // załaduj obiekty, żeby korzystać z ich metod
        q = GameObject.FindGameObjectWithTag("Question").GetComponent<Question>();
        check = GameObject.FindGameObjectWithTag("Check").GetComponent<LogControl>();

        // policz pytania znajdujące się w folderze baza
        questions = gameObject.GetComponent<Load>().CountQ();

        if (questions > 0)
        {
            InitBase();
            SetQuestion();
        }
        else
        {
            q.SetText("Brak pytan!\nUmiesc baze pytan w odpowiednim miejscu na swoim urzadzeniu.");
        }
    }
    void Update()
    {
        Variables.studyTime += Time.deltaTime; // licz czas nauki
    }

    // ====================================== Inicjalizacja ======================================
    // losowe ustawienie kolejnosci pytan
    public void SetQueue()
    {
        int rand;
        string temp;
        for (int i = 0; i < questions - 1; i++)
        {
            rand = Random.Range(i, questions);
            temp = Q[i];
            Q[i] = Q[rand];
            Q[rand] = temp;
        }
    }
    // stworzenie listy pytan
    // wywoluje metode SetQueue()
    public void InitBase()
    {
        Q = new string[questions];
        for (int i = 1; i <= questions; i++)
        {
            if (i < 10)
                Q[i - 1] = "00" + i.ToString();
            else if (i < 100)
                Q[i - 1]= "0" + i.ToString();
            else
                Q[i - 1]= i.ToString();

        }
        // ta czesc ma trzymac cala liste w kontenerze List
        for (int i = 0; i < repetitions; i++)
        {
            SetQueue(); // ustawiamy kolejke tyle razy ile jest powtorzen
            // po kazdym ustawieniu dodajemy nasza kolejnosc do listy
            for (int j = 0; j < questions; j++)
            {
                baseQ.Add(Q[j]);
            }
            // zaleta tego rozwiazania jest kolejne sciaganie z listy i nie bawienie sie indeksowaniem
        }
    }
    // wywoływana do wyświetlenia nazwy pytania w bazie
    public void SetText(string s)
    {
        gameObject.GetComponent<LogControl>().Set(s);
    }
    // ===========================================================================================

    // ====================================== Obsługa guzika ======================================
    // metoda wywoływana przez 'Ask()'
    // wywołuje metodę 'Check()' obiektu 'Question' która sprawdza poprawność zazn. odp.
    // jeżeli odpowiedzi były poprawne, usuwa pytanie z listy, jeżeli nie
    // to zostanie zadane to samo pytanie
    public void Checked()
    {
        //sprawdz czy odpowiedzi byly poprawne
        if (q.Check() && baseQ.Count > 0)
        {
            Variables.correct++;
            baseQ.RemoveAt(baseQ.Count - 1);
        }
        Variables.anwsered++;
    }
    // guzik 'sprawdz' wywoluje te metode
    public void Ask()
    {
        // kliknieto 'sprawdz'
        if (!anwsered) 
        {
            check.Set("Dalej"); // zmiana napisu na guziku
            anwsered = true;
            Checked();          // uzytkownik zdecydowal sie sprawdzic swoja odpowiedz
        }
        // kliknieto 'dalej'
        else
        {
            check.Set("Sprawdź"); // zmiana napisu na guziku
            NewQuestion();
        }
    }
    // ============================================================================================

    // ====================================== Obsługa pytań ======================================
    // metoda wywoływana przez naklikniecie guzika 'dalej' w metodzie 'Ask()'
    // sprawdza za pomocą metody Learned() czy zostały opanowane wszystkie pytania,
    // jeżeli nie to wywołuje 'SetQuestion'
    public void NewQuestion()
    {
        q.Clear(); // wyczysc stare pytania
        if (!Learned()) // sprawdz czy pytania zostaly opanowane
        {
            // jezeli nie to zadaj kolejne pytanie
            SetQuestion();
        }
        else
        {
            //uzytkownik opanowal cala baze pytan
            q.SetText("Wszystkie pytania zostały opanowane");
            check.Set("Ok!");
            SetText("fin");
        }
    }
    // wywołuje metode obiektu Load i inizcjalizuje pytanie w obiekcie 'Question'
    public void SetQuestion()
    {
        // przeczytaj ostatnie pytanie znajdujace sie na Liscie
        question = gameObject.GetComponent<Load>().ReadQ(baseQ[baseQ.Count - 1]);

        //wyswietl nazwe otwartego pliku w prawym gornym rogu
        SetText(baseQ[baseQ.Count - 1] + ".txt");
        q.InitQuestion(question);   // przkaz pytanie do obiektu 'Question'
        anwsered = false;
    }
    // sprawdza czy pozostały jakieś pytania na liście pytań 'baseQ'
    public bool Learned()
    {
        if (baseQ.Count > 0)
            return false;
        return true;
    }
    // ===========================================================================================
}