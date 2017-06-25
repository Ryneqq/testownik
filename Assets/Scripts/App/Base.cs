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
    private Question question;                          // obiekt, którym ustawiamy text, przsyłemy pytania etc.
    private Check check;                                // tekst na guziku 'sprawdz'

    void Awake()
    {
        // Wyczyść zmienne statyczne, tam znajduje się czas nauki, ilość poprawnych odpowiedzi etc.
        Variables.Clear();

        question = GameObject.FindGameObjectWithTag("Question").GetComponent<Question>();
        check = GameObject.FindGameObjectWithTag("Check").GetComponent<Check>();

        Load.Setup();
        Save.Setup();
        question.Setup();

        // Sprawdź czy istnieje plik 'save.txt'
        if(Load.CheckForSave()){
            question.Set("Czy chcesz wczytać zapis?");
            question.SetText();
            check.SetText("Ok");
            check.Loading();
            GetComponent<Spawn>().SpawnYesNo();
        } else {
            LoadBase();
        }
    }
    // ===================================== Wczytanie bazy ===================================== 
    public void LoadBase()
    {
        int questions = Load.Count();
        if (questions > 0){
            InitBase(questions);
            SetQuestion();
        } else {
            question.SetText("Brak pytań!\nUmieść bazę pytań w odpowiednim miejscu na swoim urządzeniu.");
            check.SetText("Ok");
        }
    }
    public void LoadBase(List<string> b)
    {
        baseQ = b;
    }
    // -------------------------------------- Inicjalizacja ------------------------------------- 
    public string [] SetQueue(string[] Q)
    {
        int questions = Q.Length;
        int rand;
        string temp;
        for (int i = 0; i < questions - 1; i++){
            rand = Random.Range(i, questions);
            temp = Q[i];
            Q[i] = Q[rand];
            Q[rand] = temp;
        }
        return Q;
    }
    public void InitBase(int questions)
    {
        string [] Q = new string[questions];
        for (int i = 1; i <= questions; i++){
            if (i < 10)
                Q[i - 1] = "00" + i.ToString();
            else if (i < 100)
                Q[i - 1]= "0" + i.ToString();
            else
                Q[i - 1]= i.ToString();
        }
        // Ustawiamy losową kolejność tyle razy ile jest powtórzeń
        for (int i = 0; i < repetitions; i++){
            Q = SetQueue(Q);

            for (int j = 0; j < questions; j++){
                baseQ.Add(Q[j]);
            }
        }
    }
    // ===========================================================================================

    // ====================================== Zapisanie bazy =====================================
    // Metoda korzysta z obiektu statycznego Save, którego wykorzystuje do zapisania bazy do pliku
    public void SaveBase()
    {
        Save.SaveProgress(baseQ);
    }
    // ===========================================================================================
    
    // ====================================== Obsługa pytań ======================================
    public void NewQuestion()
    {
        question.Clear();
        if (!Learned()){
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
    // Metoda wywołuje ze statycznego obiektu Load metodę do wczytania pliku (kolejnego pytania)
    public void SetQuestion()
    {
        // Wczytaj ostatnie pytanie.
        string read = Load.Read(baseQ[Qs() - 1]);
        // Wyświetl nazwe otwartego pliku w prawym gornym rogu.
        SetText(baseQ[Qs() - 1] + ".txt");
        // Przekaż pytanie do obiektu 'Question'.
        question.InitQuestion(read);
    }
    // Metoda do usuwania ostatniego pytania z listy
    public void RemoveQuestion()
    {
        baseQ.RemoveAt(Qs() - 1);
    }
    // Metoda sprawdza czy pozostały jakieś pytania na liście pytań 'baseQ'
    public bool Learned()
    {
        if (Qs() > 0)
            return false;
        return true;
    }
    // Metoda służy do wyświetlania nazwy aktualnie wczytanego pliku (pytania)
    public void SetText(string s)
    {
        gameObject.GetComponent<LogControl>().Set(s);
    }
    // Metoda zwraca ilosc pytań na liście
    public int Qs()
    {
        return baseQ.Count; 
    }
    // ===========================================================================================
}