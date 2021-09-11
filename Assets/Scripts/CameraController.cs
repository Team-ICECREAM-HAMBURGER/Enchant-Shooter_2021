using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float rotateSpeed;





    // Update is called once per frame
    void Update()
    {
        // Player Tracking
        transform.position = target.position + offset;

        transform.LookAt(target);


        // Camera Rotate
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(CameraRotation("Q"));
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(CameraRotation("E"));
        }
    }


    IEnumerator CameraRotation(string key)
    {
        if (key == "Q")
        {
            while (transform.rotation.y <= 0.62f)
            {
                Debug.Log("!");
                transform.RotateAround(target.position, Vector3.up, rotateSpeed * Time.deltaTime);
                offset = transform.position - target.position;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (key == "E")
        {
            while (transform.rotation.y >= -0.62f)
            {
                Debug.Log("!");
                transform.RotateAround(target.position, Vector3.down, rotateSpeed * Time.deltaTime);
                offset = transform.position - target.position;
                yield return new WaitForSeconds(0.01f);
            }

        }
        
    }

}
