using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecExplosion : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController en = other.GetComponent<EnemyController>();
            en.life = 0;
        }
    }
}
