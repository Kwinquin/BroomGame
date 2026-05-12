using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWave : MonoBehaviour
{
    public List<EnemyData> enemyTypes;          // ScriptableObject list
    public List<EnemyData> enemiesToSpawn = new List<EnemyData>();

    public int currWave = 1;
    public int waveValue;

    public Transform spawnLocation;

    void Start()
    {
        GenerateWave();
        StartCoroutine(SpawnWave());
    }

    void GenerateWave()
    {
        waveValue = currWave * 10;
        enemiesToSpawn.Clear();

        while (waveValue > 0)
        {
            int randomIndex = Random.Range(0, enemyTypes.Count);
            EnemyData chosen = enemyTypes[randomIndex];

            if (waveValue - chosen.cost >= 0)
            {
                enemiesToSpawn.Add(chosen);
                waveValue -= chosen.cost;
            }
            else
            {
                break;
            }
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            EnemyData data = enemiesToSpawn[i];

            GameObject obj = Instantiate(data.prefab, spawnLocation.position, Quaternion.identity);

            EnemyHealth health = obj.GetComponent<EnemyHealth>();
            health.Initialize(data.maxHealth);

            yield return new WaitForSeconds(5f);
        }

        enemiesToSpawn.Clear();
    }

}
