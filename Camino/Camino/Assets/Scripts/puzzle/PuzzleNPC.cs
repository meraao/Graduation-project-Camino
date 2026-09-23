using UnityEngine;

public class PuzzleNPC : NPCInteractable
{
    
    [Tooltip("Drag the first DialogueNode for this specific NPC here.")]
    public DialogueNode startingConversation;
    public DialogueNode defaultNode;
    [SerializeField] private SaveManager saveManager;
    public Animator puzzleAnimator;
    public bool puzzleState;
    public GameObject WarningUI;

    private void Awake()
    {
        puzzleAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("PuzzleNPC_Interacted", 0) == 1)
        {
            puzzleState = true;
            WarningUI.SetActive(false);
        }
        else
        {
            puzzleState = false;

            WarningUI.SetActive(true);
        }
    }

    public override void interact(Transform inter)
    {
        base.interact(inter);
        WarningUI.SetActive(false);

        puzzleAnimator.SetTrigger("Talk");
        if (puzzleState == false)
        {
            puzzleState = true;
        Debug.Log("This NPC starts the Pattern Puzzle!");
        if (startingConversation != null)
        {
            DialogueManager.Instance.StartDialogue(startingConversation);
                saveManager.SaveGameState("PuzzleNPC_Interacted");
        }
        else
        {
            Debug.LogWarning(gameObject.name + " has no Dialogue assigned!");
        }
        } else
        {
            if (puzzleState == true)
            {
                DialogueManager.Instance.StartDialogue(defaultNode);
            }
        }
        
    }
    public void PuzzleClapAnimation()
    {
        puzzleAnimator.SetTrigger("Clap");
    }
}
