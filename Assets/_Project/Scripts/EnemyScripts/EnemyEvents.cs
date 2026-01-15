using System;
public class EnemyEvents : Singleton<EnemyEvents>
{
    public event Action OnAllEnemyDestroy;

    public event Action<int> OnEnemyCountChanged;
    int enemyCount = 0;
    public int EnemyCount
    {
        get => enemyCount;
        set
        {
            if (enemyCount == value) return; 
            enemyCount = value;
            OnEnemyCountChanged?.Invoke(enemyCount);
        }
    }

    public void DestroyEnemies()
    {
        OnAllEnemyDestroy?.Invoke();
    }
}
