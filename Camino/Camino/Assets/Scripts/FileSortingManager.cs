using UnityEngine;

public class FileSortingManager : MonoBehaviour
{
    public static FileSortingManager instance;
    public ScooringSystem scoring;
    public SaveManager saveManager;

    public GameObject fileSortingUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // // fileSortingUI.SetActive(false); (Leave this commented out if you are still using the Two Slashes trick to test!)
    }

    public void StartTask()
    {
        if (fileSortingUI != null)
        {
            fileSortingUI.SetActive(true);
            Debug.Log("File Sorting Task Started!");
        }
    }

    public void FinishTask(bool isCorrect)
    {
        if (isCorrect)
        {
            scoring.rC += 3; 
            scoring.C += 2;  
            saveManager.SaveAllScores();

        }
        else
        {
            Debug.Log("Task Incorrect. No points added.");
        }

        // Close the Canvas immediately after scoring
        if (fileSortingUI != null)
        {
            fileSortingUI.SetActive(false);
            Debug.Log("File Sorting Canvas Closed!");
        }
    }
}