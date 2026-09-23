using UnityEngine;

public class SaveManager : MonoBehaviour
{
   
    public ScooringSystem score;
    public TaskLoader taskLoader;
    [SerializeField]public GameObject resultsUI;


    private void Start()
    {
        if (score != null)
        {
            // OCEAN Traits
            score.O = PlayerPrefs.GetFloat("OPENNESS", 0f);
            score.C = PlayerPrefs.GetFloat("CONSCIENTIOUSNESS", 0f);
            score.E = PlayerPrefs.GetFloat("EXTRAVERSION", 0f);
            score.A = PlayerPrefs.GetFloat("AGREEABLENESS", 0f);
            score.N = PlayerPrefs.GetFloat("NEUROTICISM", 0f);

            // RIASEC Traits
            score.R = PlayerPrefs.GetFloat("REALISTIC", 0f);
            score.I = PlayerPrefs.GetFloat("INVESTIGATIVE", 0f);
            score.rA = PlayerPrefs.GetFloat("ARTISTIC", 0f);
            score.S = PlayerPrefs.GetFloat("SOCIAL", 0f);
            score.rE = PlayerPrefs.GetFloat("ENTERPRISING", 0f);
            score.rC = PlayerPrefs.GetFloat("CONVENTIONAL", 0f);

            Debug.Log("All Scores Loaded Perfectly!");
            if (taskLoader.CheckIfTasksFinished()==true)
            {
                resultsUI.SetActive(true);
            }
        }
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F12))
    //    {
    //        WipeAllSaveData();
    //    }
    //}

    public void SaveAllScores()
    {
        if (score != null)
        {
            // Save OCEAN
            PlayerPrefs.SetFloat("OPENNESS", (float)score.O);
            PlayerPrefs.SetFloat("CONSCIENTIOUSNESS", (float)score.C);
            PlayerPrefs.SetFloat("EXTRAVERSION", (float)score.E);
            PlayerPrefs.SetFloat("AGREEABLENESS", (float)score.A);
            PlayerPrefs.SetFloat("NEUROTICISM", (float)score.N);

            // Save RIASEC
            PlayerPrefs.SetFloat("REALISTIC", (float)score.R);
            PlayerPrefs.SetFloat("INVESTIGATIVE", (float)score.I);
            PlayerPrefs.SetFloat("ARTISTIC", (float)score.rA);
            PlayerPrefs.SetFloat("SOCIAL", (float)score.S);
            PlayerPrefs.SetFloat("ENTERPRISING", (float)score.rE);
            PlayerPrefs.SetFloat("CONVENTIONAL", (float)score.rC);

            PlayerPrefs.Save();
            Debug.Log("All scores silently saved!");
        }
    }


    //public void SaveDialogueChoice(string choiceText)
    //{
    //    string oldLogs = PlayerPrefs.GetString("DialogueLogs", "");
    //    string newLogs = oldLogs + "- " + choiceText + "\n";

    //    PlayerPrefs.SetString("DialogueLogs", newLogs);
    //    PlayerPrefs.Save();
    //    Debug.Log("Logged choice: " + choiceText);
    //}

 
    public void SaveGameState(string stateName)
    {
        PlayerPrefs.SetInt("HasSavedData", 1);
        PlayerPrefs.SetInt(stateName, 1);
        PlayerPrefs.Save();
        Debug.Log("Game State Saved: " + stateName);
    }
    public void WipeAllSaveData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("ALL SAVE DATA WIPED DELETED! Ready for a fresh test.");
    }
}