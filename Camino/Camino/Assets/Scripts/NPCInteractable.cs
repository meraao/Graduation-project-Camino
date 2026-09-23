using UnityEngine;

public class NPCInteractable : MonoBehaviour
{


    [SerializeField] private NPCLookAt npcLookAt;

    public virtual void interact(Transform interactTransform)
    {
        if (npcLookAt != null)
        {
            Debug.Log("Interact!");
            float playerheight = 0.55217f;
            npcLookAt.LookAtPosition(interactTransform.position + Vector3.up * playerheight);
        } else
        {
            Debug.LogError("The NPCLookAt slot is empty! Please drag the script into the Inspector on " + gameObject.name);
        }
            
    }
}
