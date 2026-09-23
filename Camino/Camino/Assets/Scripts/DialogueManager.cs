using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;
using StarterAssets;
using System.Collections;
using DG.Tweening.Core.Easing;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcTextDisplay;
    public TextMeshProUGUI charName;
    public Transform buttonContainer;
    public GameObject buttonPrefab;
    public GameObject resultsUI;

    [Header("Save and Score Setup")]
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private ScooringSystem scoringSystem;
    [SerializeField] private TaskLoader taskLoader;

    [Header("Scene Action")]
    public List<UnityEvent> sceneEvents = new List<UnityEvent>();

    private StarterAssetsInputs playerInputs;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialoguePanel.SetActive(false);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInputs = player.GetComponent<StarterAssetsInputs>();
        }
        else
        {
            Debug.LogWarning("Could not find the Player! Make sure your player character's Tag is set to 'Player' at the top of the Inspector.");
        }
    }

    public void StartDialogue(DialogueNode startingNode)
    {
        dialoguePanel.SetActive(true);

        if (playerInputs != null)
        {
            playerInputs.cursorLocked = false;
            playerInputs.cursorInputForLook = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        DisplayNode(startingNode);
    }

    private void DisplayNode(DialogueNode node)
    {
        charName.text = node.charName;
        npcTextDisplay.text = node.npcText;
    
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        if (node.isFinalNode)
        {
            CreateCloseButton();
            return;
        }

        foreach (NodeAnswer answer in node.options)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = answer.optionText;

            NodeAnswer currentAnswer = answer;

            newButton.GetComponent<Button>().onClick.AddListener(() => OnOptionClicked(currentAnswer));
        }
    }

    private void OnOptionClicked(NodeAnswer answer)
    {
        if (answer.pointsToAdd > 0)
        {
            switch (answer.traitToIncrease)
            {
                case "O": AddOpennessPoints(answer.pointsToAdd); break;
                case "C": AddConscientiousnessPoints(answer.pointsToAdd); break;
                case "E": AddExtraversionPoints(answer.pointsToAdd); break;
                case "A": AddAgreeablenessPoints(answer.pointsToAdd); break;
                case "N": AddNeuroticismPoints(answer.pointsToAdd); break;

                case "R": AddRealisticPoints(answer.pointsToAdd); break;
                case "I": AddInvestigativePoints(answer.pointsToAdd); break;
                case "rA": AddArtisticPoints(answer.pointsToAdd); break;
                case "S": AddSocialPoints(answer.pointsToAdd); break;
                case "rE": AddEnterprisingPoints(answer.pointsToAdd); break;
                case "rC": AddConventionalPoints(answer.pointsToAdd); break;
            }
        }

        if (answer.eventIndex >= 0 && answer.eventIndex < sceneEvents.Count)
        {
            sceneEvents[answer.eventIndex].Invoke();
        }

        answer.onOptionSelected?.Invoke();


        if (answer.nextNode != null)
        {
            DisplayNode(answer.nextNode);
        }
        else
        {
            Debug.Log("Close dialogue");
            CloseDialogue();
        }
    }

    private void CreateCloseButton()
    {
        GameObject closeBtn = Instantiate(buttonPrefab, buttonContainer);
        closeBtn.GetComponentInChildren<TextMeshProUGUI>().text = "End Conversation";
        closeBtn.GetComponent<Button>().onClick.AddListener(CloseDialogue);
    }

    public void CloseDialogue()
    {
        Debug.Log("Dialogue Closed");
        dialoguePanel.SetActive(false);
        StartCoroutine(ResetCursorState());
        taskLoader.UpdateCurrentTask();
        if (taskLoader.CheckIfTasksFinished())
        {
            resultsUI.SetActive(true);
            FindObjectOfType<ResultsManager>().DisplayResultsUI();
        }
        else
        {
            Debug.Log("Still got tasks to do.");
        }
        
    }

    private IEnumerator ResetCursorState()
    {
        yield return new WaitForEndOfFrame();
        if (playerInputs != null)
        {
            playerInputs.cursorLocked = true;
            playerInputs.cursorInputForLook = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void AddOpennessPoints(int amount)
    {
        scoringSystem.O += amount;
        saveManager.SaveAllScores();
    }

    public void AddConscientiousnessPoints(int amount)
    {
        scoringSystem.C += amount;
        saveManager.SaveAllScores();
    }

    public void AddExtraversionPoints(int amount)
    {
        scoringSystem.E += amount;
        saveManager.SaveAllScores();
    }

    public void AddAgreeablenessPoints(int amount)
    {
        scoringSystem.A += amount;
        saveManager.SaveAllScores();
    }

    public void AddNeuroticismPoints(int amount)
    {
        scoringSystem.N += amount;
        saveManager.SaveAllScores();
    }

    public void AddRealisticPoints(int amount)
    {
        scoringSystem.R += amount;
        saveManager.SaveAllScores();
    }

    public void AddInvestigativePoints(int amount)
    {
        scoringSystem.I += amount;
        saveManager.SaveAllScores();
    }

    public void AddArtisticPoints(int amount)
    {
        scoringSystem.rA += amount;
        saveManager.SaveAllScores();
    }

    public void AddSocialPoints(int amount)
    {
        scoringSystem.S += amount;
        saveManager.SaveAllScores();
    }

    public void AddEnterprisingPoints(int amount)
    {
        scoringSystem.rE += amount;
        saveManager.SaveAllScores();
    }

    public void AddConventionalPoints(int amount)
    {
        scoringSystem.rC += amount;
        saveManager.SaveAllScores();
    }
}