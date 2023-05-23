using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform prefabBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private GameObject crosshairCanvas;

    private Animator animator;
    private int animIDShoot;

    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;

    private Vector3 mouseWorldPosition = Vector3.zero;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        AssignAnimationIDs();
    }

    void Update()
    {
        mouseWorldPosition = Vector3.zero;
        // raycast so i can see the target point
        // if I shoot at the sky there will be no sphere casted because there is no collider
        // solution to this is to TODO:(on actual level scene) create invisible walls (cubes but dissabled mesh renderer) so we can have colliders everywhere
        Vector2 screenCenterPoint = new Vector2(Screen.width/2f, Screen.height/2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            // debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point; // for rotating character towards the aim
        }

        if (starterAssetsInputs.aim)
        {
            crosshairCanvas.SetActive(true);
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false); // stops rotation of a character in motion while we move
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f)); // start animation gradualy

            // for rotating character towards the aim
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f); // smooth rotation


            // SHOOT
            // TODO: consider allowing shooting only while aiming
            if (starterAssetsInputs.shoot)
            {
                
                animator.SetBool(animIDShoot, true);
            }
            
        } else
        {
            crosshairCanvas.SetActive(false);
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true); // allows rotation of a character in motion while we move
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f)); // stop animation gradualy
        }

        
        
    }

    private void AssignAnimationIDs()
    {
        animIDShoot = Animator.StringToHash("Shoot");
    }

    private int i = 0;

    // Animation Event in metarig|shoot-coconuts so the projectile waits for the animation to finish
    public void FiredEvent(string s)
    {
        if (i % 2 == 1) 
        {
            Debug.Log("DUPLICATE FiredEvent: " + s + " called at: " + Time.time);
            animator.SetBool(animIDShoot, false);
        } else
        {
            Debug.Log("FiredEvent: " + s + " called at: " + Time.time);
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(prefabBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            Debug.Log("SHOOT: " + mouseWorldPosition + ", " + spawnBulletPosition.position);
        }
        starterAssetsInputs.shoot = false;
        animator.SetBool(animIDShoot, false);
        i++;
    }
}
