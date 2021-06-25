using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float jumpPower;
    public int itemCount;
    bool isJump;
    Rigidbody rigid;
    public CameraMove mainCamera;

    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (mainCamera.rotateCount == 0)
            rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
        else
            rigid.AddForce(new Vector3(-h, 0, -v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Field")
        {
            isJump = false;
        }
        else if (collision.gameObject.tag == "GameManager")
        {
            SceneManager.LoadScene("Tutorial");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (other.gameObject.tag == "Item")
        {
            itemCount++;
            other.gameObject.SetActive(false);
        }
    }
}
