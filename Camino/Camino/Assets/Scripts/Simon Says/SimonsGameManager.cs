using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonsGameManager : MonoBehaviour
{
    private int numRow = 3;
    private int numCol = 4;

    [Header("Spacing Setup")]
    [Tooltip("The gap between each tile in pixels")]
    [SerializeField] private float gap = 20f;
    private int numTiles;
    private Tile[] tile;

    [Header("Game Object")]
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform gameArea;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject memoryGameUI;
    [SerializeField] private ScooringSystem score;
    [SerializeField] private SimonNPC simonNPC;
    [SerializeField] private SaveManager saveManager;

    [Header("Audio Setup")]
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private AudioSystemManager audioSystemManager;
    [SerializeField] private AudioSource audioSource;

    enum GameMode
    {
        none,
        Listening, 
        Playing
    }

    private GameMode gameMode = GameMode.none;

    private List<int> levelTiles;
    private int currentIndex = 0;
    private int currentLevelIndex = 0;

    private List<List<int>> allLevels = new List<List<int>>
    {
        new List<int> { 5, 0, 10, 3, 8 },           
        new List<int> { 1, 4, 7, 2, 9, 11 },      
        new List<int> { 0, 11, 5, 6, 3, 8, 10 }
    };

    public void Awake()
    {
        gameMode = GameMode.none;
        memoryGameUI.SetActive(false);
    }

    public void StartSimonGame() //start
    {
        audioSystemManager.StopBackgroundMudic();
        memoryGameUI.SetActive(true);
        numTiles = 12;
        tile = new Tile[numTiles];
        float actualTileSize = 1.2f * 330f;
        float stepDistance = actualTileSize + gap; 

        for (int row = 0; row < numRow; row++)
        {
            for (int col = 0; col < numCol; col++)
            {
                int index = (row * numCol) + col;
                tile[index] = Instantiate(tilePrefab, gameArea);

                Color assignedColor = Color.HSVToRGB((float)index / numTiles, 0.8f, 0.9f);
                tile[index].Initilization(this, index, assignedColor);

                float posX = (col - (numCol - 1) / 2f) * stepDistance;
                float posY = (row - (numRow - 1) / 2f) * stepDistance;

                tile[index].transform.localPosition = new Vector3(posX, posY, 0f);
            }
        }
    }

    private IEnumerator FlashTile(int index)
    {
        tile[index].TurnOn();
        yield return new WaitForSeconds(duration);
        tile[index].TurnOff();
    }

    public void PlayLightAndTone(int index)
    {
        try
        {
            if (gameMode == GameMode.Playing)
            {
                StartCoroutine(FlashTile(index));

                if (levelTiles.Count > 0)
                {
                    if (index == levelTiles[currentIndex])
                    {
                        PlayTone(index);
                        currentIndex++;

                        if (currentIndex == levelTiles.Count)
                        {
                            currentLevelIndex++;
                            score.rC += 0.6;
                            score.N -= 0.6;
                            saveManager.SaveAllScores();
                            if (currentLevelIndex < allLevels.Count)
                            {
                                Debug.Log("Level complete!");
                                levelTiles = allLevels[currentLevelIndex];
                                StartCoroutine(PlaySequence());
                            }
                            else
                            {
                                Debug.Log("You beat all 3 levels! YOU WIN!");
                                gameMode = GameMode.none;
                                memoryGameUI.SetActive(false);
                                audioSystemManager.ContinueBackgroundMusic();
                                simonNPC.SimonThanksAnimation();
                            }
                        }
                       
                    }
                    else
                    {
                        Debug.Log("You lost");
                        gameMode = GameMode.none;
                        memoryGameUI.SetActive(false);
                        audioSystemManager.ContinueBackgroundMusic();
                        simonNPC.SimonThanksAnimation();
                    }
                }
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.Log("No more sequences");
            memoryGameUI.SetActive(false);
        }
    }

    private void PlayTone(int index)
    {
        audioSource.pitch = Mathf.Lerp(1.0f, 2.5f, (float)index / (numTiles - 1));
        double currentTime = AudioSettings.dspTime;
        audioSource.PlayScheduled(currentTime);
    }

    public void Play()
    {
        playButton.SetActive(false);
        currentLevelIndex = 0;

        levelTiles = allLevels[currentLevelIndex];

        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        gameMode = GameMode.Listening;
        yield return new WaitForSeconds(1f);
        foreach (int index in levelTiles)
        {
            PlayTone(index);
            yield return FlashTile(index);
            yield return new WaitForSeconds(duration);
        }
        currentIndex = 0;
        gameMode = GameMode.Playing;
    }
}