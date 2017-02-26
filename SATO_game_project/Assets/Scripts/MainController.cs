using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{

    // Use this for initialization
    protected Enemy enemyModel;
    public GameObject EnemyShip;

    public Vector3 spawnCoordinates;

    /// <summary>
    /// Called before Start, use usually for initialisations of model objects
    /// </summary>
    void Awake()
    {
        enemyModel = new Enemy();
    }

    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
       //TODO 
    }


    /// <summary>
    /// Spawn single enemy on scene
    /// </summary>
    protected void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnCoordinates.x, spawnCoordinates.x), spawnCoordinates.y, spawnCoordinates.z);
        Quaternion spawnRotation = new Quaternion();
        // need to use Euler Angles to set up the rotation of the object
        Vector3 eulerAngles = new Vector3(0, 0, 90);
        spawnRotation = Quaternion.Euler(eulerAngles);

        GameObject newEnemy = Instantiate(EnemyShip, spawnPosition, spawnRotation);
        if (newEnemy != null)
        {
            // !!!Run the test after running the scene, the increased number still persists
            enemyModel.increaseEnemies();
            enemyModel.PositionScaleX = spawnPosition.x;
            enemyModel.PositionScaleZ = spawnPosition.z;
        }
    }
}
