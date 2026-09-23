using UnityEngine;

public class EmotionalRecognitionNPC : NPCInteractable
{
    [Tooltip("NPC Info")]
    public DialogueNode startingConversation;
    public DialogueNode defaultNode;
    public DialogueNode tarekNode;
    [SerializeField]private SaveManager saveManager;
    public Animator emotionalRecognitionAnimator;
    public bool emotionalRecognitionState;
    public GameObject WarningUI;

    private void Awake()
    {
        emotionalRecognitionAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("emotionalRecognitionState_Interacted", 0) == 1)
        {
            // TRUE: They have talked to Simon before
            emotionalRecognitionState = true;
            WarningUI.SetActive(false);
        }
        else
        {
            // FALSE: They have NOT talked to Simon yet (Default State)
            emotionalRecognitionState = false;

            // Deactivate the canvas (or activate it, depending on your game's needs!)
            WarningUI.SetActive(true);
        }
    }

    public override void interact(Transform inter)
    {
        base.interact(inter);
        WarningUI.SetActive(false);

        if (emotionalRecognitionState == false)
        {
            emotionalRecognitionState = true;
            Debug.Log("This NPC starts the Emotional Recognition Test!");
            emotionalRecognitionAnimator.SetTrigger("Talk");

            if (startingConversation != null)
            {
                DialogueManager.Instance.StartDialogue(startingConversation);
                saveManager.SaveGameState("emotionalRecognitionState_Interacted");
            }
            else
            {
                Debug.LogWarning(gameObject.name + " has no Dialogue assigned!");
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("PuzzleNPC_Interacted", 0) == 1)
            {
                emotionalRecognitionAnimator.SetTrigger("Thanks");
                DialogueManager.Instance.StartDialogue(tarekNode);

            }
            else
            {
                if (emotionalRecognitionState == true)
            {
                emotionalRecognitionAnimator.SetTrigger("Thanks");
                DialogueManager.Instance.StartDialogue(defaultNode);
            }
            else
            {
                Debug.Log("Some error happened, please check the EmotionalRecognitionNPC code");
            }
            }
        }

    }

    public void EmotionalRecognitionThanksAnimation()
    {
        emotionalRecognitionAnimator.SetTrigger("Thanks");
    }
}
