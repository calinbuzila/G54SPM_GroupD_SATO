using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class MenuController : MonoBehaviour
{

    public Text highscoreText;
    public Slider difficultySlider;
    public const int DefaultDifficultyLevel = 0;
    protected int DifficultyLevel;
    private string GameDifficulty = "GameDifficulty";
    private string PlayerHighScores = "HighScores";

    protected LevelController levelController;

    void Start()
    {
        //levelController = GameObject.FindObjectOfType<LevelController>();
        //highscoreText.text = levelController.GetHighScores();
        highscoreText.text = GetHighScoresFromPrefs();
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

    public string GetHighScoresFromPrefs()
    {
        if (PlayerPrefs.HasKey(PlayerHighScores))
        {
            var playerScores = PlayerPrefs.GetString(PlayerHighScores).ToString().Split(',');
            var newListOfScores = String.Join("\n", playerScores.Select(scoreValue => scoreValue.ToString()).ToArray());
            return newListOfScores;
        }
        return "";
    }

}
