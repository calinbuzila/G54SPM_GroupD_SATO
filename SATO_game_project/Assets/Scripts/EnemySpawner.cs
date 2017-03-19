using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static bool SpawnerInRightPosition = true;
    public static Vector3 spawnerPosition;
    MainController mainController;
    void Start()
    {
        mainController = GameObject.FindObjectOfType(typeof(MainController)) as MainController;
        SpawnPointCoroutine();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().name == "Enemy")
        {
            SpawnerInRightPosition = false;
            MoveSpawnPoint();
        }
    }

    public IEnumerator MoveSpawner()
    {
        while (Enemy.NrOfEnemies > 0)
        {
            yield return new WaitUntil(() => MainController.EnemyWasPlaced);
            SpawnerInRightPosition = true;
            MoveSpawnPoint();
            yield return new WaitForSeconds(2);
        }
    }

    public void MoveSpawnPoint()
    {
        SpawnerInRightPosition = true;
        this.transform.position = mainController.MoveSpawner();
        spawnerPosition = this.transform.position;
    }

    public void SpawnPointCoroutine()
	{
		MoveSpawnPoint();
		StartCoroutine(MoveSpawner());
	}
}
