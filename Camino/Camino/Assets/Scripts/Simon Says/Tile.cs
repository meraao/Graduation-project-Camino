using UnityEngine;
using UnityEngine.UI; 

public class Tile : MonoBehaviour
{
    private SimonsGameManager simonsGameManager;
    private Image tileImage; 

    private int tileId;
    private Color baseColor; 

    public void Initilization(SimonsGameManager gameManager, int Id, Color tileColor)
    {
        this.simonsGameManager = gameManager;
        this.tileId = Id;
        this.baseColor = tileColor;

        tileImage = GetComponent<Image>();

        TurnOff();
    }

    public void TurnOff()
    {
        Color darkColor = new Color(baseColor.r * 0.3f, baseColor.g * 0.3f, baseColor.b * 0.3f, 1f);
        tileImage.color = darkColor;
    }

    public void TurnOn()
    {
        tileImage.color = baseColor;
    }

    public void OnTileClicked()
    {
        simonsGameManager.PlayLightAndTone(tileId);
    }
}