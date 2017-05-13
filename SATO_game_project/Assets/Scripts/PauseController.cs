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
        }
        else if (isPaused == false)
        {
            pausePanel.SetActive(false);
            quitPanel.SetActive(false);
        }
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public bool GetPauseStatus()
    {
        return isPaused;
    }
}