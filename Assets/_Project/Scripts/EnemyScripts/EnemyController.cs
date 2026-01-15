using UnityEngine;

public class EnemyController : MonoBehaviour
{
    IHealth health;
    private void Awake()
    {
        health = GetComponent<IHealth>();
    }
    private void OnEnable()
    {
        ++EnemyEvents.Instance.EnemyCount;
        EnemyEvents.Instance.OnAllEnemyDestroy += health.DestroyEnemy;
    }

    private void OnDisable()
    {
        --EnemyEvents.Instance.EnemyCount;
        EnemyEvents.Instance.OnAllEnemyDestroy -= health.DestroyEnemy;
    }

    // more functions later
}
