using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;
    public float healAmount = 1f;
    public float healSpeed = 5f;
    [SerializeField] private bool mainCharacter;

    
    private bool isDead = false;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
        if (mainCharacter)
            Debug.Log("Main character took damage, current health: " + health);
        else Debug.Log("Enemy took damage, current health: " + health);
    }

    // razmisli da li treba da se u posebnoj niti proverava da li je lik crko
    private void Die()
    {
        isDead = true;
        if (mainCharacter)
            Debug.Log("Main character just died.");
        else Debug.Log("Enemy just died.");

        Destroy(gameObject);
        // some kind of restart
    }

    private IEnumerator Heal()
    {
        while (!isDead)
        {
            if (health < maxHealth)
            {
                health += healAmount;
                // Debug.Log("Healing. Current health: " + health);
            }
            yield return new WaitForSeconds(healSpeed);
        }
    }

    void Start()
    {
        StartCoroutine(Heal());
    }
}
