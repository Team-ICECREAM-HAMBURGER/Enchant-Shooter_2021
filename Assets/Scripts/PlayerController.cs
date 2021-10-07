using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    HG,
    AR,
    SG,
    RPG
}

public enum EnchantType
{
    Normal,
    Elec,
    Fire,
    Ice,
}


public class PlayerController : MonoBehaviour
{
    public WeaponType weaponType;
    public EnchantType enchantType;
    public int life;
    public float moveSpeed;
    public float goldenTimer;
    public bool isGolden;
    public bool goldenSwt;
    public GameObject HG;
    public GameObject AR;
    public GameObject SG;
    public GameObject RPG;
    public Animator animator;

    private Vector3 moveVec;
    private WeaponController weapon;
    private float hAxis;
    private float vAxis;
    private float goldenTimer_Temp;


    private void Awake()
    {
        goldenTimer_Temp = goldenTimer; // Golden Timer Backup
    }


    void FixedUpdate()
    {
        // Moving Key Input (WASD of Arrow Keys) //
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        // Player Move //
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec * moveSpeed * Time.deltaTime;

        if (moveVec != Vector3.zero)
        {
            Debug.Log("ON MOVING!");
            animator.SetBool("isRun", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            Debug.Log("NOT MOVING!");
            animator.SetBool("isIdle", true);
            animator.SetBool("isRun", false);
        }

        // Player Sight //
        LookMousePos();
    }


    void Update()
    {
        //Cursor.visible = false;

        // Weapon Activate & Shoot //
        WeaponActive();

        // Enchant Bullet Activating //
        if (isGolden && goldenSwt)          // Timer ON
        {
            goldenTimer = goldenTimer_Temp;
            StartCoroutine(EnchantTimer());
        }
        else if (!isGolden && !goldenSwt)   // Timer OUT
        {
            enchantType = EnchantType.Normal;
        }
        else if (isGolden && !goldenSwt)    // Enchant Bullet Activate
        {
            BulletEnchantChange();
        }
    }


    private void LookMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit))  // Ray shoot
        {
            Vector3 mouseDir = (hit.point - transform.position);    // dir = mouse point - player point (Vector - Vector)
            mouseDir.y = 0;
            
            // Skip the Height detection
            gameObject.transform.LookAt(transform.position + mouseDir);
        }
    }


    // Weapon Activate & Shoot //
    private void WeaponActive()
    {
        switch (this.weaponType)
        {
            case WeaponType.HG:
                weapon = HG.GetComponent<WeaponController>();
                weapon.GunMode((int)WeaponType.HG);
                break;

            case WeaponType.AR:
                weapon = AR.GetComponent<WeaponController>();
                weapon.GunMode((int)WeaponType.AR);

                // IF, Weapon's Load OUT, Change to HG //
                if (weapon.ammo <= 0)
                {
                    weapon.gameObject.SetActive(false);
                    weapon.ammo = weapon.ammo_Temp;
                    weapon.canShoot = true;

                    this.weaponType = 0;
                    HG.SetActive(true);
                }
                break;

            case WeaponType.SG:
                weapon = SG.GetComponent<WeaponController>();
                weapon.GunMode((int)WeaponType.SG);

                // IF, Weapon's Load OUT, Change to HG //
                if (weapon.ammo <= 0)
                {
                    weapon.gameObject.SetActive(false);
                    weapon.ammo = weapon.ammo_Temp;
                    weapon.canShoot = true;

                    this.weaponType = 0;
                    HG.SetActive(true);
                }
                break;

            case WeaponType.RPG:
                weapon = RPG.GetComponent<WeaponController>();
                weapon.GunMode((int)WeaponType.RPG);

                // IF, Weapon's Load OUT, Change to HG //
                if (weapon.ammo <= 0)
                {
                    weapon.gameObject.SetActive(false);
                    weapon.ammo = weapon.ammo_Temp;
                    weapon.canShoot = true;

                    this.weaponType = 0;
                    HG.SetActive(true);
                }
                break;
        }
    }


    // Bullet Enchant Type Change //
    IEnumerator EnchantTimer()
    {
        goldenSwt = false;

        while(goldenTimer > 0.0f)
        {
            goldenTimer -= 1f;
            yield return new WaitForSeconds(1);
        }

        isGolden = false;
        goldenSwt = true;
    }

    
    // Bullet Enchant Type Change //
    private void BulletEnchantChange()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if ((int)enchantType >= System.Enum.GetNames(typeof(EnchantType)).Length - 1)
            {
                enchantType = EnchantType.Elec;
            }
            else
            {
                enchantType++;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if ((int)enchantType <= 0)
            {
                enchantType = EnchantType.Ice;
            }
            else
            {
                enchantType--;
            }
        }

        weapon.BulletMode(enchantType);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            this.life = 0;
            gameObject.SetActive(false);
            Debug.Log("GameOver!");
        }
    }
}