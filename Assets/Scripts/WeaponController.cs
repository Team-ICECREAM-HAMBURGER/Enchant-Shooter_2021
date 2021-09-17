using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    public float damage;
    public float shootRate;
    public int ammo;
    public bool canShoot;

    public GameObject muzzle;
    public GameObject[] bullets;

    private Rigidbody bulletRigid;



    public void GunMode(int gunType)
    {
        switch (gunType)
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


    public void BulletMode(int enchatMode)
    {

    }



    IEnumerator Shoot()
    {
        // Bullet Shoot //
        GameObject bullet = Instantiate(bullets[0], muzzle.transform.position, Quaternion.identity);
        bulletRigid = bullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = muzzle.transform.forward * 50f;

        // Shoot Delay //
        yield return new WaitForSeconds(this.shootRate);
        canShoot = true;
    }
}