using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class puzzleManager: MonoBehaviour
{
    public static puzzleManager instance;
    public List<Questions> allQuestion;
    public Questions question;
    
    // UI Elements
    public Image questionDisplay;       // The image showing the puzzle with a hole
    public List<Image> answerSlots;    // The images on your jigsaw piece buttons
    public GameObject PuzzlePatternUI;
    public ScooringSystem score;
    public PuzzleNPC puzzleNPC;
    public SaveManager saveManager;

    private void Awake()
    {
        if (instance == null && instance != this)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
        PuzzlePatternUI.SetActive(false);
            }
    public void puzzleInteraction()
    {
        if (PuzzlePatternUI != null)
        {
            PuzzlePatternUI.SetActive(true);
        }

        NewQuestion();
    }
    public void NewQuestion()
    {
        Debug.Log("Generating new question");

        if (allQuestion.Count > 0)
        {
            question = allQuestion[0];
            questionDisplay.sprite = question.puzzleWithMissingPiece;

            for (int i = 0; i < answerSlots.Count; i++)
            {
                if (i < question.answers.Count)
                {
                    answerSlots[i].sprite = question.answers[i].pieceImage;
                    answerSlots[i].gameObject.SetActive(true);
                }
                else
                {
                    answerSlots[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            PuzzlePatternUI.SetActive(false);
            puzzleNPC.puzzleAnimator.SetTrigger("Clap");
            Debug.Log("All tasks completed!");
            puzzleNPC.PuzzleClapAnimation();
        }
    }
}