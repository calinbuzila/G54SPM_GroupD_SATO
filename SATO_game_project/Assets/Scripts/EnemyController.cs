using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	static protected string DeletionTimerScriptString = "SelfDeletionTimer";
	static protected System.Type SelfDeletionScriptType = 
		System.Type.GetType(DeletionTimerScriptString + ",Assembly-CSharp");
	static protected int NumBehaviours = (int)System.Enum.GetNames(typeof(Behaviours)).Length;
	static protected int MinimumEnemyDifficultyOffset = 0;
	static protected int MaximumEnemyDifficultyOffset = 3;
	static public int EnemiesDestroyedByPlayer = 0;

	protected ColourController colourController;
	protected LevelController levelController;
	protected int randomBehaviourNumber;

    public MainController mainController;
    public EnemySpawner enemySpawner;
    public Transform enemyTransform;
	public Transform playerTransform;
	// Ordered by difficulty!
	public enum Behaviours { IdleTarget, Shooter, Kamikaze, RotatingShooter, HomingKamikaze };

	// Kamikaze related variables.
	protected bool isHoming = true;
	public float kamikazeSpeed;
	public float homingSpeed;
	public int homingTime;

	// Shooter related variables.
	protected float nextFire;
	public Transform enemyShotSpawn;
	public GameObject enemyBullet;
	public float fireRate;

    void Start ()
	{
		colourController = GameObject.FindObjectOfType<ColourController> ();
		mainController = GameObject.FindObjectOfType<MainController> ();
		levelController = GameObject.FindObjectOfType<LevelController> ();

		colourController.AssignRandomColour (gameObject);
		randomBehaviourNumber = Random.Range (MinimumEnemyDifficultyOffset, NumBehaviours - MaximumEnemyDifficultyOffset);
		// Attaches the SelfDeletionTimer script to any kamikaze enemies that spawn.
		if (randomBehaviourNumber == (int)Behaviours.Kamikaze
		    || randomBehaviourNumber == (int)Behaviours.HomingKamikaze) 
		{
			gameObject.AddComponent(SelfDeletionScriptType);
		}
    }

	public int getEnemyBehaviourNumber()
	{
		return randomBehaviourNumber;
	}

    void Update()
    {
        switch (randomBehaviourNumber)
        {
            case (int)Behaviours.Shooter:
                ShootAttack();
			break;
			case (int)Behaviours.Kamikaze:
				KamikazeAttack();
				break;
            case (int)Behaviours.RotatingShooter:
                RotateToPlayer();
                ShootAttack();
                break;
            case (int)Behaviours.HomingKamikaze:
                if (isHoming)
                {
                    HomingKamikaze();
                    StartCoroutine(ResetHoming());
                }
                break;
        }
    }

	static public void IncrementPlayerKills()
	{
		++EnemiesDestroyedByPlayer;
	}

	static public void SetMinimumEnemyDifficultyOffset(int offsetValue)
	{
		MinimumEnemyDifficultyOffset = offsetValue;
	}

	static public void SetMaximumEnemyDifficultyOffset(int offsetValue)
	{
		MaximumEnemyDifficultyOffset = offsetValue;
	}

    /// <summary>
    /// Method called when an objects collision mesh collides with the meshes of other game objects.
    /// </summary>
    /// <param name="other">The collider of the other object that the object this script is attached to just hit</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().name.Contains("Player"))
        {
            //var playerIsRespawning = other.GetComponent<Renderer>().enabled;
            if (!LevelController.playerIsRespawning)
            {
                levelController.AddToHealth(-20 * (mainController.GameDifficulty + 1));
                Destroy(gameObject);
				IncrementPlayerKills();
            }
            else
            {
                this.isHoming = false;
                RestoreRotation();
                KamikazeAttack();
            }
        }
    }

    protected IEnumerator SpawnRoutineEndedForCurrentWave()
    {
        yield return new WaitForSeconds(1);
        RestartRoutines();
    }

    protected IEnumerator ResetHoming()
    {
        yield return new WaitForSeconds(homingTime);
        this.isHoming = false;
        // need to use Euler Angles to set up the rotation of the object
        RestoreRotation();
        KamikazeAttack();
    }

    protected void RestoreRotation()
    {
        Vector3 eulerAngles = new Vector3(0, 0, 90);
        enemyTransform.rotation = Quaternion.Euler(eulerAngles);
    }

    protected void KamikazeAttack()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-10.0f, 0.0f, transform.position.z), kamikazeSpeed * Time.deltaTime);
    }

    protected void RotateToPlayer()
    {
        var player = GameObject.Find("Player(Clone)");
		if (player == null) 
		{
			return;
		}
        playerTransform = player.GetComponent<Transform>();

        enemyTransform.LookAt(playerTransform);
        enemyTransform.Rotate(Vector3.right, 90);
    }

    protected void ShootAttack()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(enemyBullet, enemyShotSpawn.position, enemyShotSpawn.rotation);
        }
    }

    protected void HomingKamikaze()
    {
        var player = GameObject.Find("Player(Clone)");
        if (player != null)
        {
            var playerTransform = player.GetComponent<Transform>();
            enemyTransform.LookAt(playerTransform);
            enemyTransform.Rotate(Vector3.right, 90);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z), homingSpeed * Time.deltaTime);
        }
    }

    void OnDestroy()
    {
        Enemy.NrOfEnemies -= 1;
        RestartRoutines();
    }

    protected void RestartRoutines()
    {
        if (EnemiesDestroyedByPlayer == mainController.TotalEnemiesInWave)
        {
            if (Enemy.NrOfEnemies == 0 && levelController.GetLives() != 0)
            {
                enemySpawner = GameObject.FindObjectOfType(typeof(EnemySpawner)) as EnemySpawner;
                Enemy.NrOfEnemies = 0;
                mainController.IncrementWave();
                enemySpawner.SpawnPointCoroutine();
                mainController.StartFromExternalSourceCoroutine();
            }
            EnemiesDestroyedByPlayer = 0;
        }
    }
}
