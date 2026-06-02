//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[System.Serializable]
//public class WaveConfig
//{
//    public int waveValue = 20;
//    public float healthMultiplier = 1f;
//    public float damageMultiplier = 1f;
//    public float spawnDelay = 1f;
//}

//public class MonsterWave : MonoBehaviour
//{
//    [Header("Enemy Types")]
//    public List<EnemyData> enemyTypes;
//    public List<EnemyData> enemiesToSpawn = new List<EnemyData>();

//    [Header("Wave Settings")]
//    public List<WaveConfig> waves = new List<WaveConfig>();

//    [Header("Wave Tracking")]
//    public int enemiesAlive = 0;
//    public bool waveActive = false;

//    [Header("UI")]
//    public GameObject moveChoiceUI;

//    private void Start()
//    {
//        StartCoroutine(StartNextWaveAfterDelay(2f));
//    }

//    IEnumerator StartNextWaveAfterDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);
//        StartWave();
//    }

//    void StartWave()
//    {
//        if (currWave > maxWaves)
//        {
//            Debug.Log("All waves complete!");
//            return;
//        }

//        waveActive = true;
//        GenerateWave();
//        StartCoroutine(SpawnWave());
//    }

//    // -----------------------------
//    // WAVE GENERATION
//    // -----------------------------
//    void GenerateWave()
//    {
//        WaveConfig config = waves[currWave - 1];
//        waveValue = config.waveValue;

//        enemiesToSpawn.Clear();

//        while (waveValue > 0)
//        {
//            List<EnemyData> affordable = enemyTypes.FindAll(e => e.cost <= waveValue);
//            if (affordable.Count == 0)
//                break;

//            EnemyData chosen = affordable[Random.Range(0, affordable.Count)];
//            enemiesToSpawn.Add(chosen);
//            waveValue -= chosen.cost;
//        }
//    }


//    Vector3 GetSpawnAboveCamera(float minX, float maxX)
//    {
//        Camera cam = Camera.main;
//        float randomX = Random.Range(minX, maxX);

//        Vector3 top = cam.ViewportToWorldPoint(
//            new Vector3(0.5f, 1.1f, cam.nearClipPlane + 10f)
//        );

//        return new Vector3(randomX, top.y, 0f);
//    }

//    // -----------------------------
//    // SPAWNING
//    // -----------------------------
//    IEnumerator SpawnWave()
//    {
//        WaveConfig config = waves[currWave - 1];
//        float spawnDelay = config.spawnDelay;

//        for (int i = 0; i < enemiesToSpawn.Count; i++)
//        {
//            EnemyData data = enemiesToSpawn[i];

//            Vector3 spawnPos = GetSpawnAboveCamera(-10f, 10f);
//            GameObject obj = Instantiate(data.prefab, spawnPos, Quaternion.identity);

//            // Apply manual multipliers
//            EnemyHealth health = obj.GetComponent<EnemyHealth>();
//            health.Initialize(Mathf.RoundToInt(data.maxHealth * config.healthMultiplier));
//            health.waveManager = this;

//            EnemyAttack atk = obj.GetComponent<EnemyAttack>();
//            if (atk != null)
//            {
//                atk.attackADamage = Mathf.RoundToInt(atk.attackADamage * config.damageMultiplier);
//                atk.attackBDamage = Mathf.RoundToInt(atk.attackBDamage * config.damageMultiplier);
//                atk.attackCDamage = Mathf.RoundToInt(atk.attackCDamage * config.damageMultiplier);
//            }

//            enemiesAlive++;

//            yield return new WaitForSeconds(spawnDelay);
//        }

//        enemiesToSpawn.Clear();
//    }


//    // -----------------------------
//    // ENEMY DEATH
//    // -----------------------------
//    public void EnemyDied()
//    {
//        enemiesAlive--;

//        if (enemiesAlive <= 0 && waveActive)
//        {
//            WaveComplete();
//        }
//    }

//    // -----------------------------
//    // WAVE COMPLETE
//    // -----------------------------
//    void WaveComplete()
//    {
//        waveActive = false;

//        Debug.Log("Wave " + currWave + " complete!");

//        // Show move choice UI
//        moveChoiceUI.SetActive(true);

//        // Pause gameplay
//        Time.timeScale = 0f;
//    }

//    // Called by UI button after player picks a move
//    public void OnMoveChosen()
//    {
//        moveChoiceUI.SetActive(false);
//        Time.timeScale = 1f;

//        currWave++;

//        if (currWave <= maxWaves)
//        {
//            StartCoroutine(StartNextWaveAfterDelay(2f));
//        }
//        else
//        {
//            Debug.Log("All 5 waves complete!");
//        }
//    }
//}
