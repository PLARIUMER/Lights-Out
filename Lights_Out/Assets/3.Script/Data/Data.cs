using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/PlayerData")]
public class Data : ScriptableObject
{
    public int level;
    public int gold;
    public int Health;
    public float speed;
    public float damage;
    public int defense;
}
