using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        anim.SetTrigger("die");
        yield return new WaitForSeconds (anim.GetCurrentAnimatorClipInfo(0).Length);

        Destroy(gameObject);
    }
}
