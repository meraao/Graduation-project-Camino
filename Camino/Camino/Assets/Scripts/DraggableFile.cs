using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableFile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string fileCategory; // We will type "Doc", "Photo", or "Audio" here in Unit
    private Vector3 startPosition;
    private Transform startParent;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;

        // Bring the file to the front of the screen
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        // Let the mouse click "through" the item to hit the folder underneath
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Follow the mouse
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Turn clicks back on
        canvasGroup.blocksRaycasts = true;

        // If it wasn't dropped in a valid folder, snap back to the start position
        if (transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
    }
}