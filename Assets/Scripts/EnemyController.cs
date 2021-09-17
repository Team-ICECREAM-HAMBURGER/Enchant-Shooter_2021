using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit: " + other.gameObject.name);
    }
}