using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public Text highscoreText;
    public Slider difficultySlider;
    public const int DefaultDifficultyLevel = 0;
    protected int DifficultyLevel;
    private string GameDifficulty = "GameDifficulty";

    protected LevelController levelController;

    void Start()
    {
        levelController = GameObject.FindObjectOfType<LevelController>();
        highscoreText.text = levelController.GetHighScores();
        if (PlayerPrefs.HasKey(GameDifficulty))
        {
            DifficultyLevel = PlayerPrefs.GetInt(GameDifficulty);
            difficultySlider.value = DifficultyLevel;
        }
        else
        {
            DifficultyLevel = DefaultDifficultyLevel;
        }
        difficultySlider.onValueChanged.AddListener(delegate { UpdateDifficultyValue(); });
    }

    public void UpdateDifficultyValue()
    {
        Debug.Log("Difficulty Number: " + difficultySlider.value);
        DifficultyLevel = (int)(difficultySlider.value);
        SaveDifficulty();
    }

    public void SaveDifficulty()
    {
        if (PlayerPrefs.HasKey(GameDifficulty))
        {
            PlayerPrefs.SetInt(GameDifficulty, DifficultyLevel);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt(GameDifficulty, DefaultDifficultyLevel);
            PlayerPrefs.Save();
        }

    }

}
