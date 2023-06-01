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
    [SerializeField] private LayerMask playerLayer;

    private int _nextPointIndex;
    private float _range = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
            _nextPointIndex = 0;
    }

    private void Move()
    {
        enemyMesh.LookAt(wayPoints[_nextPointIndex]); // rotira ga da gleda u sledeci collectable
        enemyMesh.Translate(Vector3.forward * speed * Time.deltaTime); // kreni tamo gde si okrenut

        if (Vector3.Distance(enemyMesh.position, wayPoints[_nextPointIndex].position) < _range)
        {
            _nextPointIndex++;
            if (_nextPointIndex >= wayPoints.Count)
            {
                _nextPointIndex = 0;
            }
        }
    }

    void Update()
    {
        //currently does not move

        // KICK
        if (kickTimer <= 0)
        {
            // Debug.Log("KICKED SOMETHING!");
            Collider[] hitPlayers = Physics.OverlapSphere(enemyMesh.position, kickRadius, playerLayer);
            foreach (Collider player in hitPlayers)
            {
                HealthController playerController = player.GetComponent<HealthController>();
                Debug.Log("Kicking the player: " + player + " " + playerController);
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
}
