using UnityEngine;
using StarterAssets; // Required to talk to the player controller

public class CursorController : MonoBehaviour
{
    private StarterAssetsInputs playerInputs;

    private void Start()
    {
        // Automatically find the Player and grab the input script
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInputs = player.GetComponent<StarterAssetsInputs>();
        }
        else
        {
            Debug.LogWarning("CursorController couldn't find the Player!");
        }
    }

    private void Update()
    {
        // Safety check to make sure we actually found the player
        if (playerInputs == null) return;

        // 1. When the player PRESSES and HOLDS the Left Alt key
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            UnlockCursor();
        }

        // 2. When the player RELEASES the Left Alt key
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            LockCursor();
        }
    }

    public void UnlockCursor()
    {
        playerInputs.cursorLocked = false;
        playerInputs.cursorInputForLook = false; // Stops the camera from moving
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockCursor()
    {
        playerInputs.cursorLocked = true;
        playerInputs.cursorInputForLook = true; // Gives camera control back
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}