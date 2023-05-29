using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // krece se od tacke do tacke, u ovoj varijanti do svakog collectable-a

    [SerializeField] private Transform enemyMesh;
    [SerializeField] private float speed = 0f;
    [SerializeField] private List<Transform> wayPoints;

    private int _nextPointIndex;
    private float _range = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
            _nextPointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Move();
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
}
