using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Move")]
public class MoveData : ScriptableObject
{
    public string moveName;
    public int damage;
    public float attackRange;
    public float cooldown;
    public AnimationClip animation;
}
