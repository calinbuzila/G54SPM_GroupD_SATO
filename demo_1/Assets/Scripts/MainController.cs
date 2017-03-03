using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{

    // Use this for initialization
    protected Enemy enemyModel;
    public GameObject enemyShip;
    public Transform mainBoundary;

    public int nrOfEnemies;
    public float spawnWait;
    public float spawnStartWait;
    public float spawnWaveWait;


    /// <summary>
    /// Called before Start, use usually for initialisations of model objects
    /// </summary>
    void Awake()
    {
        enemyModel = new Enemy();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //for key press C it spawns an enemy
    void FixedUpdate()
    {
        bool spawnKey = Input.GetKeyDown(KeyCode.C);
        if (spawnKey)
        {
            StartCoroutine(SpawnEnemies());
        }
    }


    /// <summary>
    /// Spawn single enemy on scene
    /// </summary>
    protected void SpawnEnemy()
    {
        float xAxis;
        float yAxis;
        float zAxis;
        xAxis = yAxis = zAxis = 0.0f;

        // get the boundaries for the Main Boundary game object
        if (mainBoundary != null)
        {
            xAxis = Random.Range(mainBoundary.localScale.x / 2, mainBoundary.localScale.x -7);
            zAxis = Random.Range(0, mainBoundary.localScale.z / 2);
            //Debug.Log(xAxis/2 + "XAXIS");
        }

        Vector3 spawnPosition = new Vector3(xAxis, yAxis, zAxis);
        Quaternion spawnRotation = new Quaternion();
        // need to use Euler Angles to set up the rotation of the object
        Vector3 eulerAngles = new Vector3(0, 0, 90);
        spawnRotation = Quaternion.Euler(eulerAngles);
        if (enemyShip != null)
        {

            GameObject newEnemy = Instantiate(enemyShip, spawnPosition, spawnRotation);
            if (newEnemy != null)
            {
                // !!!Run the test after running the scene, the increased number still persists
                enemyModel.increaseEnemies();
                enemyModel.Name = "Enemy";
                enemyModel.BoundaryX = mainBoundary.localScale.x;
                enemyModel.BoundaryZ = mainBoundary.localScale.z;
                newEnemy.name = enemyModel.Name;
                enemyModel.PositionScaleX = spawnPosition.x;
                enemyModel.PositionScaleZ = spawnPosition.z;
            }
        }
    }

    protected IEnumerator SpawnEnemies()
    {
        //while (true)
        //{
            // first enemy will appear after set timer returns a delay in the start coroutine caller method
            yield return new WaitForSeconds(spawnStartWait);

            for (int i = 0; i < nrOfEnemies; i++)
            {
                SpawnEnemy();
                //after spawning first it returns a delay into the caller method: start coroutine
                yield return new WaitForSeconds(spawnWait);
            }

        //}
    }
}
