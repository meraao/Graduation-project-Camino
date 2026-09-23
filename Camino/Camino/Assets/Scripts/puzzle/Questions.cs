using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AnswerData
{
    public Sprite pieceImage;
    public bool isTrue;
}

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz/Question")]
public class Questions : ScriptableObject
{
    public Sprite puzzleWithMissingPiece;
    public List<AnswerData> answers;
}