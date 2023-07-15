using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimationalAnimation : MonoBehaviour
{
    Animator animator;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    float velocityX;
    float velocityZ;
    int velocityXHash;
    int velocityZHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        velocityZHash = Animator.StringToHash("Velocity Z");
        velocityXHash = Animator.StringToHash("Velocity X");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = runPressed? maximumRunVelocity: maximumWalkVelocity;

        animator.SetFloat(velocityZHash, velocityZ);
        animator.SetFloat(velocityXHash, velocityX);
        ChangeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);


    }
    void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity) 
    {
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        if (!forwardPressed && velocityZ > 0)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        if (!leftPressed && velocityX < 0)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        if (!rightPressed && velocityX > 0)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }
    void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity) 
    {
        if (!forwardPressed && velocityZ < 0)
        {
            velocityZ = 0;
        }

        if (!leftPressed && !rightPressed && velocityX != 0 && (velocityX > -0.05f && velocityX < 0.05))
        {
            velocityX = 0;
        }
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {//set max speed
            velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {//start decelerating if this piece of shit is still sprinting
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {//if we are already getting stopped set speed to 0 
            velocityZ = currentMaxVelocity;
        }
    }
}
