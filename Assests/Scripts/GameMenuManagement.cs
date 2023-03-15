using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManagement : MonoBehaviour
{
    public GameObject QuitPanel;
    public AudioSource ClickSound;
    //public AudioSource HomeMusic;
    public GameObject LoadingPanel;
    public GameObject GameModeSelectionPanel;

    private void Start()
    {
        Invoke("PlayHomeMusic", 16f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitPanel.SetActive(true);
        }
    }
    public void PlayGame_Easy()
    {
        ClickSound.Play();
        GameModeSelectionPanel.SetActive(false);
        LoadingPanel.SetActive(true);
        PlayerPrefs.SetString("GameMode", GameConstants.Easy_SceneName);
        AsyncOperation LoadingGame = SceneManager.LoadSceneAsync("Game");
        //SceneManager.LoadScene(GameConstants.Easy_SceneName);
    }
    public void PlayGame_Hard()
    {
        ClickSound.Play();
        PlayerPrefs.SetString("GameMode", GameConstants.Hard_SceneName);
        GameModeSelectionPanel.SetActive(false);
        LoadingPanel.SetActive(true);
        AsyncOperation LoadingGame = SceneManager.LoadSceneAsync("Game_Hard");
        //SceneManager.LoadScene(GameConstants.Hard_SceneName);
    }
    public void QuitGame()
    {
        ClickSound.Play();
        Application.Quit();
    }
    public void PlayClickSound()
    {
        ClickSound.Play();
    }

    //public void PlayHomeMusic()
    //{
    //    HomeMusic.Play();
    //}
}
