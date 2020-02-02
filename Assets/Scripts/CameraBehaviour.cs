using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform playerTransform;

    public float cameraOffsetX;
    public float cameraOffsetY;

    public bool isFrozenOnOrigin = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    void LateUpdate()
    {
        if (isFrozenOnOrigin)
            transform.position = new Vector3(0.0f, 0.0f, -50.0f);
        else
        {
            Vector3 newPos = transform.position;
            newPos.x += Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f;
            newPos.y += Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;

            transform.position = newPos;

            //Vector3 temp = transform.position;
            //temp.x = playerTransform.position.x;
            //temp.y = playerTransform.position.y;

            //temp.x += cameraOffsetX;
            //temp.y += cameraOffsetY;
            //transform.position = temp;
        }

    }
}
