using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	//TODO Implement health/ lives mechanic(s)
	protected static int PlayerScore;

	void Start()
	{
		SetScore (0);
	}

	public void IncrementScore()
	{
		AddToScore (1);
	}

	public void DecrementScore()
	{
		AddToScore (-1);
	}

	public void AddToScore(int value)
	{
		PlayerScore += value;
	}

	public int GetScore()
	{
		return PlayerScore;
	}

	public void SetScore(int value)
	{
		PlayerScore = value;
	}
}
