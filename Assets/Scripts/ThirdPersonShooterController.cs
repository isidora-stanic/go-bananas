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

    // TODO: play animation for shooting
    // private Animator _animator;
    // private bool _hasAnimator;
    // private int _animIDShoot;

    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    void Start()
    {
        // _hasAnimator = TryGetComponent(out _animator);
        // AssignAnimationIDs();
    }

    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
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

            // for rotating character towards the aim
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f); // smooth rotation
        } else
        {
            crosshairCanvas.SetActive(false);
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true); // allows rotation of a character in motion while we move
        }

        // SHOOT
        if (starterAssetsInputs.shoot)
        {
            Debug.Log("SHOOT: " + mouseWorldPosition + ", " + spawnBulletPosition.position);
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(prefabBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            starterAssetsInputs.shoot = false;
        }
        
    }

    // TODO: play animation for shooting
    // private void AssignAnimationIDs()
    // {
    //     _animIDShoot = Animator.StringToHash("Shoot");
    // }
}
