using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Obiekt do obsługi bazy pytań, 
/// zajmuje się koordynacją zadawania pytań, 
/// to serce programu.
/// </summary>
public class Base : MonoBehaviour
{
    private List<string> baseQ = new List<string>();    // w liście przetrzymuje tyle tablic Q ile mam ustawionych powtórzen
    private int repetitions = 3;                        // powtórzenia
    private int questions = 0;                          // liczba pytan w bazie
    private Question question;                          // obiekt, którym ustawiamy text, przsyłemy pytania etc.
    private Progress progress;                          // obiekt do wyświetlania stosunku poprawnych odpowiedzi
    private LogControl check;                           // tekst na guziku 'sprawdz'
    private bool anwsered = false;                      // bool sprawdzający czy odpowiedziano na pytanie
    private bool loading = false;                       // bool sprawdza czy wczytujemy dane

    void Awake()
    {
        // wyczyść zmienne statyczne
        Variables.Clear();

        question = GameObject.FindGameObjectWithTag("Question").GetComponent<Question>();
        check = GameObject.FindGameObjectWithTag("Check").GetComponent<LogControl>();
        progress = gameObject.GetComponent<Progress>();

        gameObject.GetComponent<Load>().Setup();

        // sprawdź czy istnieje save
        if(gameObject.GetComponent<Load>().CheckForSave()){
            LoadSave();
        } else {
            LoadBase();
        }
    }
    private void LoadSave(){
            loading = true;
            question.Set("Czy chcesz wczytać zapis?");
            question.SetText();
            question.SpawnYesNo();
            check.Set("Ok");
    }
    private void LoadBase(){
        questions = gameObject.GetComponent<Load>().Count();
        if (questions > 0)
        {
            InitBase();
            SetQuestion();
        } else {
            question.SetText("Brak pytan!\nUmiesc baze pytan w odpowiednim miejscu na swoim urzadzeniu.");
            check.Set("Ok");
        }
    }
    // ====================================== Inicjalizacja ======================================
    public string [] SetQueue(string[] Q)
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
        return Q;
    }
    public void InitBase()
    {
        string [] Q = new string[questions];
        for (int i = 1; i <= questions; i++)
        {
            if (i < 10)
                Q[i - 1] = "00" + i.ToString();
            else if (i < 100)
                Q[i - 1]= "0" + i.ToString();
            else
                Q[i - 1]= i.ToString();
        }

        for (int i = 0; i < repetitions; i++)
        {
            Q = SetQueue(Q); // ustawiamy kolejke tyle razy ile jest powtorzen

            for (int j = 0; j < questions; j++)
            {
                baseQ.Add(Q[j]);
            }
            // zaleta tego rozwiazania jest kolejne sciaganie z listy i nie bawienie sie indeksowaniem
        }
    }
    public void SetText(string s)
    {
        gameObject.GetComponent<LogControl>().Set(s); // wyświetlanie nazwy pliku
    }
    public int Qs()
    {
        return baseQ.Count; // zwraca ilosc pytań na liście
    }
    // ===========================================================================================

    // ====================================== Obsługa guzika ======================================
    // metoda wywoływana przez 'Ask()'
    // wywołuje metodę 'Check()' obiektu 'Question' która sprawdza poprawność zazn. odp.
    // jeżeli odpowiedzi były poprawne, usuwa pytanie z listy, jeżeli nie
    // to zostanie zadane to samo pytanie
    public void SetCheckText(string text){
        check.Set(text);
    }
    public void Checked()
    {
        //sprawdz czy odpowiedzi byly poprawne
        if (question.Check() && !Learned())
        {
            Variables.correct++;
            baseQ.RemoveAt(Qs() - 1);
        }
        Variables.anwsered++;
        // wywołaj liczenie stosunku poprawnych odpowiedzi
        progress.CountPercentage();
    }
    public void CheckYesNo(){
         if(question.IsSave()){
            if (question.Check())
                {
                    gameObject.GetComponent<Save>().SaveProgress(baseQ);
                }
                Application.LoadLevel("Menu");
        } else {
            if(question.Check())
            {
                baseQ = gameObject.GetComponent<Load>().LoadSave();
            } else {
                LoadBase();
            }
            loading = false;
            check.Set("Sprawdź");
            NewQuestion();
        }
    }
    // guzik 'sprawdz' wywoluje te metode
    public void Ask()
    {
        // kliknieto 'sprawdz'
        if (!anwsered && !question.IsSave() && !loading) 
        {
            if(Learned()){
                Application.LoadLevel("Menu");
            } else {
                check.Set("Dalej");
                anwsered = true;
                Checked();          // Sprawdz odpowiedz
            }
        }
        else if (question.IsSave() || loading)
        {
           CheckYesNo();
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
        question.Clear(); // wyczysc stare pytania
        if (!Learned()) // sprawdz czy pytania zostaly opanowane
        {
            SetQuestion();
        } else {
            //uzytkownik opanowal cala baze pytan
            string t = "Wszystkie pytania zostały opanowane";
            question.Set(t);
            question.SetText();
            check.Set("Ok!");
            SetText("fin");
            GameObject.Find("Piwo").GetComponent<Image>().enabled = true;
            anwsered = false;
        }
    }
    // wywołuje metode obiektu Load i inizcjalizuje pytanie w obiekcie 'Question'
    public void SetQuestion()
    {
        // przeczytaj ostatnie pytanie znajdujace sie na Liscie
        string read = gameObject.GetComponent<Load>().Read(baseQ[Qs() - 1]);

        // wyswietl nazwe otwartego pliku w prawym gornym rogu
        SetText(baseQ[Qs() - 1] + ".txt");
        question.InitQuestion(read);   // przkaz pytanie do obiektu 'Question'
        anwsered = false;
    }
    // sprawdza czy pozostały jakieś pytania na liście pytań 'baseQ'
    public bool Learned()
    {
        if (Qs() > 0)
            return false;
        return true;
    }
    // ===========================================================================================
}