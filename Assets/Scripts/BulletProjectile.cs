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

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        bulletRigidbody.velocity = transform.forward * bullteSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO: get the layer, do the damage (this method or OnCollisionEnter)
        Destroy(gameObject, autoDestructionTimeAfterHit); // destroying bullet
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // Debug.Log("contact info: " + contact.point + " " + contact.normal);
        }
        // Debug.Log("first: " + collision.collider + ", second: " + collision.collider);
        // Debug.Log("second layer: " + collision.collider.gameObject.layer);
        Collider other = collision.collider;
        // if (collision.relativeVelocity.magnitude > 2)
        //     audioSource.Play();
        HealthController enemyController = other.GetComponent<HealthController>();
        // Debug.Log("Enemy: " + other + " " + enemyController);
        if (enemyController != null)
        {
            enemyController.TakeDamage(projectileDamage);
            Debug.Log("BAAAM");
        }
    }
}
