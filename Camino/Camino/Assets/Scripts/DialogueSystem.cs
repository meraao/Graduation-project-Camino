using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class StoryBlock
{
    [TextArea(3, 10)]
    public string dialogueText;
    public Choice[] choices;
}

[System.Serializable]
public class Choice
{
    public string choiceText;
    public int nextNode;
    public int scoreValue; // NEW: Holds the 1-5 score for this specific button
}

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed = 0.05f;

    [Header("Question Data")]
    public TextAsset questionDataCSV;

    [Header("Dialogue Tree")]
    public StoryBlock[] nodes;

    [Header("UI Buttons")]
    public Button[] optionButtons;
    public TextMeshProUGUI[] optionTexts;

    // NEW: The memory bank. 300 questions means an array size of 300.
    public int[] playerAnswers = new int[300];

    private int currentNode = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        if (questionDataCSV != null)
        {
            LoadQuestionsFromCSV();
        }
        else
        {
            Debug.LogError("Please assign the Question Data CSV in the Inspector!");
        }

        HideOptions();
        DisplayNode(0);
    }

    void LoadQuestionsFromCSV()
    {
        string[] rawLines = questionDataCSV.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        nodes = new StoryBlock[rawLines.Length];

        string[] standardChoices = { "Strongly Disagree", "Disagree", "Neutral", "Agree", "Strongly Agree" };

        for (int i = 0; i < rawLines.Length; i++)
        {
            StoryBlock newBlock = new StoryBlock();
            newBlock.dialogueText = rawLines[i];
            newBlock.choices = new Choice[5];

            int nextQuestionIndex = (i == rawLines.Length - 1) ? -1 : (i + 1);

            for (int j = 0; j < 5; j++)
            {
                newBlock.choices[j] = new Choice();
                newBlock.choices[j].choiceText = standardChoices[j];
                newBlock.choices[j].nextNode = nextQuestionIndex;
                newBlock.choices[j].scoreValue = j + 1; // NEW: Assigns a score of 1, 2, 3, 4, or 5 to the button
            }

            nodes[i] = newBlock;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTyping)
        {
            StopCoroutine(typingCoroutine);
            textComponent.text = nodes[currentNode].dialogueText;
            isTyping = false;
            ShowOptions();
        }
    }
    /*
     void Update()
    {
        // Your existing click-to-skip typing code
        if (Input.GetMouseButtonDown(0) && isTyping)
        {
            StopCoroutine(typingCoroutine);
            textComponent.text = nodes[currentNode].dialogueText;
            isTyping = false;
            ShowOptions();
        }

        // --- NEW: Developer Cheat Key ---
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AutoFinishSurvey();
        }
        // --------------------------------
    }

    // --- NEW: The function that fakes the answers ---
    void AutoFinishSurvey()
    {
        Debug.Log("Cheat Activated: Auto-filling all 300 questions...");
        
        // Loop through every question and pick a random answer between 1 and 5
        for (int i = 0; i < nodes.Length; i++)
        {
            playerAnswers[i] = Random.Range(1, 6); 
        }

        // Instantly jump to the end of the game (-1 triggers the 'Survey Complete' log)
        DisplayNode(-1); 
    }  for testing things 
     */

    public void DisplayNode(int nodeIndex)
    {
        if (nodeIndex < 0 || nodeIndex >= nodes.Length)
        {
            gameObject.SetActive(false);
            Debug.Log("Survey Complete! Check the playerAnswers array."); // Added a log to let you know it finished
            return;
        }

        currentNode = nodeIndex;
        HideOptions();
        typingCoroutine = StartCoroutine(TypeLine(nodes[currentNode].dialogueText));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        textComponent.text = string.Empty;

        string[] lines = line.Split('\n');

        foreach (string singleLine in lines)
        {
            textComponent.text += singleLine + "\n";
            yield return new WaitForSeconds(textSpeed * 10);
        }

        isTyping = false;
        ShowOptions();
    }

    void ShowOptions()
    {
        Choice[] currentChoices = nodes[currentNode].choices;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < currentChoices.Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionTexts[i].text = currentChoices[i].choiceText;

                // Capture the data we need to record
                int nextIndex = currentChoices[i].nextNode;
                int scoreToRecord = currentChoices[i].scoreValue;
                int questionBeingAnswered = currentNode;

                optionButtons[i].onClick.RemoveAllListeners();

                // NEW: Route the click through our recording function first
                optionButtons[i].onClick.AddListener(() => RecordAnswerAndContinue(questionBeingAnswered, scoreToRecord, nextIndex));
            }
        }
    }

    // NEW: The function that saves the score into the array before moving on
    void RecordAnswerAndContinue(int questionIndex, int score, int nextNodeIndex)
    {
        playerAnswers[questionIndex] = score;
        DisplayNode(nextNodeIndex);
    }

    void HideOptions()
    {
        foreach (Button btn in optionButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }
}