using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float jumpPower;
    bool isJump;
    Rigidbody rigid;

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

        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
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
            Debug.Log("Finish");
            SceneManager.LoadScene("Tutorial");
        }
    }
}
