using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    Rigidbody PlayerRigiBody;
    CharacterController characterController;
    AudioSource walkingSound;

    private void Awake()
    {
        PlayerRigiBody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        walkingSound = GetComponent<AudioSource>();
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
            if (Input.GetButton("Sprint"))
            {
                characterController.SimpleMove(Vector3.ClampMagnitude(moveForward + moveSide, 1.0f) * runSpeed);
                //Debug.Log("Running");
            }
            else
            {
                if (!walkingSound.isPlaying)
                {
                    walkingSound.Play();
                }
                characterController.SimpleMove(Vector3.ClampMagnitude(moveForward + moveSide, 1.0f) * walkSpeed);

            }
        }
        else
        {
            walkingSound.Stop();
        }
    }
}
