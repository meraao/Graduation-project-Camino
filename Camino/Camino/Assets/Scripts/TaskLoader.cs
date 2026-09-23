using TMPro;
using UnityEngine;

public class TaskLoader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentTask;
    [SerializeField] private ResultsManager resultsManager;
    [SerializeField] private GameObject resultsUI;
    public bool allTasksDone = false;

    private void Start()
    {
        UpdateCurrentTask();

        if (allTasksDone)
        {
            resultsManager.DisplayResultsUI();
            if (resultsUI != null) resultsUI.SetActive(true);
        }
    }

    public void UpdateCurrentTask()
    {
        if (currentTask == null) return;

        int puzzleDone = PlayerPrefs.GetInt("PuzzleNPC_Interacted", 0);
        int simonDone = PlayerPrefs.GetInt("SimonTaskState_Interacted", 0);
        int fixDone = PlayerPrefs.GetInt("FixingMachineState_Interacted", 0);
        int fileDone = PlayerPrefs.GetInt("FileSortingState_Interacted", 0);
        int emotionDone = PlayerPrefs.GetInt("emotionalRecognitionState_Interacted", 0);
        int pitchingDone = PlayerPrefs.GetInt("pitchingIdeaState_Interacted", 0);


        if (fixDone == 0)
        {
            currentTask.text = "First talk to Faris, he's the first to help.";
        }
        else if (fileDone == 0)
        {
            currentTask.text = "Now find Nagham, she's to the top left of the map.";
        }
        else if (emotionDone == 0)
        {
            currentTask.text = "Ghala Needs your help, you can find her in the chickens barn.";
        }
        else if (puzzleDone == 0)
        {
            currentTask.text = "Tarek looks really frustrated, you can find him around the interance.";
        }
        else if (simonDone == 0)
        {
            currentTask.text = "As for Salma, she's having a hard time with sequences, shes to the buttom right to the map";
        }
        else if (pitchingDone == 0)
        {
            currentTask.text = "Amir is trying to have a talk but no one is listening, wanna help?";
        }
        else
        {
            currentTask.text = "Congrates! you completed all the tasks.";
            allTasksDone = true;
        }
    }

    public bool CheckIfTasksFinished()
    {
        if (allTasksDone)
        {
            Debug.Log("ALL TASKS COMPLETE! Showing Results...");
            resultsManager.DisplayResultsUI();
            return true;
        }
        else
        {
            return false;
        }
    }
}
