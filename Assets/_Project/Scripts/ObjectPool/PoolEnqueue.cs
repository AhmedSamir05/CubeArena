using UnityEngine;

public class PoolEnqueue : MonoBehaviour
{
    public GameObject parentPrefab;
    private void OnDisable()
    {
        PoolManager.Instance.Destroy(gameObject, parentPrefab);
    }


}
