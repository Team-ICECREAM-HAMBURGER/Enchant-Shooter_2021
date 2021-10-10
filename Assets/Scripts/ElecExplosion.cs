using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecExplosion : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("On");
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("!");
            EnemyController en = other.GetComponent<EnemyController>();
            en.life = 0;
        }
    }
}