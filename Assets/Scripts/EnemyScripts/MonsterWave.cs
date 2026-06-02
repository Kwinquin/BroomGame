using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveConfig
{
    public int WaveNumber = 1;
    public int waveValue = 20;
    public float healthMultiplier = 1f;
    public float damageMultiplier = 1f;
    public float spawnDelay = 1f;
}

public class MonsterWave : MonoBehaviour
{
    [Header("Enemy Types")]
    public List<EnemyData> enemyTypes;
    public List<EnemyData> enemiesToSpawn = new List<EnemyData>();

    [Header("Wave Settings")]
    public List<WaveConfig> waves = new List<WaveConfig>();

    [Header("Wave Tracking")]
    public int currWave = 1;
    public int waveValue = 0;
    public bool waveActive = false;

    private int totalEnemiesThisWave = 0;
    private int deadEnemiesThisWave = 0;


    [Header("UI")]
    public GameObject moveChoiceUI;

    public PlayerAttack player;   // assign in Inspector
    private bool spawningFinished = false;



    private void Start()
    {
        StartCoroutine(StartNextWaveAfterDelay(2f));
    }

    IEnumerator StartNextWaveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartWave();
    }

    void StartWave()
    {
        if (currWave > waves.Count)
        {
            Debug.Log("All waves complete!");
            return;
        }

        waveActive = true;
        GenerateWave();
        StartCoroutine(SpawnWave());
    }

    void GenerateWave()
    {
        WaveConfig config = waves[currWave - 1];
        waveValue = config.waveValue;

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

        // NEW: total enemies for this wave
        totalEnemiesThisWave = enemiesToSpawn.Count;
        deadEnemiesThisWave = 0;
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
        spawningFinished = false;

        WaveConfig config = waves[currWave - 1];
        float spawnDelay = config.spawnDelay;

        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            EnemyData data = enemiesToSpawn[i];

            Vector3 spawnPos = GetSpawnAboveCamera(-10f, 10f);
            GameObject obj = Instantiate(data.prefab, spawnPos, Quaternion.identity);

            // Apply manual multipliers
            EnemyHealth health = obj.GetComponent<EnemyHealth>();
            health.Initialize(Mathf.RoundToInt(data.maxHealth * config.healthMultiplier));
            health.waveManager = this;

            EnemyAttack atk = obj.GetComponent<EnemyAttack>();
            if (atk != null)
            {
                atk.attackADamage = Mathf.RoundToInt(atk.attackADamage * config.damageMultiplier);
                atk.attackBDamage = Mathf.RoundToInt(atk.attackBDamage * config.damageMultiplier);
                atk.attackCDamage = Mathf.RoundToInt(atk.attackCDamage * config.damageMultiplier);
            }

            yield return new WaitForSeconds(spawnDelay);
        }

        enemiesToSpawn.Clear();

        spawningFinished = true;

    }

    public void EnemyDied()
    {
        deadEnemiesThisWave++;

        if (deadEnemiesThisWave >= totalEnemiesThisWave && waveActive)
        {
            WaveComplete();
        }
    }



    void WaveComplete()
    {
        waveActive = false;

        Debug.Log("Wave " + currWave + " complete!");

        moveChoiceUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnMoveChosen()
    {
        moveChoiceUI.SetActive(false);
        Time.timeScale = 1f;

        // Unlock moves based on wave progression
        if (currWave == 1)
        {
            player.heavyUnlocked = true;
            Debug.Log("Heavy Attack Unlocked!");
        }
        else if (currWave == 2)
        {
            player.specialUnlocked = true;
            Debug.Log("Special Attack Unlocked!");
        }

        currWave++;

        if (currWave <= waves.Count)
        {
            StartCoroutine(StartNextWaveAfterDelay(2f));
        }
        else
        {
            Debug.Log("All waves complete!");
        }
    }

}
