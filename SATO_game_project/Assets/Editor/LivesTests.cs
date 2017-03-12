using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class LivesTests {

	/// <summary>
	/// Checks that the player's lives initialise to the default amount.
	/// </summary>
	[Test]
	public void LivesInitialiseToDefault()
	{
		LevelController levelController = GameObject.FindObjectOfType<LevelController> ();
		levelController.Initialise ();
		Assert.AreEqual (levelController.GetLives (), LevelController.DefaultLives);
	}

	/// <summary>
	/// Checks that the player's lives may be decremented correctly.
	/// </summary>
	/// <param name="initialValue"></param>
	[TestCase(3)]
	[TestCase(2)]
	[TestCase(1)]
	[TestCase(20)]
	public void LivesDecrementCorrectly(int initialValue)
	{
		LevelController levelController = GameObject.FindObjectOfType<LevelController> ();
		levelController.SetLives (initialValue);
		levelController.DecrementLives ();
		Assert.AreEqual (levelController.GetLives (), initialValue - 1);
	}

	/// <summary>
	/// Checks that the player's lives may not go into the negative.
	/// </summary>
	[Test]
	public void LivesNeverGoNegativeThroughDecrementation()
	{
		LevelController levelController = GameObject.FindObjectOfType<LevelController> ();
		levelController.SetLives (0);
		levelController.DecrementLives ();
		Assert.IsTrue (levelController.GetLives () >= 0);
	}

	/// <summary>
	/// Checks like the player's lives may be incremented correctly.
	/// </summary>
	/// <param name="initialValue"></param>
	[TestCase(3)]
	[TestCase(20)]
	public void LivesIncrementCorrectly(int initialValue)
	{
		LevelController levelController = GameObject.FindObjectOfType<LevelController> ();
		levelController.SetLives (initialValue);
		levelController.IncrementLives ();
		Assert.AreEqual (levelController.GetLives (), initialValue + 1);
	}

	/// <summary>
	/// Lives may not increment above the set limit.
	/// </summary>
	[Test]
	public void LivesNeverExceedLimitThroughIncrementation()
	{
		LevelController levelController = GameObject.FindObjectOfType<LevelController> ();
		levelController.SetLives (LevelController.LivesLimit);
		levelController.IncrementLives ();
		Assert.IsTrue(levelController.GetLives() == LevelController.LivesLimit);
	}
}
