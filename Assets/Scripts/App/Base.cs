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
    private Check check;                                // tekst na guziku 'sprawdz'

    void Awake()
    {
        // wyczyść zmienne statyczne
        Variables.Clear();

        question = GameObject.FindGameObjectWithTag("Question").GetComponent<Question>();
        check = GameObject.FindGameObjectWithTag("Check").GetComponent<Check>();
        progress = gameObject.GetComponent<Progress>();

        Load.Setup();
        Save.Setup();
        question.Setup();

        // sprawdź czy istnieje save
        if(Load.CheckForSave()){
            LoadSave();
        } else {
            LoadBase();
        }
    }
    private void LoadSave(){
            question.Set("Czy chcesz wczytać zapis?");
            question.SetText();
            GetComponent<Spawn>().SpawnYesNo();
            check.SetText("Ok");
            check.Loading();
    }
    public void LoadBase(){
        questions = Load.Count();
        if (questions > 0)
        {
            InitBase();
            SetQuestion();
        } else {
            question.SetText("Brak pytań!\nUmieść bazę pytań w odpowiednim miejscu na swoim urządzeniu.");
            check.SetText("Ok");
        }
    }
    public void LoadBase(List<string> b){
        baseQ = b;
    }
    public void SaveBase(){
        Save.SaveProgress(baseQ);
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

    public void SetText(string s) // to uważam, że powinno się znajdować w skrypcie Question
    {
        gameObject.GetComponent<LogControl>().Set(s); // wyświetlanie nazwy pliku
    }
    public int Qs()
    {
        return baseQ.Count; // zwraca ilosc pytań na liście
    }
    // ===========================================================================================
    
    // ====================================== Obsługa pytań ======================================
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
            check.SetText("Ok!");
            SetText("fin");
            GameObject.Find("Piwo").GetComponent<Image>().enabled = true;
        }
    }
    // wywołuje metode obiektu Load i inizcjalizuje pytanie w obiekcie 'Question'
    public void SetQuestion()
    {
        // przeczytaj ostatnie pytanie znajdujace sie na Liscie
        string read = Load.Read(baseQ[Qs() - 1]);

        // wyswietl nazwe otwartego pliku w prawym gornym rogu
        SetText(baseQ[Qs() - 1] + ".txt");
        question.InitQuestion(read);   // przkaz pytanie do obiektu 'Question'
    }

    public void RemoveQuestion(){
        baseQ.RemoveAt(Qs() - 1);
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