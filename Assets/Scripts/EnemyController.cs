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
    //[SerializeField] private float speed;
    [SerializeField] private int score;

    // Temp //   
    [SerializeField] private bool isRun;
    [SerializeField] private bool isAttack;
    [SerializeField] private bool isIdle;
    [SerializeField] private bool doDie;


    public Enchant enchant;
    public GameObject player;

    private Animator animator;
    private NavMeshAgent nav;
    private Rigidbody eRB;
    private float smooth = 2.0f;

    
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        eRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        ChaseStart();
    }


    private void FixedUpdate()
    {
        FreezeVelocity();
    }


    private void ChaseStart()
    {
        isRun = true;
        animator.SetBool("isRun", true);
    }


    private void Update()
    {
        if (isRun)  // Animation; Run
        {
            // Move To Player //
            nav.SetDestination(player.transform.position);

            Vector3 relativePos = (player.transform.position - gameObject.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
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
}