using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTimer : MonoBehaviour
{
    [SerializeField] private float respawnTime = 30f;

    private void ActivateAgain()
    {
        Debug.Log("Respawning!");
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (!gameObject.activeSelf)
        {
            Debug.Log("I'm collected!");
            Invoke("ActivateAgain", respawnTime);
        }
    }
}
