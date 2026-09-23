using UnityEngine;

public class AudioSystemManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource buttonSound;

    private void Awake()
    {
        backgroundMusic.Play();
    }

    public void ButtonClicked()
    {
        buttonSound.Play();
    }

    public void StopBackgroundMudic()
    {
        backgroundMusic?.Pause();
    }

    public void ContinueBackgroundMusic()
    {
        backgroundMusic.UnPause();
    }
}
