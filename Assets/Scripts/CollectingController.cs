using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectingController : MonoBehaviour
{
    [SerializeField] private int coconutsCount = 0;
    [SerializeField] private int bananaPeelCount = 0;
    [SerializeField] private int coinCount = 0;

    private bool hasAudio;
    [SerializeField] private AudioClip coconutSound, bananaPeelSound, boltSound, whipCreamSound, coinSound;

    public int CoconutCount { 
        get { return coconutsCount; }
        set { coconutsCount = value; }
     }
    public int BananaPeelCount { get { return bananaPeelCount; } }
    public int CoinCount { get { return coinCount; } }

    private HealthController healthController;
    private StarterAssets.ThirdPersonController moveController;

    public TMP_Text coconutText, bananaPeelText, coinText;
    void Start()
    {
        healthController = gameObject.GetComponent<HealthController>();
        moveController = gameObject.GetComponent<StarterAssets.ThirdPersonController>();
    }

    void Update()
    {
        coconutText.SetText(""+coconutsCount);
        bananaPeelText.SetText(""+bananaPeelCount);
        coinText.SetText(""+coinCount);
    }

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
            healthController.BecomeIndestructible();
            other.gameObject.SetActive(false);
            Debug.Log("Collected a WhipCream!");
            PlayIfAudioManager(whipCreamSound);

        } else if (other.gameObject.CompareTag("Bolt"))
        {
            moveController.BecomeSuperFast();
            other.gameObject.SetActive(false);
            Debug.Log("Collected a Bolt!");
            PlayIfAudioManager(boltSound);

        } else if (other.gameObject.CompareTag("Coin"))
        {
            coinCount++;
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
