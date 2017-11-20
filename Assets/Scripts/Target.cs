using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    void Start()
    {

    }

    void Update()
    {

    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
