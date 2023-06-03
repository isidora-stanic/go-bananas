using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    [SerializeField] private float bullteSpeed = 10f;
    [SerializeField] private float autoDestructionTimeAfterHit = 0.3f;
    [SerializeField] private float projectileDamage;
    [SerializeField] private LayerMask enemyLayer;


    [SerializeField] private AudioClip[] projectileLaunchSound, projectileHitSound;
    private AudioManager audioManager;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        audioManager = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioManager>();
        PlayIfAudioManager(projectileLaunchSound);
    }

    void Start()
    {
        bulletRigidbody.velocity = transform.forward * bullteSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, autoDestructionTimeAfterHit); // destroying bullet
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        HealthController enemyController = other.GetComponent<HealthController>();
        if (enemyController != null)
        {
            enemyController.TakeDamage(projectileDamage);
            PlayIfAudioManager(projectileHitSound);
        }
    }

    public void PlayIfAudioManager(AudioClip[] clip)
    {
        try 
            {
                audioManager.PlayRandomSound(clip);
            } 
        catch 
            {
                Debug.Log("There is no audio manager");
            }
    }
}
