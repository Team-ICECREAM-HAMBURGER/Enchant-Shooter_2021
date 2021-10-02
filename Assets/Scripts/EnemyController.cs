using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Enchant
{
    Elec,
    Fire,
    Ice,
    Normal
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
    [SerializeField] private int life;
    [SerializeField] private int attackPower;
    [SerializeField] private int score;    
    [SerializeField] private bool hitElec;
    [SerializeField] private int hitElecCount;
    [SerializeField] private bool hitFire;
    [SerializeField] private bool hitIce;
    [SerializeField] private int hitIceCount;


    public Enchant enchant;
    public GameObject player;

    private PlayerController playerController;
    private Animator animator;
    private NavMeshAgent nav;
    private Rigidbody eRB;
    private float smooth = 2.0f;
    private bool isRun;
    private bool isAttack;
    private bool isIdle;
    private bool doDie;


    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        eRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();

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
    }


    private void FreezeVelocity()
    {
        eRB.velocity = Vector3.zero;
        eRB.angularVelocity = Vector3.zero;
    }


    private void EnchantFire()
    {
        
    }


    private void EnchantElec()
    {
        this.hitElecCount += 1;

        if (this.hitElecCount >= 10)
        {
            Debug.Log("BOOOOOM");
            EnchantReset();
        }
    }


    private void EnchantIce()
    {
        this.hitIceCount += 1;

        if (this.hitIceCount >= 20)
        {
            Debug.Log("ICCCCCE");
            EnchantReset();
        }
    }


    private void EnchantReset()
    {
        this.hitElecCount = 0;
        this.hitIceCount = 0;

        this.hitElec = false;
        this.hitFire = false;
        this.hitIce = false;   
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("hit");

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
            AttackStart();
            playerController.life -= 1;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isRun = true;
        }
    }
}