using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject settingsSceen;
    [SerializeField] private GameObject resumeButton;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HasSavedData"))
        {
            resumeButton.SetActive(true);
        }
        else
        {
            resumeButton.SetActive(false);
        }
    }
    public void loadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void loadMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void StartANewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("New game started.");
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        settingsSceen.SetActive(true);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
