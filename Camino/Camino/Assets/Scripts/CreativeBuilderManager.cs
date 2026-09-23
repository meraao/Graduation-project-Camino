using UnityEngine;

public class CreativeBuilderManager : MonoBehaviour
{
    public Transform[] allSnapPoints;
    public int maxBlocksAllowed = 5; // We are only using 5 blocks for this arch

    [Header("Player Trait Scores")]
    public int artisticScore = 0;
    public int opennessScore = 0;
    private bool isTaskComplete = false;

    public void CheckWinCondition()
    {
        if (isTaskComplete) return;

        int filledPoints = 0;
        int blocksUsed = GameObject.FindGameObjectsWithTag("Shape").Length;

        foreach (Transform snapPoint in allSnapPoints)
        {
            Collider2D hit = Physics2D.OverlapCircle(snapPoint.position, 0.1f);
            if (hit != null && hit.CompareTag("Shape"))
            {
                filledPoints++;
            }
        }

        if (filledPoints == allSnapPoints.Length && blocksUsed <= maxBlocksAllowed)
        {
            Debug.Log("Task Success! Perfect Blueprint.");
            artisticScore += 3;
            opennessScore += 2;
            isTaskComplete = true;
        }
        else
        {
            Debug.Log("Task Failed: Blueprint incomplete or overlapping.");
        }
    }
}