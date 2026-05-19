using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWave : MonoBehaviour
{
    public List<EnemyData> enemyTypes;
    public List<EnemyData> enemiesToSpawn = new List<EnemyData>();

    public int currWave = 1;
    public int waveValue;

    // public Transform spawnLocation;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone!");
            GenerateWave();
            StartCoroutine(SpawnWave());

        }
    }

    void Start()
    {
        //GenerateWave();
        //StartCoroutine(SpawnWave());
    }

    void GenerateWave()
    {
        waveValue = currWave * 10;
        enemiesToSpawn.Clear();

        while (waveValue > 0)
        {
            List<EnemyData> affordable = enemyTypes.FindAll(e => e.cost <= waveValue);

            if (affordable.Count == 0)
                break;

            EnemyData chosen = affordable[Random.Range(0, affordable.Count)];

            enemiesToSpawn.Add(chosen);
            waveValue -= chosen.cost;
        }
    }

    Vector3 GetSpawnAboveCamera(float minX, float maxX)
    {
        Camera cam = Camera.main;

        float randomX = Random.Range(minX, maxX);

        Vector3 top = cam.ViewportToWorldPoint(
            new Vector3(0.5f, 1.1f, cam.nearClipPlane + 10f)
        );

        return new Vector3(randomX, top.y, 0f);
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            EnemyData data = enemiesToSpawn[i];

            Vector3 spawnPos = GetSpawnAboveCamera(-10f, 10f);

            GameObject obj = Instantiate(data.prefab, spawnPos, Quaternion.identity);

            EnemyHealth health = obj.GetComponent<EnemyHealth>();
            health.Initialize(data.maxHealth);

            yield return new WaitForSeconds(5f);
        }

        enemiesToSpawn.Clear();
    }
}
