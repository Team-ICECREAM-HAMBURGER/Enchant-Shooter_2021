using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    [HideInInspector] public int ammo_Temp;

    public float damage;
    public float shootRate;
    public int ammo;
    public int bulletType;
    public int gunType;
    public bool canShoot;
    public GameObject muzzle;
    public GameObject[] bullets;
    public GameObject player;
    public ParticleSystem[] muzzleFX;
    public UIController uiController;

    private GameObject bullet;
    private Rigidbody bulletRigid;
    private PlayerController playerController;


    private void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
        ammo_Temp = ammo;
    }


    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            playerController.animator.SetBool("isFire", false);
        }
    }


    public void GunMode(int gunType)
    {
        this.gunType = gunType;

        switch (gunType)
        {
            case 0: // HG
            case 2: // SG
            case 3: // RPG
                if (Input.GetMouseButtonDown(0) && canShoot)
                {
                    StartCoroutine(Shoot(gunType));
                    uiController.gunAudio[gunType].Play();
                    canShoot = false;
                }
                break;
            case 1: // AR
                if (Input.GetMouseButton(0) && canShoot)
                {
                    StartCoroutine(Shoot(gunType));
                    // Audio play //
                    if (!uiController.gunAudio[gunType].isPlaying)
                    {
                        uiController.gunAudio[gunType].Play();
                    }
                    canShoot = false;
                }

                if (Input.GetMouseButtonUp(0) || this.ammo <= 0)
                {
                    uiController.gunAudio[gunType].Stop();
                }
                break;
        }

        if (this.ammo <= 0)
        {
            this.ammo = this.ammo_Temp;
            this.canShoot = true;
            this.gameObject.SetActive(false);
            playerController.WeaponReset(this);
        }
    }


    public void BulletMode(EnchantType enchantType)
    {
        // TODO: EnchatMode Effect Set; Switch
        switch (enchantType)
        {
            case EnchantType.Elec:
                //Debug.Log("Elec");
                break;

            case EnchantType.Ice:
                //Debug.Log("Ice");
                break;

            case EnchantType.Fire:
                //Debug.Log("Fire");
                break;
        }

        this.bulletType = (int)enchantType;
    }


    IEnumerator Shoot(int gunType)
    {
        // Bullet Shoot //
        bullet = Instantiate(bullets[this.bulletType], muzzle.transform.position, Quaternion.Euler(playerController.mouseDir));
        bulletRigid = bullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = muzzle.transform.forward * 20f;
        playerController.animator.SetBool("isFire", true);

        // FX play //
        muzzleFX[this.bulletType].Play();

        // Shoot Delay //
        yield return new WaitForSeconds(this.shootRate);
        canShoot = true;

        // Ammo Update //
        if (gunType != 0)   // IF NOT HG
        {
            this.ammo -= 1;
        }
    }
}