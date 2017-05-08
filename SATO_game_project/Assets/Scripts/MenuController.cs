﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Text highscoreText;
    public Slider difficultySlider;
    public const int DefaultDifficultyLevel = 0;
    protected int DifficultyLevel;

    protected LevelController levelController;

    void Start()
    {
        levelController = GameObject.FindObjectOfType<LevelController>();
        highscoreText.text = levelController.GetHighScores();
        DifficultyLevel = DefaultDifficultyLevel;
        difficultySlider.onValueChanged.AddListener(delegate { UpdateDifficultyValue(); });
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitScene()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void UpdateDifficultyValue()
    {
        Debug.Log("Difficulty Number: " + difficultySlider.value);
        DifficultyLevel = (int)(difficultySlider.value);
    }

}
