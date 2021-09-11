using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float hAxis;
    private float vAxis;
    private int layerMask;

    private Vector3 moveVec;


    private void Awake()
    {
        layerMask = (-1) - (1 << LayerMask.NameToLayer("Obstacles"));
        layerMask = ~layerMask;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // Moving Key Input (WASD of Arrow Keys) //
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        // Player Move //
        transform.Translate(Vector3.right.normalized * hAxis * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward.normalized * vAxis * moveSpeed * Time.deltaTime);
        //moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        //transform.position += moveVec * moveSpeed * Time.deltaTime;

        // Player Sight //
        LookMousePos();
    }

    
    private void LookMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, layerMask))
        {
            Vector3 mouseDir = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
            gameObject.transform.LookAt(transform.position + mouseDir);
        }
    } 
}