using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public int tileID;
    public bool wasClicked  = false;
    public bool isEmptyTile = false;

    public bool isSliding = false;
    public Vector3 startPos;
    public Vector3 targetPos;
    private float interpolationParam = 0.0f;
    private float interpolationSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSliding)
        {
            interpolationParam += interpolationSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPos, interpolationParam);

            if (interpolationParam >= 1.0f)
            {
                isSliding = false;
                interpolationParam = 0.0f;
            }
        }
    }

    public void OnMouseDown()
    {
        wasClicked = true;
    }
}
