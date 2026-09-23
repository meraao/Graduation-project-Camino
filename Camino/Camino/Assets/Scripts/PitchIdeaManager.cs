using UnityEngine;

public class PitchIdeaManager : MonoBehaviour
{
    public static PitchIdeaManager instance;

    public GameObject pitchIdeaUI;

    
    public SaveManager saveManager;
    public ScooringSystem scoring;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }

        
        if (pitchIdeaUI != null)
        {
            pitchIdeaUI.SetActive(false);
        }
    }

    public void StartTask()
    {
        if (pitchIdeaUI != null)
        {
            pitchIdeaUI.SetActive(true);
            Debug.Log("Pitch Idea Task Started!");
        }
    }

    public void FinishTask(bool isCorrect)
    {
        if (isCorrect && scoring != null && saveManager != null)
        {
            scoring.rE += 3; // +3 Enterprising 
            scoring.E += 2;  // +2 Extraversion 
            saveManager.SaveAllScores();
        }

        else
        {
            Debug.Log("Pitch Incorrect (or Managers missing). Canvas closed. No points added.");
        }

        if (pitchIdeaUI != null)
        {
            pitchIdeaUI.SetActive(false);
            Debug.Log("Pitch Idea Canvas Closed!");
        }
    }
}