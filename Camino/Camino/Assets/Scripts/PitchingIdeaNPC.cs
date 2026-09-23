using UnityEngine;

public class PitchingIdeaNPC : NPCInteractable
{

    [Tooltip("Drag the first DialogueNode for this specific NPC here.")]
    public DialogueNode startingConversation;
    public DialogueNode defaultNode;
    [SerializeField] private SaveManager saveManager;
    public Animator pitchingIdeaAnimator;
    public bool pitchingIdeaState;
    public GameObject WarningUI;


    private void Awake()
    {
        pitchingIdeaAnimator = GetComponent<Animator>();
        if (pitchingIdeaState == true)
        {
            pitchingIdeaAnimator.SetTrigger("Idle");
        }
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("pitchingIdeaState_Interacted", 0) == 1)
        {// TRUE: They have talked to Simon before
            pitchingIdeaState = true;
            WarningUI.SetActive(false);
        }
        else
        {
            // FALSE: They have NOT talked to Simon yet (Default State)
            pitchingIdeaState = false;

            // Deactivate the canvas (or activate it, depending on your game's needs!)
            WarningUI.SetActive(true);
        }
    }

    public override void interact(Transform inter)
    {
        base.interact(inter);
        WarningUI.SetActive(false);

        if (pitchingIdeaState == false)
        {
            pitchingIdeaState = true;
            Debug.Log("This NPC starts the Pitching Idea Task!");
            pitchingIdeaAnimator.SetTrigger("Talk");

            if (startingConversation != null)
            {
                DialogueManager.Instance.StartDialogue(startingConversation);
                saveManager.SaveGameState("pitchingIdeaState_Interacted");
            }
            else
            {
                Debug.LogWarning(gameObject.name + " has no Dialogue assigned!");
            }
        }
        else
        {
                if (pitchingIdeaState == true)
                {
                    DialogueManager.Instance.StartDialogue(defaultNode);
                }
                else
                {
                    Debug.Log("Some error happened, please check the PitchingIdeaNPC code");
                }
        }

    }

    public void PitchingIdeaThanksAnimation()
    {
        pitchingIdeaAnimator.SetTrigger("Thanks");
    }
}
