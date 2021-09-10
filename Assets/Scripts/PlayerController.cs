using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float horizontalInput;
    private float verticalInput;


    // Update is called once per frame
    void FixedUpdate()
    {
        // Moving Key Input (WASD of Arrow Keys)
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Player Move
        transform.Translate(Vector3.forward * verticalInput * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);


    }
}
