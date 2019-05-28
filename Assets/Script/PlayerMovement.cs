using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    Rigidbody PlayerRigiBody;

    private void Awake()
    {
        PlayerRigiBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerMovement();
    }

    private void playerMovement()
    {
        float HorizInput = Input.GetAxis("Horizontal");
        float VertInput = Input.GetAxis("Vertical");
        bool isWalking = !Mathf.Approximately(HorizInput, 0f) || !Mathf.Approximately(VertInput, 0f);
        Vector3 moveForward = transform.forward * VertInput;
        Vector3 moveSide = transform.right * HorizInput;

        if (isWalking)
        {
            //Debug.Log("isWalking");
            if (Input.GetButton("Sprint"))
            {
                PlayerRigiBody.velocity = (moveForward + moveSide) * runSpeed;
                //Debug.Log("Running");
            }
            else
            {
                PlayerRigiBody.velocity = (moveForward + moveSide) * walkSpeed;
                //Debug.Log("Walking");
            }
        }
        else
        {
            //Debug.Log("is Not Walking");
            PlayerRigiBody.velocity = new Vector3(0f, 0f, 0f);
        }
    }
}
