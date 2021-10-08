using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private GameController gc;


    private void Awake()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Vector3 hitPos = transform.position;
            gc.HitFX(hitPos, gameObject.name);  // Hit Fx Play Call
            Destroy(gameObject);
        }
    }
}