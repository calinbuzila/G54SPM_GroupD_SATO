using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ScoreTests {

	[Test]
	public void ScoreInitialisesToZero() {
		//Arrange
		LevelController levelController = new LevelController();

		Assert.AreEqual(0, levelController.GetScore());
	}
}
