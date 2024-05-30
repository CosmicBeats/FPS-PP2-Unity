using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyToSpawn;
    [SerializeField] int numberToSpawn;
    [SerializeField] float spawnTimer;
    [SerializeField] Transform[] spawnPos;

    int spawnCount;
    bool isSpawning;
    bool startSpawning;
        // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.UpdateGameGoalWin(numberToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if(startSpawning &&  !isSpawning && spawnCount < numberToSpawn)
        {
            StartCoroutine(Spawn());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            startSpawning = true;
        }
    }

    IEnumerator Spawn()
    {
        isSpawning = true;

        int arrayPosition = Random.Range(0, spawnPos.Length);

        int enemyIndex = Random.Range(0, enemyToSpawn.Length);

        Instantiate(enemyToSpawn[enemyIndex], spawnPos[arrayPosition].position, spawnPos[arrayPosition].rotation);
        spawnCount++;

        yield return new WaitForSeconds(spawnTimer);
        isSpawning = false;
    }
}