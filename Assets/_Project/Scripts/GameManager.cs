using UnityEngine;
using System;
using System.Runtime.Serialization;
[DefaultExecutionOrder(-500)]
public class GameManager : Singleton<GameManager>
{
    public event Action<int> OnNewWave;
    public event Action OnReset;
    // true is paused, false is resume
    public event Action<bool> OnPauseChanged;

    [HideInInspector, NonSerialized] public int currentWave=1;

    private void Start()
    {
        EnemyEvents.Instance.OnEnemyCountChanged += CheckEnemyCount;
        NewWave(currentWave);
    }

    private void CheckEnemyCount(int counter)
    {
        if (counter == 0 && EnemySpawner.Instance.isSpawningComplete)
        {
            NewWave(currentWave + 1);
        }
    }


    public void NewWave(int waveNum)
    {
        if (waveNum <= 0)
            return;

        currentWave = waveNum;
        EnemyEvents.Instance.DestroyEnemies();
        OnNewWave?.Invoke(waveNum);
    }

    bool isPaused=false;
    public void ChangePauseStatus()
    {
        isPaused = !isPaused;
        OnPauseChanged?.Invoke(isPaused);
    }

}
