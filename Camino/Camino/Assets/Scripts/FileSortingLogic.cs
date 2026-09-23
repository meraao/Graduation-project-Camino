using UnityEngine;

public class FileSortingLogic : MonoBehaviour
{
    
    public Canvas fileSortingCanvas;

   
    public void FinishTask(bool isCorrect)
    {
    
        if (isCorrect)
        {
            // IMPORTANT: Replace this line with your team's actual scoring script 
            // (e.g., ScoreManager.instance.AddScore("C", 1); )
            Debug.Log("Task Correct! Added 1 point.");
        }
        else
        {
            Debug.Log("Task Incorrect. No points added.");
        }

        
        if (fileSortingCanvas != null)
        {
            fileSortingCanvas.enabled = false;
            Debug.Log("Canvas Closed!");
        }
    }
}