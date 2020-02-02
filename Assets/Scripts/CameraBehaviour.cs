using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform playerTransform;

    public float cameraOffsetX;
    public float cameraOffsetY;

    public float zoomSpeed = 1;
    public float targetOrtho = 5.0f;
    public float smoothSpeed = 2.0f;
    public float minOrtho = 1.0f;
    public float maxOrtho = 20.0f;

    public bool transitioning = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (isFrozenOnOrigin)
            transform.position = new Vector3(0.0f, 0.0f, -50.0f);
        else
        {
            Vector3 newPos = transform.position;
            newPos.x += Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f;
            newPos.y += Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;

        if (!transitioning)
        {

            Vector3 temp = transform.position;
            temp.x = playerTransform.position.x;
            temp.y = playerTransform.position.y;

            temp.x += cameraOffsetX;
            temp.y += cameraOffsetY;
            transform.position = temp;
        }  
       

    }
}
