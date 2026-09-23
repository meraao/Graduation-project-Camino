using UnityEngine;

public class SimonNPC : NPCInteractable
{


    [Tooltip("Drag the first DialogueNode for this specific NPC here.")]
    public DialogueNode startingConversation;
    public DialogueNode defaultNode;
    [SerializeField] private SaveManager saveManager;
    public Animator simonAnimator;
    public bool simonTaskState;
    public GameObject WarningUI;


    private void Awake()
    {
        simonAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("SimonTaskState_Interacted", 0) == 1)
        {// TRUE: They have talked to Simon before
            simonTaskState = true;
            WarningUI.SetActive(false);
        }
        else
        {
            // FALSE: They have NOT talked to Simon yet (Default State)
            simonTaskState = false;

            // Deactivate the canvas (or activate it, depending on your game's needs!)
            WarningUI.SetActive(true);
        }
    }

    public override void interact(Transform inter)
    {
        base.interact(inter);
        WarningUI.SetActive(false);

        if (simonTaskState == false)
        {
            simonTaskState = true;
            Debug.Log("This NPC starts the Simon Says Test!");
            simonAnimator.SetTrigger("Talk");

            if (startingConversation != null)
            {
                DialogueManager.Instance.StartDialogue(startingConversation);
                saveManager.SaveGameState("SimonTaskState_Interacted");
            }
            else
            {
                Debug.LogWarning(gameObject.name + " has no Dialogue assigned!");
            }
        }
        else
        {
            if (simonTaskState == true)
            {
                simonAnimator.SetTrigger("Thanks");
                DialogueManager.Instance.StartDialogue(defaultNode);
            }
            else
            {
                Debug.Log("Some error happened, please check the SimonNPC code");
            }
        }

    }

    public  void SimonThanksAnimation()
    {
        simonAnimator.SetTrigger("Thanks");
    }
}