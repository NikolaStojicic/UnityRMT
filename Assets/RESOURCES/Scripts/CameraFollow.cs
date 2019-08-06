using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private float moveSpeed = 100f;
    private float scrollSpeed = 5000f;


    /// <summary>
    /// Used to make camera 5 top-down camera, move on commands w,a,s,d + scroll wheel.
    /// </summary>
    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * Time.deltaTime;

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Vector3 lastPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.position += scrollSpeed * new Vector3(0, -Input.GetAxis("Mouse ScrollWheel"), 0) * Time.deltaTime;
            if (transform.position.y > 300 || transform.position.y < 130)
                transform.position = lastPos;
        }
    }

}
