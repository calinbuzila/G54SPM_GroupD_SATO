using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ScoreTests {

	/// <summary>
	/// Checks that the score initialises to zero.
	/// </summary>
	[Test]
	public void ScoreInitialisesToZero() 
	{
        LevelController levelController = GameObject.FindObjectOfType<LevelController>();
		levelController.Initialise ();
		Assert.AreEqual(0, levelController.GetScore());
	}

	/// <summary>
	/// Checks that the score cannot ever go into the negative through LevelController.SetScore(int);
	/// </summary>
	/// <param name="setToValue"></param>
	[TestCase(-1)]
	[TestCase(-20)]
	[TestCase(-12345)]
	[TestCase(25)]
	[TestCase('B')]
	public void ScoreNeverGoesNegativeThroughSetScore(int setToValue)
	{
        LevelController levelController = GameObject.FindObjectOfType<LevelController>();
		levelController.SetScore (setToValue);
		Assert.IsTrue (levelController.GetScore () >= 0);
	}

	/// <summary>
	/// Checks that the score cannot ever go into the negative through LevelController.AddToScore(int);
	/// </summary>
	/// <param name="initialValue"></param>
	/// <param name="addedValue">
	[TestCase(0, -1)]
	[TestCase(20, -40)]
	[TestCase(5, -90)]
	public void ScoreNeverGoesNegativeThroughAddToScore(int initialValue, int addedValue)
	{
        LevelController levelController = GameObject.FindObjectOfType<LevelController>();

		levelController.SetScore (initialValue);
		levelController.AddToScore (addedValue);

		Assert.IsTrue (levelController.GetScore () >= 0);
	}

	/// <summary>
	/// Checks that the player may never exceed the possible score limit 
	/// through incrementation, and that the value does not cause undetermined behaviour.
	/// </summary>
	/// <param name="initialValue">
	[TestCase(20)]
	[TestCase(int.MaxValue)]
	[TestCase(50513)]
	public void ScoreDoesNotExceedLimitThroughIncrementation(int initialValue)
	{
        LevelController levelController = GameObject.FindObjectOfType<LevelController>();

		levelController.SetScore (initialValue);
		levelController.IncrementScore ();
		int newValue = levelController.GetScore ();

		Assert.IsTrue (newValue == initialValue + 1 || newValue == LevelController.ScoreLimit);
	}
}
