using UnityEngine;
using UnityEngine.UI;
public class EnemyUIDestroyer : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(DestroyEnemies);
    }

    private void DestroyEnemies()
    {
        EnemyEvents.Instance.DestroyEnemies();
    }
}
