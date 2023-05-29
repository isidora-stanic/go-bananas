using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingController : MonoBehaviour
{
    [SerializeField] private int coconutsCount = 0;
    [SerializeField] private int bananaPeelCount = 0;

    private bool hasAudio;
    [SerializeField] private AudioClip coconutSound, bananaPeelSound, boltSound, whipCreamSound, coinSound;

    public int CoconutCount { 
        get { return coconutsCount; }
        set { coconutsCount = value; }
     }
    public int BananaPeelCount { get { return bananaPeelCount; } }


    // TODO: think what each of this collectables will do for a player?
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coconut"))
        {
            coconutsCount++;
            other.gameObject.SetActive(false);
            Debug.Log("Collected a Coconut!");
            AudioManager.Instance.PlaySound(coconutSound);

        } else if (other.gameObject.CompareTag("BananaPeel"))
        {
            bananaPeelCount++;
            other.gameObject.SetActive(false);
            Debug.Log("Collected a Banana Peel!");
            PlayIfAudioManager(bananaPeelSound);
            

        } else if (other.gameObject.CompareTag("WhipCream"))
        {
            // StartCoroutine(Indestructable());
            other.gameObject.SetActive(false);
            Debug.Log("Collected a WhipCream!");
            PlayIfAudioManager(whipCreamSound);

        } else if (other.gameObject.CompareTag("Bolt"))
        {
            // StartCoroutine(SuperFast()); // TODO: consider it to make you a tiny bit faster if you collect x number of bolts instead of super speed?
            other.gameObject.SetActive(false);
            Debug.Log("Collected a Bolt!");
            PlayIfAudioManager(boltSound);

        } else if (other.gameObject.CompareTag("Coin"))
        {
            // coin++? TODO: consider if you need this or something to make you stronger or faster?
            other.gameObject.SetActive(false);
            Debug.Log("Collected a Coin!");
            PlayIfAudioManager(coinSound);
        }
    }

    public void PlayIfAudioManager(AudioClip clip)
    {
        try 
            {
                AudioManager.Instance.PlaySound(clip);
            } 
        catch 
            {
                Debug.Log("There is no audio manager");
            }
    }
}
