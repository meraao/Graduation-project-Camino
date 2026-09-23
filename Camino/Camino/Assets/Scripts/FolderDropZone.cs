using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class FolderDropZone : MonoBehaviour, IDropHandler
{
    public string acceptedCategory;
    public TMP_Text feedbackText;

    // --- NEW AUDIO STUFF ---
    public AudioSource audioPlayer;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DraggableFile draggedFile = eventData.pointerDrag.GetComponent<DraggableFile>();

            if (draggedFile != null)
            {
                if (draggedFile.fileCategory == acceptedCategory)
                {
                    feedbackText.text = "Correct!";
                    feedbackText.color = Color.green;

                    // Play the happy sound!
                    if (audioPlayer != null && correctSound != null)
                    {
                        audioPlayer.PlayOneShot(correctSound);
                    }

                    draggedFile.transform.SetParent(this.transform);
                    draggedFile.transform.position = this.transform.position;
                    draggedFile.enabled = false;
                }
                else
                {
                    feedbackText.text = "Wrong folder!";
                    feedbackText.color = Color.red;

                    // Play the error sound!
                    if (audioPlayer != null && wrongSound != null)
                    {
                        audioPlayer.PlayOneShot(wrongSound);
                    }
                }
            }
        }
    }
}