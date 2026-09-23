using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmotionQuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public Sprite emotionSprite;
        public string correctAnswer;
    }
    [Header("Systems")]
    public ScooringSystem scooringSystem;
    public SaveManager saveManager;

    [Header("Questions")]
    public Question[] questions;

    [Header("Canvas")]
    public GameObject quizCanvas;

    [Header("npc")]
    public EmotionalRecognitionNPC emotionalNPC;

    [Header("UI")]
    public Image emotionImage;
    public TMP_Text questionText;
    public TMP_Text feedbackText;

    [Header("Buttons")]
    public Button happyButton;
    public Button sadButton;
    public Button cantTellButton;

    private int currentQuestion = 0;
    private bool answered = false;

    void Start()
    {
        // Hide quiz at game start
        quizCanvas.SetActive(false);

        happyButton.onClick.AddListener(() => CheckAnswer("happy"));
        sadButton.onClick.AddListener(() => CheckAnswer("sad"));
        cantTellButton.onClick.AddListener(() => CheckAnswer("can't tell"));
    }

    // FUNCTION MERA CAN CALL
    public void StartQuiz()
    {
        quizCanvas.SetActive(true);

        currentQuestion = 0;

        happyButton.gameObject.SetActive(true);
        sadButton.gameObject.SetActive(true);
        cantTellButton.gameObject.SetActive(true);

        emotionImage.gameObject.SetActive(true);

        LoadQuestion();
    }

    void LoadQuestion()
    {
        answered = false;

        feedbackText.text = "";

        questionText.text =
            "What emotion do you think this image is showing?";

        emotionImage.sprite =
            questions[currentQuestion].emotionSprite;
    }

    void CheckAnswer(string selectedAnswer)
    {
        if (answered)
            return;

        answered = true;

        string correctAnswer =
            questions[currentQuestion].correctAnswer;

        if (selectedAnswer == correctAnswer)
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;
            scooringSystem.S += 1.5;
            scooringSystem.A += 1;
            saveManager.SaveAllScores();
        }
        else
        {
            feedbackText.text =
                "Incorrect! Correct answer: " + correctAnswer;

            feedbackText.color = Color.red;
        }

        StartCoroutine(NextQuestion());
    }

    IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(0.75f);

        currentQuestion++;

        if (currentQuestion >= questions.Length)
        {
            questionText.text = "Quiz Finished!";
            feedbackText.text = "";
            emotionalNPC.EmotionalRecognitionThanksAnimation();

            emotionImage.gameObject.SetActive(false);

            happyButton.gameObject.SetActive(false);
            sadButton.gameObject.SetActive(false);
            cantTellButton.gameObject.SetActive(false);

            yield return new WaitForSeconds(1.5f);

            quizCanvas.SetActive(false);
            emotionalNPC.EmotionalRecognitionThanksAnimation();
            yield break;
        }

        LoadQuestion();
    }
}