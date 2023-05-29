using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector = new Vector3(15, 30, 45);

    void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime);
    }
}
