using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Answers : MonoBehaviour
{
   // public static puzzleManager instance;
    private Questions question;
    private bool currentState;
    public  playerInteraction playerInteraction;


    public void SetCurrentState(int x)
    {
       if (puzzleManager.instance.question != null)
    {
        question = puzzleManager.instance.question;

        // Check if the clicked index is correct
        currentState = question.answers[x].isTrue;

        if (currentState)
        {
            Debug.Log("Correct");
                puzzleManager.instance.score.I += 1;
                puzzleManager.instance.score.O += 0.6;
                puzzleManager.instance.saveManager.SaveAllScores();
                
            }
            else
        {
            Debug.Log("Wrong");
        }

        if (puzzleManager.instance.allQuestion.Count > 0)
        {
            puzzleManager.instance.allQuestion.RemoveAt(0);
        }

        // Generate the next one
        puzzleManager.instance.NewQuestion();
    }
}
}