using System.Collections;
using UnityEngine;

public class DraggingWireScript : MonoBehaviour
{
    [Header("Wire Visuals")]
    public SpriteRenderer wireEnd;
    public GameObject lightOn;

    [Header("Canvas")]
    public GameObject wireGameCanvas;

    private Vector3 startPoint;

    // prevents changing wire after connecting
    private bool isLocked = false;

    // tracks completed wires
    private static int completedWires = 0;

    // total wires needed
    public static int totalWires = 3;

    void Start()
    {
        startPoint = transform.parent.position;

        // only reset once
        if (completedWires >= totalWires)
        {
            completedWires = 0;
        }

        // hide game at start
        if (wireGameCanvas != null)
        {
            wireGameCanvas.SetActive(false);
        }
    }

    // FUNCTION MERA CAN CALL
    public void StartWireGame()
    {
        completedWires = 0;

        if (wireGameCanvas != null)
        {
            wireGameCanvas.SetActive(true);
        }
    }

    private void OnMouseDrag()
    {
        // stop movement if already connected
        if (isLocked)
            return;

        // mouse position to world point
        Vector3 newPosition =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        newPosition.z = 0;

        // check nearby connection points
        Collider2D[] colliders =
            Physics2D.OverlapCircleAll(newPosition, .2f);

        foreach (Collider2D collider in colliders)
        {
            // ignore self
            if (collider.gameObject != gameObject)
            {
                // snap wire to target
                UpdateWire(collider.transform.position);

                // lock THIS wire no matter what
                isLocked = true;

                // correct connection
                if (transform.parent.name.Equals(collider.transform.parent.name))
                {
                    collider.GetComponent<DraggingWireScript>()?.Done();
                    Done();
                }
                else
                {
                    // wrong connection still locks
                    lightOn.SetActive(false);

                    completedWires++;

                    CheckGameComplete();
                }

                return;
            }
        }

        // update wire while dragging
        UpdateWire(newPosition);
    }

    void Done()
    {
        // avoid duplicate completion
        if (isLocked == false)
            return;

        // turn on light
        lightOn.SetActive(true);

        completedWires++;

        CheckGameComplete();

        // destroy dragging ability
        Destroy(this);
    }

    void CheckGameComplete()
    {
        if (completedWires >= totalWires)
        {
            StartCoroutine(CloseWireGame());
        }
    }

    IEnumerator CloseWireGame()
    {
        yield return new WaitForSeconds(1.5f);

        if (wireGameCanvas != null)
        {
            wireGameCanvas.SetActive(false);
        }
    }

    void UpdateWire(Vector3 newPosition)
    {
        // update position
        transform.position = newPosition;

        // update direction
        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        // update scale
        float dist = Vector2.Distance(startPoint, newPosition);

        wireEnd.size =
            new Vector2(dist, wireEnd.size.y);
    }
}