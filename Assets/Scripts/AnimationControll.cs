using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControll : MonoBehaviour
{
    Animator animator;
    float velocity = 0f;
    float acceleration = 0.1f;
    float deceleration = 0.5f;
    int isRunningHash;
    int isWalkingHash;
    int VelocityHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        VelocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    { //if (Input.GetAxis("Vertical") != 0f)
        bool isRunning = animator.GetBool(isRunningHash); 
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        Debug.Log(isWalking+ "  walking");


        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }
        // else { animator.SetBool("StartWalking", false); }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if (isWalking && forwardPressed && runPressed)
        {
            animator.SetBool(isRunningHash, true);

        }
        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }
        if (forwardPressed && velocity < 1f)
        {
            velocity += Time.deltaTime * acceleration;
        }
        if( !forwardPressed && velocity >0f)
        {
            velocity -= Time.deltaTime * deceleration;
        }
        if (!forwardPressed && velocity < 0f)
        {
            velocity = 0;
        }
        animator.SetFloat(VelocityHash,velocity);


    }
}
