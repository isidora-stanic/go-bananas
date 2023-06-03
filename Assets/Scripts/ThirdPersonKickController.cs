using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonKickController : MonoBehaviour
{

    [SerializeField] private float kickDamage;
    [SerializeField] private float kickRadius;
    [SerializeField] private LayerMask enemyLayer;


    [SerializeField] private AudioClip[] kickSound;
    private AudioManager audioManager;


    private Animator animator;
    private int animIDKick;

    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioManager>();
    }

    void Start()
    {
        AssignAnimationIDs();
    }

    void Update()
    {
        // KICK
        if (starterAssetsInputs.kick)
        {
            animator.SetBool(animIDKick, true);
        }
    }

    private void AssignAnimationIDs()
    {
        animIDKick = Animator.StringToHash("Kick");
    }

    // Animation Event in metarig|punch-leg so the damage waits for the animation to finish
    public void KickedEvent(string s)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, kickRadius, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            HealthController enemyController = enemy.GetComponent<HealthController>();
            if (enemyController != null)
            {
                enemyController.TakeDamage(kickDamage);
                PlayIfAudioManager(kickSound);
            }
        }
        starterAssetsInputs.kick = false;
        animator.SetBool(animIDKick, false);
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
