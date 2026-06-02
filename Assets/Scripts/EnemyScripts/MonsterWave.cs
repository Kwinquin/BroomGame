using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class WaveConfiguration
{
    public int WaveNumber = 1; //just for reference
    [HideInInspector] public int enemiesToKill = 5;

    [Header("Enemy Amounts")]
    public int easyEnemies = 0;
    public int mediumEnemies = 0;
    // public int hardEnemies = 0; // if we wanted to add more types

    [Header("Multipliers")]
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
    public List<WaveConfiguration> waves = new List<WaveConfiguration>();

    [Header("Wave Tracking")]
    public int currWave = 1;
    public bool waveActive = false;

    public int totalEnemiesThisWave = 0;
    public int deadEnemiesThisWave = 0;

    [Header("Other References")]
    public GameObject finishWaveUI;
    public PlayerAttack player;
    public float spawnRadius = 18f;
    [SerializeField] private InventoryControl inventoryControl;
    [SerializeField] private ItemData heavyAttackItem;
    [SerializeField] private ItemData specialAttackItem;

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
        WaveConfiguration config = waves[currWave - 1];

        enemiesToSpawn.Clear(); //don't count enemies from previous wave

        AddEnemiesOfDifficulty(0, config.easyEnemies); //easy is 0
        AddEnemiesOfDifficulty(1, config.mediumEnemies); //medium is 1

        totalEnemiesThisWave = enemiesToSpawn.Count;
        deadEnemiesThisWave = 0;

        Debug.Log($"Wave {currWave} generated. Total required kills: {totalEnemiesThisWave}");
    }

    void AddEnemiesOfDifficulty(int difficulty, int count)
    {
        List<EnemyData> matches = enemyTypes.FindAll(e => e.difficulty == difficulty);

        for (int i = 0; i < count; i++)
        {
            EnemyData chosen = matches[Random.Range(0, matches.Count)];
            enemiesToSpawn.Add(chosen);
        }
    }


    //this is setting the outside of camera-view radius in which the enemies can spawn in
    Vector3 GetSpawnOnRadiusOutsideCamera()
    {
        Camera cam = Camera.main;
        Vector3 cameraPos = cam.transform.position;

        float randomAngle = Random.Range(0f, Mathf.PI * 2f);

        //these are like the coordinates on a unit circle, makes me sick
        float spawnX = Mathf.Cos(randomAngle) * spawnRadius;
        float spawnY = Mathf.Sin(randomAngle) * spawnRadius;

        Vector3 spawnPosition = new Vector3(cameraPos.x + spawnX, cameraPos.y + spawnY, 0f);

        return spawnPosition;
    }


    IEnumerator SpawnWave()
    {
        WaveConfiguration config = waves[currWave - 1];
        float spawnDelay = config.spawnDelay;

        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            EnemyData data = enemiesToSpawn[i];

            Vector3 spawnPos = GetSpawnOnRadiusOutsideCamera();
            GameObject obj = Instantiate(data.prefab, spawnPos, Quaternion.identity);

            // Apply a multiplier depending on the wave we are in, instead of many types of enemies the enemies upgrade per wave
            EnemyHealth health = obj.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.Initialize(Mathf.RoundToInt(data.maxHealth * config.healthMultiplier));
                health.waveManager = this;
            }
            //same thing with the damage the enemies deal
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
    }

  //------------------------------------------------------------------------------------------------
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

        finishWaveUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void NextWave() //this will be called by a button
    {
        finishWaveUI.SetActive(false);
        Time.timeScale = 1f;

        // unlocking new attack moves
        if (currWave == 2)
        {
            player.heavyUnlocked = true;
            inventoryControl.AddInInventory(heavyAttackItem);
            Debug.Log("Heavy Attack Unlocked!");
        }
        else if (currWave == 3)
        {
            player.specialUnlocked = true;
            inventoryControl.AddInInventory(specialAttackItem);
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
            SceneManager.LoadScene("WinScene");
        }
    }
}