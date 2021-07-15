using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public int itemCount;
    float h;
    float v;
    bool jDown;
    bool isJump;
    Rigidbody rigid;
    public CameraMove mainCamera;
    Transform warpPos;
    Vector3 moveVec;
    Animator anim;

    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GetInput();
        Jump();
        Move();
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Field")
        {
            isJump = false;
            anim.SetBool("isJump", false);
        }
        else if (collision.gameObject.tag == "GameManager")
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (collision.gameObject.tag == "Warp")
        {
            warpPos = GameObject.FindGameObjectWithTag("WarpPoint").transform;
            transform.position = warpPos.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene("Scene01");
        }
        else if (other.gameObject.tag == "Item")
        {
            itemCount++;
            other.gameObject.SetActive(false);
        }
    }

    void GetInput()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        jDown = Input.GetButtonDown("Jump");

    }

    void Move()
    {
        if (mainCamera.rotateCount == 0)
            moveVec = new Vector3(h, 0, v).normalized;
        else
            moveVec = new Vector3(-h, 0, -v).normalized;

        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);

        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
        }
    }
}
