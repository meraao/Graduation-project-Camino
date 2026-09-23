using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class NodeAnswer
{
    public string optionText;
    public DialogueNode nextNode;

    [Header("Scoring")]
    public string traitToIncrease = "";
    public int pointsToAdd = 0; 

    public UnityEvent onOptionSelected;

    [Tooltip("Leave at -1 for no event. Set to 0 to trigger the first event in your Manager.")]
    public int eventIndex = -1; 
}

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue System/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
    [SerializeField] public string charName;
    [TextArea(3, 10)]
    public string npcText;

    [Tooltip("Check this if this node is the end of the conversation.")]
    public bool isFinalNode;

    public List<NodeAnswer> options;
}