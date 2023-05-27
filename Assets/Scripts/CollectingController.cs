using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingController : MonoBehaviour
{
    [SerializeField] private int coconutsCount = 0;
    [SerializeField] private int bananaPeelCount = 0;

    public int CoconutCount { get; set; }
    public int BananaPeelCount { get; }

    // TODO: think what each of this collectables will do for a player?
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coconut"))
        {
            coconutsCount++;
            other.gameObject.SetActive(false);
            Debug.Log("Collected a Coconut!");

        } else if (other.gameObject.CompareTag("BananaPeel"))
        {
            bananaPeelCount++;
            other.gameObject.SetActive(false);
            Debug.Log("Collected a Banana Peel!");

        } else if (other.gameObject.CompareTag("WhipCream"))
        {
            // StartCoroutine(Indestructable());
            other.gameObject.SetActive(false);
            Debug.Log("Collected a WhipCream!");

        } else if (other.gameObject.CompareTag("Bolt"))
        {
            // StartCoroutine(SuperFast()); // TODO: consider it to make you a tiny bit faster if you collect x number of bolts instead of super speed?
            other.gameObject.SetActive(false);
            Debug.Log("Collected a Bolt!");

        } else if (other.gameObject.CompareTag("Coin"))
        {
            // coin++? TODO: consider if you need this or something to make you stronger or faster?
            other.gameObject.SetActive(false);
            Debug.Log("Collected a Coin!");
        }
    }
}
