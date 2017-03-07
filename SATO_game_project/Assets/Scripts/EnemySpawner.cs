using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // Use this for initialization
    public static bool SpawnerInRightPosition = true;
    public static Vector3 spawnerPosition;
    MainController mainController;
    void Start()
    {
        mainController = GameObject.FindObjectOfType(typeof(MainController)) as MainController;
        MoveSpawnPoint();
        StartCoroutine(MoveSpawner());
    }

    // Update is called once per frame
    void Update()
    {

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
        while (true)
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
}
