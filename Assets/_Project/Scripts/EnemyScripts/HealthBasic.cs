using UnityEngine;

public class HealthBasic : MonoBehaviour, IHealth
{
    [SerializeField] int characterHealth=1;
    [SerializeField] GameObject destroyEffect;
    int health;

    private void OnEnable()
    {
        health = characterHealth;
    }
    public void OnHit(int hitValue = 1)
    {
        health -= hitValue;
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        PoolManager.Instance.Instantiate(destroyEffect, transform.position, destroyEffect.transform.rotation);
        gameObject.SetActive(false);
    }
}
