using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWave : MonoBehaviour
{
    [Header("Enemy Types")]
    public List<EnemyData> enemyTypes;
    public List<EnemyData> enemiesToSpawn = new List<EnemyData>();

    [Header("Wave Settings")]
    public int currWave = 1;
    public int maxWaves = 5;
    public int waveValue;

    [Header("Difficulty Scaling")]
    public float healthScalePerWave = 0.25f;   // +25% health per wave
    public float damageScalePerWave = 0.15f;   // +15% damage per wave
    public float spawnSpeedIncrease = 0.2f;    // faster spawns each wave

    [Header("Wave Tracking")]
    public int enemiesAlive = 0;
    public bool waveActive = false;

    [Header("UI")]
    public GameObject moveChoiceUI;

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
        if (currWave > maxWaves)
        {
            Debug.Log("All waves complete!");
            return;
        }

        waveActive = true;
        GenerateWave();
        StartCoroutine(SpawnWave());
    }

    // -----------------------------
    // WAVE GENERATION
    // -----------------------------
    void GenerateWave()
    {
        // Difficulty scaling: waveValue grows faster each wave
        waveValue = Mathf.RoundToInt(currWave * 12 + Mathf.Pow(currWave, 1.3f) * 5);

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

    // -----------------------------
    // SPAWNING
    // -----------------------------
    IEnumerator SpawnWave()
    {
        float spawnDelay = Mathf.Max(0.4f, 1.5f - currWave * spawnSpeedIncrease);

        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            EnemyData data = enemiesToSpawn[i];

            Vector3 spawnPos = GetSpawnAboveCamera(-10f, 10f);
            GameObject obj = Instantiate(data.prefab, spawnPos, Quaternion.identity);

            // Scale enemy stats
            float healthMult = 1f + (currWave - 1) * healthScalePerWave;
            float damageMult = 1f + (currWave - 1) * damageScalePerWave;

            EnemyHealth health = obj.GetComponent<EnemyHealth>();
            health.Initialize(Mathf.RoundToInt(data.maxHealth * healthMult));
            health.waveManager = this;

            EnemyAttack atk = obj.GetComponent<EnemyAttack>();
            if (atk != null)
            {
                atk.attackADamage = Mathf.RoundToInt(atk.attackADamage * damageMult);
                atk.attackBDamage = Mathf.RoundToInt(atk.attackBDamage * damageMult);
                atk.attackCDamage = Mathf.RoundToInt(atk.attackCDamage * damageMult);
            }

            enemiesAlive++;

            yield return new WaitForSeconds(spawnDelay);
        }

        enemiesToSpawn.Clear();
    }

    // -----------------------------
    // ENEMY DEATH
    // -----------------------------
    public void EnemyDied()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0 && waveActive)
        {
            WaveComplete();
        }
    }

    // -----------------------------
    // WAVE COMPLETE
    // -----------------------------
    void WaveComplete()
    {
        waveActive = false;

        Debug.Log("Wave " + currWave + " complete!");

        // Show move choice UI
        moveChoiceUI.SetActive(true);

        // Pause gameplay
        Time.timeScale = 0f;
    }

    // Called by UI button after player picks a move
    public void OnMoveChosen()
    {
        moveChoiceUI.SetActive(false);
        Time.timeScale = 1f;

        currWave++;

        if (currWave <= maxWaves)
        {
            StartCoroutine(StartNextWaveAfterDelay(2f));
        }
        else
        {
            Debug.Log("All 5 waves complete!");
        }
    }
}
