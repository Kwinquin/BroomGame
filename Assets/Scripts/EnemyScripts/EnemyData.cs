using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public GameObject prefab;
    public int difficulty = 1;
    public int maxHealth;
}
