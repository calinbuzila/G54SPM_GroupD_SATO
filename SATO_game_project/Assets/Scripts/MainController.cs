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
    public GameObject enemySpawner;
    public static bool EnemyWasPlaced = false;
    

    public int nrOfEnemies;
    public float spawnWait;
    public float spawnStartWait;
    public float spawnWaveWait;

	protected RespawnPointController respawnPointController;


    /// <summary>
    /// Called before Start, use usually for initialisations of model objects
    /// </summary>
    void Awake()
    {
        enemyModel = new Enemy();
    }

    void Start()
    {
        MoveSpawner();
        StartCoroutine(SpawnEnemies());
		respawnPointController = GameObject.FindObjectOfType<RespawnPointController> ();
		respawnPointController.Respawn ();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //for key press C it spawns an enemy
    void FixedUpdate()
    {
        //bool spawnKey = Input.GetKeyDown(KeyCode.C);
        //if (spawnKey)
        //{
        //    StartCoroutine(SpawnEnemies());
        //}
    }


    /// <summary>
    /// Spawn single enemy on scene
    /// </summary>
    public void SpawnEnemy()
    {
        float xAxis;
        float yAxis;
        float zAxis;
        xAxis = yAxis = zAxis = 0.0f;

        // get the boundaries for the Main Boundary game object
        if (mainBoundary != null)
        {
            xAxis = Random.Range(mainBoundary.localScale.x / 2, mainBoundary.localScale.x - 7);
            zAxis = Random.Range((-mainBoundary.localScale.z / 2), mainBoundary.localScale.z / 2);
           
        }

        Vector3 spawnPosition = new Vector3(xAxis, yAxis, zAxis);
        Quaternion spawnRotation = new Quaternion();
        // need to use Euler Angles to set up the rotation of the object
        Vector3 eulerAngles = new Vector3(0, 0, 90);
        spawnRotation = Quaternion.Euler(eulerAngles);
        GameObject newEnemy = null;
        if (enemyShip != null)
        {
            if (Enemy.positionings.ContainsKey(spawnPosition.x))
            {
                while (Enemy.positionings.ContainsKey(spawnPosition.x))
                {
                    xAxis = Random.Range(mainBoundary.localScale.x / 2, mainBoundary.localScale.x - 7);
                    while (Enemy.positionings.ContainsValue(spawnPosition.z))
                    {
                        zAxis = Random.Range((-mainBoundary.localScale.z / 2), mainBoundary.localScale.z / 2);
                    }
                }
            }
            else if (Enemy.positionings.ContainsValue(spawnPosition.z))
            {
                while (Enemy.positionings.ContainsKey(spawnPosition.z))
                {
                    zAxis = Random.Range((-mainBoundary.localScale.z / 2), mainBoundary.localScale.z / 2);
                    while (Enemy.positionings.ContainsKey(spawnPosition.x))
                    {
                        xAxis = Random.Range(mainBoundary.localScale.x / 2, mainBoundary.localScale.x - 7);
                    }
                }
            }
            else
            {
                Enemy.positionings.Add(spawnPosition.x, spawnPosition.z);
              
                if (EnemySpawner.SpawnerInRightPosition)
                {
                    newEnemy = Instantiate(enemyShip, EnemySpawner.spawnerPosition, spawnRotation);
                    EnemyWasPlaced = true;
                }

                else
                {
                    EnemyWasPlaced = false;
                    MoveSpawner();
                }
                
            }

            if (newEnemy != null)
            {
                // !!!Run the test after running the scene, the increased number still persists
				Enemy.NrOfEnemies += 1;
                enemyModel.Name = "Enemy";
                Enemy.BoundaryX = mainBoundary.localScale.x;
                Enemy.BoundaryZ = mainBoundary.localScale.z;
                newEnemy.name = enemyModel.Name;
                Enemy.PositionScaleX = spawnPosition.x;
                Enemy.PositionScaleZ = spawnPosition.z;
               
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


    /// <summary>
    /// Method used for calling routine from outside the MainController game object
    /// </summary>
    public void StartFromExternalSourceCouroutine()
    {
        StartCoroutine(SpawnEnemies());
    }

    public Vector3 MoveSpawner()
    {
        float xAxis;
        float yAxis;
        float zAxis;
        xAxis = yAxis = zAxis = 0.0f;

        // get the boundaries for the Main Boundary game object
        if (mainBoundary != null)
        {
            xAxis = Random.Range(mainBoundary.localScale.x / 2, mainBoundary.localScale.x - 7);
            zAxis = Random.Range((-mainBoundary.localScale.z / 2), mainBoundary.localScale.z / 2);
            
        }

        Vector3 spawnPosition = new Vector3(xAxis, yAxis, zAxis);
        return spawnPosition;

    }
}
