using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;

    private void Awake()
    {
        menuUI.SetActive(false);
    }

    public void MenuButton()
    {
        menuUI.SetActive(true);
    }

    public void Restart()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("ALL SAVE DATA WIPED DELETED! Ready for a fresh test.");
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseMneu()
    {
        menuUI.SetActive(false);
    }
}
