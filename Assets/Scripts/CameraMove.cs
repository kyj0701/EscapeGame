using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset;
    public PlayerMove player;
    public int rotateCount;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position;
        rotateCount = 0;
    }

    void LateUpdate()
    {
        if (rotateCount == 0)
            transform.position = playerTransform.position + Offset;

        if (rotateCount != player.itemCount)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            Debug.Log(transform.position);
            transform.position = playerTransform.position - Offset;
            Debug.Log(transform.position);
            rotateCount++;
        }

        if (rotateCount != 0 && rotateCount == player.itemCount)
        {
            transform.position = playerTransform.position - Offset;
        }    
    }
}
