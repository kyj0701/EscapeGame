using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    Rigidbody rigid;
    int rotatePoint;
    int onPlate;
    public GameObject field;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rotatePoint = 0;
        onPlate = 1;
    }
    void Update() 
    {
        if(rotatePoint == 1)
        {
            field.transform.Rotate(new Vector3(0,-90,0) * Time.deltaTime);
        }
        
        if((int)field.transform.eulerAngles.y == 270)
        {
            rotatePoint = 0;
        }
    }
    void OnCollisionEnter(Collision other) 
    {
        if (onPlate == 1)
        {
            if (other.gameObject.tag == "Player")
            {
                transform.localScale = new Vector3(1,0.1f,1);
                rotatePoint = 1;
                onPlate = 0;
            }
        }
    }
}
