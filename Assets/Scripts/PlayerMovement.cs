using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject cameraSetActive;
   

    public float mouse_X;
    public float mouse_Y;
    public float mouseSensivity;

    // public float mouseSensivity;

    
    public float speed = 20f;
    public float horizontalInput;
    public float verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        cameraSetActive.SetActive(false);
        cameraSetActive.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouse_Y += Input.GetAxis("Mouse Y") * -1 * mouseSensivity;
        mouse_X += Input.GetAxis("Mouse X") * mouseSensivity;
        transform.localEulerAngles = new Vector3(0, mouse_X, 0);
        Movement();
       // Rotation();

    }
    void Movement()
    {//i ll change it later on force
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        

        if (horizontalInput > 0)
        {
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        }
        if (horizontalInput < 0)
        {
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }
        if (verticalInput > 0)
        {
            transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed * 2);

        }
        if (verticalInput < 0)
        {
            transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed );
        }
    }
  //  void Rotation()  { }
          
        
    
   
}
