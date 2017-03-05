using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using Assets.Scripts;
using System.Collections.Generic;

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
        Debug.Log(Enemy.NrOfEnemies);
        Assert.That(Enemy.NrOfEnemies, Is.GreaterThanOrEqualTo(1));

    }

    /// <summary>
    /// Test maximum number of enemies
    /// </summary>
    [Test]
    public void MaximumSpawningEnemies()
    {
        Enemy enemy = new Enemy();
        //Debug.Log(enemy.NrOfEnemies);
        Assert.That(Enemy.NrOfEnemies, Is.LessThanOrEqualTo(7));

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
        Enemy.PositionScaleX = scaleX;
        Enemy.PositionScaleZ = scaleZ;
        Debug.Log(Enemy.PositionScaleX + "/" + expectedX);
        // -7 error scale to take into consideration
        Assert.That(expectedX, Is.EqualTo(Enemy.BoundaryX));
        Assert.That(expectedZ, Is.EqualTo(Enemy.BoundaryZ));
        Assert.True(Enemy.PositionScaleX >= (expectedX / 2) && Enemy.PositionScaleX <= (expectedX - 7));
        Assert.True(Enemy.PositionScaleZ >= (-expectedZ / 2) && Enemy.PositionScaleZ <= expectedZ);

    }
    /// <summary>
    /// Test for enemies to not spawn on top 
    /// </summary>
    /// <param name="expectedX"></param>
    /// <param name="expectedZ"></param>
    [TestCase(33.0f, 18.0f)]
    public void EnemyIsNotOnSamePosition(float expectedX, float expectedZ)
    {
        //Enemy enemy = new Enemy();
        Assert.That(expectedX, Is.EqualTo(Enemy.BoundaryX));
        Assert.That(expectedZ, Is.EqualTo(Enemy.BoundaryZ));
        float xAxis;
        float yAxis;
        float zAxis;
        xAxis = yAxis = zAxis = 0.0f;
        xAxis = Random.Range(expectedX / 2, expectedZ - 7);
        zAxis = Random.Range(-expectedZ / 2, expectedZ / 2);
        Vector3 spawnPosition = new Vector3(xAxis, yAxis, zAxis);
        Dictionary<float, float> positionOfEnemies = new Dictionary<float, float>();
        positionOfEnemies.Add(spawnPosition.x, spawnPosition.z);
        Debug.Log(spawnPosition.x + " !!! " + spawnPosition.z);
        xAxis = Random.Range(expectedX / 2, expectedX - 7);
        zAxis = Random.Range(0, expectedZ / 2);
        Vector3 spawnPositionSecondEnemy = new Vector3(xAxis, yAxis, zAxis);
        Debug.Log(spawnPositionSecondEnemy.x + " *** " + spawnPositionSecondEnemy.z);
        if (positionOfEnemies.ContainsKey(spawnPositionSecondEnemy.x))
        {
            if (positionOfEnemies.ContainsValue(spawnPosition.z))
            {
                xAxis = Random.Range(expectedX / 2, expectedX - 7);
                zAxis = Random.Range(0, expectedZ / 2);
                Vector3 spawnOtherEnemy = new Vector3(xAxis, yAxis, zAxis);
            }
            else
            {
                positionOfEnemies.Add(spawnPositionSecondEnemy.x, spawnPositionSecondEnemy.z);
            }
        }
        else
        {
            positionOfEnemies.Add(spawnPositionSecondEnemy.x, spawnPositionSecondEnemy.z);
        }

    }


}
