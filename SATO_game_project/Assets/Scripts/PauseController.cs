using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject quitPanel;
    protected LevelController levelController;
    public GameObject endPanel;
    public Text countdownText;
    protected bool isPaused = false;

    void Start()
    {
        levelController = GameObject.FindObjectOfType<LevelController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && countdownText.enabled == false && levelController.GetGameOverStatus() == false)
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;
        if (isPaused == true)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (isPaused == false)
        {
            pausePanel.SetActive(false);
            quitPanel.SetActive(false);
            StartCoroutine(StartTimer());
        }
    }

    public bool GetPauseStatus()
    {
        return isPaused;
    }

    IEnumerator StartTimer()
    {
        countdownText.enabled = true;
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1);
        countdownText.enabled = false;
        Time.timeScale = 1;
    }
}