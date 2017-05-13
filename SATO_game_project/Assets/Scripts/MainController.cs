using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainController : MonoBehaviour
{
	public static bool EnemyWasPlaced = false;
	public static bool CoroutineIsRunning = false;

    protected Enemy enemyModel;
    public GameObject enemyShip;
    public Transform mainBoundary;
    public GameObject enemySpawner;
    public Text waveText;
    public Text countdownText;
    public int initialNumberOfEnemies;
    public float spawnWait;
    public float spawnStartWait;
    public float spawnWaveWait;

	protected int totalEnemiesInWave;
	protected int waveNumber = 1;
	// Doubles each wave.
	static protected int WaveEnemyGrowthRate = 2;

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
        StartCoroutine(StartTimer());
        UpdateWaveDisplay();
		totalEnemiesInWave = initialNumberOfEnemies;
        MoveSpawner();
        StartCoroutine(SpawnEnemies());
		respawnPointController = GameObject.FindObjectOfType<RespawnPointController> ();
		respawnPointController.Respawn ();
    }

	public int TotalEnemiesInWave
	{
		get { return totalEnemiesInWave; }
		set { totalEnemiesInWave = value; }
	}

	public int GetWaveNumber()
	{
		return waveNumber;
	}

	public void IncrementWave()
	{
		++waveNumber;
		AdjustDifficultyOfEnemies ();
        UpdateWaveDisplay();
		totalEnemiesInWave = totalEnemiesInWave * WaveEnemyGrowthRate;
        Debug.Log("Number of enemies in new wave: " + totalEnemiesInWave);
	}

	protected void AdjustDifficultyOfEnemies()
	{
		switch (waveNumber) 
		{
		// Disable idle enemies and enable rotating shooters on wave 2.
		case 2:
			EnemyController.SetMinimumEnemyDifficultyOffset (1);
			EnemyController.SetMaximumEnemyDifficultyOffset (1);
			break;
		// Disable straight shooters and enable homing kamikazes on wave 3.
		case 3:
			EnemyController.SetMinimumEnemyDifficultyOffset (2);
			EnemyController.SetMaximumEnemyDifficultyOffset (0);
			break;
		// Disable straight flying kamikaze enemies on wave 4.
		case 4:
			EnemyController.SetMinimumEnemyDifficultyOffset (3);
			break;
		// TODO consider boss spawn on wave 5?
		}
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
        //enemy ship holds the reference for the enemy object/prefab
        if (enemyShip != null)
        {
            // positionings contains the list with x,z coordinates for each enemy
            //if the list contains another enemy on x axis then regenerate the x coordinate until it is one different
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
            //if the list contains another enemy on z axis then regenerate the z coordinate until it is one different
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
            //if x and z were set then add the enemy position into the list
            else
            {
                Enemy.positionings.Add(spawnPosition.x, spawnPosition.z);
                // checks the spawner object positio if it is ok and now over other enemy, the sync between of the two coroutines are based on the delay set from unity(2seconds)
                if (EnemySpawner.SpawnerInRightPosition)
                {
                    newEnemy = Instantiate(enemyShip, EnemySpawner.spawnerPosition, spawnRotation);
                    //mark that the enemy was placed in the scene so that the spawner continues moving
                    EnemyWasPlaced = true;
                }
                else
                {
                    EnemyWasPlaced = false;
                    MoveSpawner();
                }
			}
            // if statement used for getting information for enemy tests
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
        // first enemy will appear after set timer returns 
		// a delay in the start coroutine caller method
        yield return new WaitForSeconds(spawnStartWait);
        CoroutineIsRunning = true;
		// TODO Change logic to detect player kills and compare 
		//		against wave amount.
        for (int i = 0; i < totalEnemiesInWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnWait);
        }
        CoroutineIsRunning = false;
        //}
    }

    /// <summary>
    /// Method used for calling routine from outside the MainController game object
    /// </summary>
    public void StartFromExternalSourceCoroutine()
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

	public void CheckStatusAndResetWaves()
	{
		if (Enemy.NrOfEnemies == 0 && !MainController.CoroutineIsRunning)
		{
			IncrementWave ();
			var spawner = enemySpawner.GetComponent<EnemySpawner>() as EnemySpawner;
			Enemy.NrOfEnemies = 0;
			spawner.SpawnPointCoroutine();
			this.StartFromExternalSourceCoroutine ();
		}
	}

    protected void UpdateWaveDisplay()
    {
        waveText.text = "Wave: " + waveNumber;
    }

    public void DestroyAllEnemies()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(gameObject => gameObject.name.Contains("Enemy")).ToArray();
        for (int i = 0; i < allObjects.Length; i++)
        {
            //if (allObjects[i].name.Contains("Enemy"))
            //{
               Destroy(allObjects[i]);
            //}
        }
    }

    IEnumerator StartTimer()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.enabled = false;
    }
}
