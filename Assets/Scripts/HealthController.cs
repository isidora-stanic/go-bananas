using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class <c>HealthController</c> manages health, healing, taking damage and diying of a character.
/// </summary>
public class HealthController : MonoBehaviour
{
    
    enum TypeOfCharacter {Main, Enemy, Side};

    [Header("Health and Healing properties")]
    [SerializeField] private float maximumHealth = 100f;
    [SerializeField] private float health = 100f;
    [SerializeField] private float healAmount = 1f;
    [SerializeField] private float healSpeed = 5f;

    [Header("Special indicators")]
    [SerializeField] private TypeOfCharacter characterType;
    [SerializeField] private bool indestructible;
    [SerializeField] private float indestructibleTime = 20f;
    private bool isDead = false;
    public bool IsDead { get { return isDead; } } // another class can only get this value


    /// <summary>
    /// Method <c>TakeDamage</c> decreases health by given damage.
    /// Checks if character has no more health and calls Die method if so.
    /// If a character is indestructuble, it doesn't do anything.
    /// </summary>
    public void TakeDamage(float damage)
    {
        if (indestructible) return;
        health -= damage;
        Debug.Log("OUCH");
        if (health <= 0f)
        {
            health = 0f; // so it does not display negative value
            Die();
        }
        if (characterType == TypeOfCharacter.Main)
            Debug.Log("Main character took damage, current health: " + health);
        else if (characterType == TypeOfCharacter.Enemy)
            Debug.Log("Enemy took damage, current health: " + health);
        else Debug.Log("Side character took damage, current health: " + health);
    }

    /// <summary>
    /// Method <c>Die</c> sets indicator isDead to true and destroys game object of a character.
    /// </summary>
    private void Die()
    {
        isDead = true;
        if (characterType == TypeOfCharacter.Main)
            Debug.Log("Main character just died.");
        else if (characterType == TypeOfCharacter.Enemy)
            Debug.Log("Enemy just died.");
        else Debug.Log("Side character just died.");

        gameObject.SetActive(false);
        // some kind of restart
    }

    /// <summary>
    /// Method <c>Heal</c> heals a character if needed, for heal amount by heal speed.
    /// </summary>
    private IEnumerator Heal()
    {
        while (!isDead)
        {
            if (health < maximumHealth)
            {
                health += healAmount;
                // Debug.Log("Healing. Current health: " + health);
            }
            yield return new WaitForSeconds(healSpeed);
        }
    }

    /// <summary>
    /// Method <c>Start</c> starts a coroutine for healing.
    /// </summary>
    void Start()
    {
        StartCoroutine(Heal());
    }

    // /// <summary>
    // /// Method <c>Indestructible</c> makes a character indestructible, for indestructible time.
    // /// </summary>
    // private IEnumerator Indestructible()
    // {
    //     if (!isDead)
    //     {
    //         indestructible = true;
    //         Debug.Log("Im immortal! " + indestructible);
    //         yield return new WaitForSeconds(indestructibleTime);
    //         indestructible = false;
    //         Debug.Log("Im immortal! " + indestructible);
    //     }
    // }

    private void SetIndestructibleFalse()
    {
        indestructible = false;
    }


    /// <summary>
    /// Method <c>Start</c> starts a coroutine for indestructible player.
    /// </summary>
    public void BecomeIndestructible()
    {
        // StartCoroutine(Indestructible());
        if (!isDead)
        {
            indestructible = true;
            Debug.Log("Im immortal! " + indestructible);
            Invoke("SetIndestructibleFalse", indestructibleTime);
            Debug.Log("Im not immortal! " + !indestructible);
        }
    }
}
