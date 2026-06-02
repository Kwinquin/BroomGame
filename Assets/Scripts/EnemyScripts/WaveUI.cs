using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [Header("References")]
    public MonsterWave waveManager;
    public TextMeshProUGUI waveText;

    void Update()
    {
        if (waveManager == null) return;

        waveText.text = "Wave " + waveManager.currWave;
    }
}
