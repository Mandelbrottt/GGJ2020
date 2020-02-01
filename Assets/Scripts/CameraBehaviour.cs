using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    public Transform transform;
    public Transform playerTransform;

    public float cameraOffsetX;
    public float cameraOffsetY;

    // Start is called before the first frame update
    void Start()
    {

    }

    void LateUpdate()
    {

        Vector3 temp = transform.position;
        temp.x = playerTransform.position.x;
        temp.y = playerTransform.position.y;

        temp.x += cameraOffsetX;
        temp.y += cameraOffsetY;
        transform.position = temp;

    }
}
