using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    public int enemyCount=40;

    public GameObject[] enemyPrefabs;

    public float spawningSpeed = 10;
}
