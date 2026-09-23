//using UnityEngine;

//public class SimonNPC : NPCInteractable
//{


//    [Tooltip("Drag the first DialogueNode for this specific NPC here.")]
//    public DialogueNode startingConversation;
//    public DialogueNode defaultNode;
//    [SerializeField] private SaveManager saveManager;
//    public Animator simonAnimator;
//    public bool simonTaskState;

//    private void Awake()
//    {
//        simonAnimator = GetComponent<Animator>();
//    }
//    private void Start()
//    {
//        if (PlayerPrefs.GetInt("SimonTaskState_Interacted", 0) == 1)
//        {
//            simonTaskState = true;
//        }
//    }

//    public override void interact(Transform inter)
//    {
//        base.interact(inter);
//        simonAnimator.SetTrigger("Talk");

//        if (simonTaskState == false)
//        {
//            simonTaskState = true;
//            Debug.Log("This NPC starts the Simon Says Test!");
            

//            if (startingConversation != null)
//            {
//                DialogueManager.Instance.StartDialogue(startingConversation);
//                saveManager.SaveGameState("SimonTaskState_Interacted");
//            }
//            else
//            {
//                Debug.LogWarning(gameObject.name + " has no Dialogue assigned!");
//            }
//        }
//        else
//        {
//            if (simonTaskState == true)
//            {

//                DialogueManager.Instance.StartDialogue(defaultNode);
//            }
//            else
//            {
//                Debug.Log("Some error happened, please check the SimonNPC code");
//            }
//        }

//    }

//    public void SimonClap()
//    {
//        simonAnimator.SetTrigger("Clap");

//    }
//}
