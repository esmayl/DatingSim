using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 50;
    public Transform cam;

    internal Vector3 currentPos;

    Animator anim;
    Rigidbody playerController;
    Vector3 v3Input;

    void Start ()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<Rigidbody>();
        v3Input = Vector3.zero;
        currentPos = transform.position;
	}

    private void FixedUpdate()
    {
        currentPos = transform.position;

        if (Input.anyKey)
        {
            float v = Input.GetAxisRaw("Vertical");
            float h = Input.GetAxisRaw("Horizontal");

            v3Input = v * cam.forward + h * cam.right;
            v3Input.Normalize();
            v3Input *= speed;

            v3Input.y = 0;

            playerController.AddForce(v3Input);

            transform.LookAt(v3Input);

            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);

            v3Input = Vector3.zero;
        }

    }

}

