//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;
//public class NPCConversation : MonoBehaviour
//{
//    [Header("UI Elements")]
//    public GameObject dialoguePanel;
//    public TextMeshProUGUI dialogueText;
//    public Button okayButton;
//    public Button noButton;

//    [Header("Cameras")]
//    public GameObject cinemachineCam; 
//    public GameObject npcPuzzleCam;

//    [Header("Game State")]
//    public char riasecPoint = 'I';
//    public char oceanPoint = 'O';

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        dialoguePanel.SetActive(false);
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    public void StartTalking()
//    {
//        dialoguePanel.SetActive(true);
//        dialogueText.text = "Hi! I need you to help me solve this puzzle.";

//        // Swap Cameras
//        cinemachineCam.SetActive(false);
//        npcPuzzleCam.SetActive(true);

//        // Show cursor
//        Cursor.lockState = CursorLockMode.None;
//        Cursor.visible = true;
//    }
//}
