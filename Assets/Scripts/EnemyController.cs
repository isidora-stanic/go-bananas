using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // krece se od tacke do tacke, u ovoj varijanti do svakog collectable-a

    [SerializeField] private Transform enemyMesh;
    [SerializeField] private float speed = 0f;
    [SerializeField] private List<Transform> wayPoints;

    [SerializeField] private float kickSpeed = 5f;
    private float kickTimer = 0f;

    [SerializeField] private float kickDamage = 20f;
    [SerializeField] private float kickRadius = 40f;
    [SerializeField] private float detectRadius = 10f;
    [SerializeField] private LayerMask playerLayer;

    private int _nextPointIndex;
    private float _range = 1.0f;

    // animation
    private Animator _animator;
    private int _animIDSpeed;
    // private bool _hasAnimator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        AssignAnimationIDs();
    }

    // Start is called before the first frame update
    void Start()
    {
            _nextPointIndex = 0;
    }

    private void Move(GameObject player = null)
    {
        if (player != null)
        {
            _animator.SetFloat(_animIDSpeed, speed);
            enemyMesh.LookAt(player.transform);
            enemyMesh.Translate(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        _animator.SetFloat(_animIDSpeed, speed);
        
        enemyMesh.LookAt(wayPoints[_nextPointIndex]); // rotira ga da gleda u sledeci collectable
        enemyMesh.Translate(Vector3.forward * speed * Time.deltaTime); // kreni tamo gde si okrenut

        if (Vector3.Distance(enemyMesh.position, wayPoints[_nextPointIndex].position) < _range)
        {
            GoToNextPosition();
        }
    }

    private void GoToNextPosition()
    {
        _nextPointIndex++;
        if (_nextPointIndex >= wayPoints.Count)
        {
            _nextPointIndex = 0;
        }
    }

    void Update()
    {
        // move towards a player if in radius
        Collider[] players = IsPlayerInRange();
        if (players.Length > 0)
        {
            Move(players[0].gameObject);
        } else Move();
        

        // KICK
        if (kickTimer <= 0)
        {
            // Debug.Log("KICKED SOMETHING!");
            Collider[] hitPlayers = Physics.OverlapSphere(enemyMesh.position, kickRadius, playerLayer);
            foreach (Collider player in hitPlayers)
            {
                HealthController playerController = player.GetComponent<HealthController>();
                // Debug.Log("Kicking the player: " + player + " " + playerController);
                if (playerController != null)
                {
                    playerController.TakeDamage(kickDamage);
                }
            }
            kickTimer = kickSpeed;
        }
        else
        {
            kickTimer -= Time.deltaTime;
            // Debug.Log("NOT KICKING! " + kickTimer);
        }

    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
    }

    private Collider[] IsPlayerInRange()
    {
        Collider[] hitPlayers = Physics.OverlapSphere(enemyMesh.position, detectRadius, playerLayer);
        return hitPlayers;
    }
}
