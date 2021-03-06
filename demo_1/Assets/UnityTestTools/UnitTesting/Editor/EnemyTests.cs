﻿using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using Assets.Scripts;

[TestFixture]
public class EnemyTests
{

    [Test]
    public void EditorTest()
    {
        //Arrange
        var gameObject = new GameObject();

        //Act
        //Try to rename the GameObject
        var newGameObjectName = "My game object";
        gameObject.name = newGameObjectName;

        //Assert
        //The object has a new name
        Assert.AreEqual(newGameObjectName, gameObject.name);
    }

    /// <summary>
    /// Test if a single enemy exists 
    /// </summary>
    [Test]
    public void MinimumSpawningEnemies()
    {
        Enemy enemy = new Enemy();
        Debug.Log(enemy.NrOfEnemies);
        Assert.That(enemy.NrOfEnemies, Is.GreaterThanOrEqualTo(1));

    }

    /// <summary>
    /// Test maximum number of enemies
    /// </summary>
    [Test]
    public void MaximumSpawningEnemies()
    {
        Enemy enemy = new Enemy();
        //Debug.Log(enemy.NrOfEnemies);
        Assert.That(enemy.NrOfEnemies, Is.LessThanOrEqualTo(7));

    }


    /// <summary>
    /// Test for enemies to be spawned only in the MainBoundary coordinates divided by 2: 19.5 and 10.0
    /// if it returns true then the test failed
    /// </summary>
    /// <param name="scaleX"></param>
    /// <param name="scaleZ"></param>
    /// <param name="expectedX"></param>
    /// <param name="expectedZ"></param>
    [TestCase(25.0f, 13.0f, 33.0f, 18.0f)]
    [TestCase(25.0f, 13.0f, 33.0f, 18.0f)]
    public void SpawnInDesignatedArea(float scaleX, float scaleZ, float expectedX, float expectedZ)
    {
        Enemy enemy = new Enemy();
        enemy.PositionScaleX = scaleX;
        enemy.PositionScaleZ = scaleZ;
        Debug.Log(enemy.PositionScaleX + "/" + expectedX);
        // -7 error scale to take into consideration
        Assert.That(expectedX, Is.EqualTo(enemy.BoundaryX));
        Assert.That(expectedZ, Is.EqualTo(enemy.BoundaryZ));
        Assert.True(enemy.PositionScaleX >= (expectedX / 2) && enemy.PositionScaleX <= (expectedX - 7));
        Assert.True(enemy.PositionScaleZ >= 0 && enemy.PositionScaleZ <= expectedZ);

    }


}
