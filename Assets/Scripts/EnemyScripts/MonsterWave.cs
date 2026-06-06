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

    //physical boundaries of desk
    private float minDeskX = -33f;
    private float maxDeskX = 33f;
    private float minDeskY = -10f;
    private float maxDeskY = 10f;

    [Header("Other References")]
    public GameObject finishWaveUI;
    public PlayerAttack player;
    [SerializeField] private InventoryControl inventoryControl;
    [SerializeField] private ItemData heavyAttackItem;
    [SerializeField] private ItemData specialAttackItem;
    public AudioClip finishSound;

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
        enemiesToSpawn.Clear(); 

        AddEnemiesOfDifficulty(0, config.easyEnemies); 
        AddEnemiesOfDifficulty(1, config.mediumEnemies); 

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

    Vector3 GetSpawnInsideDeskBoundaries()
    {
        float randomX = Random.Range(minDeskX, maxDeskX);
        float randomY = Random.Range(minDeskY, maxDeskY);

        return new Vector3(randomX, randomY, 0f);
    }

    IEnumerator SpawnWave()
    {
        WaveConfiguration config = waves[currWave - 1];
        float spawnDelay = config.spawnDelay;

        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            EnemyData data = enemiesToSpawn[i];

            Vector3 spawnPos = GetSpawnInsideDeskBoundaries();
            GameObject obj = Instantiate(data.prefab, spawnPos, Quaternion.identity);

            EnemyHealth health = obj.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.Initialize(Mathf.RoundToInt(data.maxHealth * config.healthMultiplier));
                health.waveManager = this;
            }
            
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
        OtherAudio.Instance.PlaySound(finishSound);

        Debug.Log("Wave " + currWave + " complete!");

        finishWaveUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void NextWave() 
    {
        finishWaveUI.SetActive(false);
        Time.timeScale = 1f;

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