using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagementScript : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject GameOverPanel;
    public GameObject RivalaryCarSpawner;
    //public Text EarnedPointsText;
    //public Image HighScoreImage1;
    //public Image HighScoreImage2;
    public AudioSource ClickSound;
    public AudioSource CarRunningSound_Easy;
    public AudioSource CarRunningSound_Hard;
    public AudioSource CarCrashingSound;
    //public AudioSource Music;
    public bool IsStopped = false;
    //public static GameManagementScript instance;
    public GameObject RestartGameConfirmationBox;
    public GameObject ExplosionAnimation;

    // Start is called before the first frame update
    void Start()
    {
        //if (instance == null) instance = this;
        if (PlayerPrefs.GetString("GameMode") == GameConstants.Easy_SceneName)
        {
            Debug.Log("Playing Easy Mode Sound!");
            CarRunningSound_Easy.PlayDelayed(0.4f);
        }

        else
        {
            Debug.Log("Playing Hard Mode Sound!");
            CarRunningSound_Hard.PlayDelayed(0.4f);
        }
        IsStopped = false;
    }
    public void PauseGame()
    {
        IsStopped = true;
        PausePlayerCarRacingSound();
        ClickSound.Play();
        RivalaryCarSpawner.SetActive(false);
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        IsStopped = false;
        ClickSound.Play();
        PlayCarRacingSound();
        RivalaryCarSpawner.SetActive(true);
        RivalryCarSpawner.instance.SpawnRivalaryCarAgainAfterStop();
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        CarCrashingSound.Play();
        Time.timeScale = 0;
        ExplosionAnimation.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        ExplosionAnimation.SetActive(true);
        ActivateAnimation();
        IsStopped = true;
        StopPlayerCarRacingSound();
        //Music.Stop();
        StartCoroutine(OverTheGameAfterSomeTime());
        //SetEarnedScoreTextOnGameOver();
    }
    private void ActivateAnimation()
    {
        Transform animatorObject = ExplosionAnimation.transform.Find("ExpAnimator");
        if (animatorObject != null)
        {
            Animator animator = animatorObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.speed = 1;
            }
        }
    }
    IEnumerator OverTheGameAfterSomeTime()
    {
        yield return new WaitForSecondsRealtime(2f);
        RivalaryCarSpawner.SetActive(false);
        GameOverPanel.SetActive(true);
    }
    public void Restart()
    {
        IsStopped = false;
        ClickSound.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayCarRacingSound();
    }
    public void ShowGameRestartConfirmationBox()
    {
        PlayClickSound();
        RestartGameConfirmationBox.SetActive(true);
    }
    public void HideGameRestartConfirmationBox()
    {
        PlayClickSound();
        RestartGameConfirmationBox.SetActive(false);
    }
    public void OpenGameMenu()
    {
        IsStopped = true;
        StopPlayerCarRacingSound();
        ClickSound.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameOver();
    }
    public void PlayClickSound()
    {
        ClickSound.Play();
    }
    private void PlayCarRacingSound()
    {
        if (PlayerPrefs.GetString("GameMode") == GameConstants.Easy_SceneName) CarRunningSound_Easy.PlayOneShot(CarRunningSound_Easy.clip);
        else CarRunningSound_Hard.PlayOneShot(CarRunningSound_Hard.clip);
    }
    private void PausePlayerCarRacingSound()
    {
        CarRunningSound_Easy.Pause();
        CarRunningSound_Hard.Pause();
    }
    private void StopPlayerCarRacingSound()
    {
        CarRunningSound_Easy.Stop();
        CarRunningSound_Hard.Stop();
    }
}
