using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletEnchant
{
    electric,
    fire,
    ice
}


public class WeaponController : MonoBehaviour
{
    public float damage;
    public float shootRate;
    public int ammo;

    public BulletEnchant bulletEnchant;
    public GameObject muzzle;

    [HideInInspector] public GameObject[] bullets;

    private Rigidbody bulletRigid;
    private float shootRate_Tmp;

    private bool canShoot = true;


    public void GunMode(int gunType)
    {
        shootRate_Tmp = shootRate;

        switch(gunType)
        {
            case 0: // HG
            case 2: // SG
            case 3: // RPG
                if (Input.GetMouseButtonDown(0) && canShoot)
                {
                    StartCoroutine("Shoot");
                    canShoot = false;
                }
                break;
            case 1: // AR
                if (Input.GetMouseButton(0) && canShoot)
                {
                    StartCoroutine("Shoot");
                    canShoot = false;
                }
                break;
        }        
    }


    IEnumerator Shoot()
    {
        // Bullet Shoot //
        GameObject bullet = Instantiate(bullets[(int)BulletEnchant.electric], muzzle.transform.position, Quaternion.identity);
        bulletRigid = bullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = muzzle.transform.forward * 50f;

        // Shoot Delay //
        yield return new WaitForSeconds(this.shootRate);
        canShoot = true;
    }
}