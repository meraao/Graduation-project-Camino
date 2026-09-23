using UnityEngine;

public class FileSortingNPC : NPCInteractable
{
    [Tooltip("Drag the first DialogueNode for this specific NPC here.")]
    public DialogueNode startingConversation;
    public DialogueNode defaultNode;
    [SerializeField] private SaveManager saveManager;
    public Animator fileSortingAnimator;
    public bool fileSortingState;
    public GameObject WarningUI;


    private void Awake()
    {
        fileSortingAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("FileSortingState_Interacted", 0) == 1)
        {
            fileSortingState = true;
            WarningUI.SetActive(false);
        }
        else
        {
            fileSortingState = false;
            WarningUI.SetActive(true);
        }
    }

    public override void interact(Transform inter)
    {
        base.interact(inter);
        WarningUI.SetActive(false);

        if (fileSortingState == false)
        {
            fileSortingState = true;
            Debug.Log("This NPC starts the File Sorting Test!");
            fileSortingAnimator.SetTrigger("Talk");

            if (startingConversation != null)
            {
                DialogueManager.Instance.StartDialogue(startingConversation);
                saveManager.SaveGameState("FileSortingState_Interacted");
            }
            else
            {
                Debug.LogWarning(gameObject.name + " has no Dialogue assigned!");
            }
        }
        else
        {
            if (fileSortingState == true)
            {
                fileSortingAnimator.SetTrigger("Talk");
                DialogueManager.Instance.StartDialogue(defaultNode);
            }
            else
            {
                Debug.Log("Some error happened, please check the FileSortingNPC code");
            }
        }

    }

    public void FileSortingThanksAnimation()
    {
        fileSortingAnimator.SetTrigger("Thanks");
    }
}
