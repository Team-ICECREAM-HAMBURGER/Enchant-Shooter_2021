using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Enchant
{
    Normal,
    Elec,
    Fire,
    Ice,
}


public enum Stat
{
    Run,
    Attack,
    Idle,
    Die
}


public class EnemyController : MonoBehaviour
{  
    [SerializeField] private bool hitElec;
    [SerializeField] private int hitElecCount;
    [SerializeField] private bool hitFire;
    [SerializeField] private int hitFireTimer = 1;
    [SerializeField] private bool hitIce;
    [SerializeField] private int hitIceCount;
    [SerializeField] private float dmgMulti = 1.0f;

    public Enchant enchant;
    public ParticleSystem[] iceFX;
    public ParticleSystem[] fireFX;
    public ParticleSystem[] elecFX;
    public int score;
    public float life;

    private GameObject player;
    private Material mat;
    private Animator animator;
    private NavMeshAgent nav;
    private Rigidbody eRB;
    private PlayerController playerController;
    private UIController uiController;
    private float smooth = 2.0f;
    private bool isRun;
    private bool isAttack;
    private bool isIdle;
    private bool isBoom;
    private bool isFrozen;
    private bool doDie;
    private bool goldenGet;
    private bool isRPG;
    private float navSpeed_Temp;


    private void Awake()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
        eRB = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        nav = GetComponentInChildren<NavMeshAgent>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        uiController = GameObject.Find("Canvas").GetComponent<UIController>();
        navSpeed_Temp = nav.speed;

        ChaseStart();
    }


    private void FixedUpdate()
    {
        FreezeVelocity();
    }


    private void ChaseStart()
    {
        isRun = true; isAttack = false;
        animator.SetBool("isRun", true);
        animator.SetBool("isAttack", false);
    }


    private void AttackStart()
    {
        isAttack = true; isRun = false;
        animator.SetBool("isAttack", true);
        animator.SetBool("isRun", false);
    }


    private void Update()
    {
        if (playerController.weaponType == WeaponType.RPG)
        {
            this.isRPG = true;
        }

        if (isRun)  // Animation; Run
        {
            // Move To Player //
            nav.SetDestination(player.transform.position);
            ChaseStart();

            if (!doDie && !isBoom && !isFrozen)
            {
                transform.LookAt(player.transform.position);
            }
        }
        else
        {
            isIdle = true;
        }

        // Enemy DEATH //
        if (this.life <= 0) 
        {
            this.doDie = true;
            mat.color = Color.gray;
            GoldenBulletGet();  // Enchant Bullet GET
            StartCoroutine(Killed());
        }
    }


    // Enchant Bullet GET; Enchat Bullet Timer ON //
    private void GoldenBulletGet()
    {
        if (enchant != Enchant.Normal)  
        {
            playerController.isGolden = true;
            //this.goldenSwt = true;
            //playerController.goldenTimer = playerController.goldenTimer_Temp;
            //StartCoroutine(playerController.EnchantTimer());
        }
    }


    private void FreezeVelocity()
    {
        eRB.velocity = Vector3.zero;
        eRB.angularVelocity = Vector3.zero;
    }


    IEnumerator Killed()
    {
        nav.isStopped = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;

        animator.SetTrigger("doDie");
        
        if (!uiController.enemyKilledSwt)
        {
            uiController.EnemyKilledAudio();
        }

        this.isRun = false; this.isAttack = false;

        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        uiController.enemyKilledSwt = false;
        this.goldenGet = false;
        Destroy(gameObject);
    }


    private void EnchantFire()
    {
        StartCoroutine(FireTimer());
    }


    IEnumerator FireTimer()
    {
        while (hitFireTimer < 3)
        {
            this.fireFX[0].Play();
            dmgMulti = 3f;
            hitFireTimer += 1;
            //bulletHit();
            yield return new WaitForSeconds(1);
        }

        dmgMulti = 1;
        hitFireTimer = 1;
        this.fireFX[0].Stop();
    }


    private void EnchantElec()
    {
        if (this.life <= 0)  // this.life <= 0
        {
            elecFX[0].Play();
            EnchantReset();
        }
    }


    private void EnchantIce()
    {
        //this.iceFX[0].Stop();
        //bulletHit();

        if (this.hitIceCount < 1)
        {
            this.hitIceCount += 1;
            nav.speed -= 5f;

            this.iceFX[0].Play();   // ICE Hit FX Play
        }
        else
        {
            this.isFrozen = true;
            StartCoroutine(IceTimer());
            EnchantReset();
        }
    }


    IEnumerator IceTimer()
    {
        this.isBoom = true; 
        this.iceFX[0].Stop();   // ICE Hit FX Stop
        this.iceFX[1].Play();   // ICE Explo FX Play

        yield return new WaitForSeconds(3);

        this.isBoom = false;
        this.iceFX[1].Stop();   // ICE Explo FX Stop
        nav.speed = navSpeed_Temp;
    }


    private void EnchantReset()
    {
        this.hitElecCount = 0;
        this.hitIceCount = 0;

        this.hitElec = false;
        this.hitFire = false;
        this.hitIce = false;   
    }


    private void bulletHit()
    {
        if (this.isRPG)
        {
            life = 0;
            Debug.Log(dmgMulti);
            this.isRPG = false;
        }
        life -= playerController.weapon.damage * dmgMulti;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            playerController.scoreAll += this.score;
            bulletHit();
            
            switch (playerController.enchantType)
            {
                case EnchantType.Elec:
                    this.hitElec = true;
                    EnchantElec();   
                    break;

                case EnchantType.Fire:
                    this.hitFire = true;
                    EnchantFire();
                    break;

                case EnchantType.Ice:
                    this.hitIce = true;
                    EnchantIce();
                    break;
            }
        }

        
        if (collision.gameObject.tag == "Player")
        {
            isRun = false;
        }
        
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isRun = true;
            playerController.isHit = false;
        }
    }
}