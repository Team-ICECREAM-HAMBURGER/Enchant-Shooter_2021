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

    private Vector3 moveVec;
    private WeaponController weapon;
    private float hAxis;
    private float vAxis;


    void FixedUpdate()
    {
        // Moving Key Input (WASD of Arrow Keys) //
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        // Player Move //
        //transform.Translate(Vector3.right.normalized * hAxis * moveSpeed * Time.deltaTime);
        //transform.Translate(Vector3.forward.normalized * vAxis * moveSpeed * Time.deltaTime);
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec * moveSpeed * Time.deltaTime;

        // Player Sight //
        LookMousePos();
    }


    void Update()
    {
        // Weapon Activate & Shoot //
        switch(weaponType)
        {
            case WeaponType.HG:
                weapon = transform.GetChild(0).gameObject.GetComponent<WeaponController>();
                weapon.GunMode((int)WeaponType.HG);
                break;

            case WeaponType.AR:
                weapon = transform.GetChild(1).gameObject.GetComponent<WeaponController>();
                weapon.GunMode((int)WeaponType.AR);
                break;

            case WeaponType.SG:
                weapon = transform.GetChild(2).gameObject.GetComponent<WeaponController>();
                weapon.GunMode((int)WeaponType.SG);
                break;

            case WeaponType.RPG:
                weapon = transform.GetChild(3).gameObject.GetComponent<WeaponController>();
                weapon.GunMode((int)WeaponType.RPG);
                break;
        }

        // Bullet Enchant Type Change //
        // TODO
        ///////////////////////////////


    }


    private void LookMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit))  // Ray shoot
        {
            Vector3 mouseDir = (hit.point - transform.position);    // dir = mouse point - player point (Vector - Vector)
            mouseDir.y = 0;                                         // Skip the Height detection
            gameObject.transform.LookAt(transform.position + mouseDir);
        }
    }
}