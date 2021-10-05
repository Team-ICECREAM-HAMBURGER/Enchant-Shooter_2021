using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    [HideInInspector] public int ammo_Temp;

    public float damage;
    public float shootRate;
    public int ammo;
    public bool canShoot;
    public GameObject muzzle;
    public GameObject[] bullets;
    public GameObject player;

    private int bulletType;
    private Rigidbody bulletRigid;
    private GameObject bullet;
    private PlayerController playerController;
    private ParticleSystem muzzleFX;

    [SerializeField] private float bulletSpeed;


    private void Awake()
    {
        muzzleFX = transform.GetComponentInChildren<ParticleSystem>();
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
        switch (gunType)
        {
            case 0: // HG
            case 2: // SG
            case 3: // RPG
                if (Input.GetMouseButtonDown(0) && canShoot)
                {
                    StartCoroutine(Shoot(gunType));
                    canShoot = false;
                }
                break;

            case 1: // AR
                if (Input.GetMouseButton(0) && canShoot)
                {
                    StartCoroutine(Shoot(gunType));
                    canShoot = false;
                }
                break;
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
        bullet = Instantiate(bullets[this.bulletType], muzzle.transform.position, Quaternion.identity);
        bulletRigid = bullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = muzzle.transform.forward * 50f;
        //bullet.transform.Translate(Vector3.forward * 50 * Time.deltaTime);
        playerController.animator.SetBool("isFire", true);

        // FX play //
        muzzleFX.Play();


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