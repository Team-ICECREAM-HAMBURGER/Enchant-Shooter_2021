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


public class PlayerController : MonoBehaviour
{
    public WeaponType weaponType;

    [SerializeField] private float moveSpeed;

    private float hAxis;
    private float vAxis;

    private Vector3 moveVec;
    private WeaponController weapon;



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
        // Item get //


        //-----//
        







        // Weapon Activate & Shoot //
        switch (weaponType)
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
    }


    private void LookMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mouseDir = (hit.point - transform.position);
            mouseDir.y = 0;
            gameObject.transform.LookAt(transform.position + mouseDir);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {

        }
    }


}