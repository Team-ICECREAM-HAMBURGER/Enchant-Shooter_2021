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
    [SerializeField] private int score;    
    [SerializeField] private bool hitElec;
    [SerializeField] private int hitElecCount;
    [SerializeField] private bool hitFire;
    [SerializeField] private int hitFireTimer = 1;
    [SerializeField] private bool hitIce;
    [SerializeField] private int hitIceCount;
    [SerializeField] private float dmgMulti = 1.0f;

    public float life;
    public Enchant enchant;
    public ParticleSystem[] iceFX;
    public ParticleSystem[] fireFX;
    public ParticleSystem[] elecFX;

    private GameObject player;
    private Material mat;
    private Animator animator;
    private NavMeshAgent nav;
    private Rigidbody eRB;
    private PlayerController playerController;
    private float smooth = 2.0f;
    private bool isRun;
    private bool isAttack;
    private bool isIdle;
    private bool doDie;
    private float navSpeed_Temp;


    private void Awake()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
        eRB = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        nav = GetComponentInChildren<NavMeshAgent>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
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
        if (isRun)  // Animation; Run
        {
            // Move To Player //
            nav.SetDestination(player.transform.position);
            ChaseStart();

            Vector3 relativePos = (player.transform.position - gameObject.transform.position).normalized;
            relativePos.y = 0;
            transform.LookAt(player.transform.position);
        }
        else
        {
            isIdle = true;
        }

        // Enemy DEATH //
        if (this.life <= 0) 
        {
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
        mat.color = Color.gray;
        nav.isStopped = true;

        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
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
            dmgMulti = 1.5f;
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
        //this.hitElecCount += 1;
        //bulletHit();

        if (this.life <= 0)
        {
            Debug.Log("BOOOOOM");
            elecFX[0].Play();
            EnchantReset();
        }
    }


    private void EnchantIce()
    {
        //this.iceFX[0].Stop();
        //bulletHit();

        if (this.hitIceCount < 5)
        {
            this.hitIceCount += 1;
            nav.speed -= 1f;

            this.iceFX[0].Play();   // ICE Hit FX Play
        }
        else
        {
            Debug.Log("ICCCCCE");
            StartCoroutine(IceTimer());
            EnchantReset();
        }
    }


    IEnumerator IceTimer()
    {
        this.iceFX[0].Stop();   // ICE Hit FX Stop
        this.iceFX[1].Play();   // ICE Explo FX Play

        yield return new WaitForSeconds(3);

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
        life -= playerController.weapon.damage * dmgMulti;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("hit");
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

        /*
        if (collision.gameObject.tag == "Player")
        {
            AttackStart();
        }
        */
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