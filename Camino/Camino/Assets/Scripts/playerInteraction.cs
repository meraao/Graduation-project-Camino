using TMPro;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    protected bool aroundNPC = false;

    [SerializeField] private GameObject playerInteractionUI;
    [SerializeField] private TextMeshProUGUI interactionText;
    private bool hasInteracted = false;
    [SerializeField]private AudioSystemManager audioSystemManager;

    float interactRange = 0.8f;

    private void Update()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        NPCInteractable nearbyNPC = null;

        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable npc))
            {
                nearbyNPC = npc;
                break;
            }
        }

        if (nearbyNPC != null)
        {
            aroundNPC = true;

           
            if (!hasInteracted)
            {
                if (interactionText != null)
                {
                    interactionText.text = $"Talk to {nearbyNPC.gameObject.name}";
                }
                playerInteractionUI.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E) && !hasInteracted)
            {
                playerInteractionUI.SetActive(false);
                audioSystemManager.ButtonClicked();
                Debug.Log("E key pressed!");
                nearbyNPC.interact(transform);

               
                hasInteracted = true;
            }
        }
        else
        {
            aroundNPC = false;
            playerInteractionUI.SetActive(false);

            hasInteracted = false;
        }
    }
}