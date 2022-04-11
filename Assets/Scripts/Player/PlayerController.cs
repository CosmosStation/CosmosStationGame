using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cam;

    Quaternion startingRotation;

    float ver, hor, jump, rothor, rotver;

    bool isGround;

    float currentSpeed = 0.1f, jumpspeed = 100, sensetivity = 0.5f;

    [SerializeField] float runSpeed = 15, stepSpeed = 5, normalSpeed = 10;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        startingRotation = transform.rotation;
        ver = 0.0f;
        hor = 0.0f;
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }


    void FixedUpdate()
    {
        // if (Input.GetKey(KeyCode.LeftShift))
        // {
        //     currentSpeed = runSpeed;
        // }
        // else if (Input.GetKey(KeyCode.LeftControl))
        // {
        //     currentSpeed = stepSpeed;
        // }
        // else
        // {
        //     currentSpeed = normalSpeed;
        // }
        if (isGround)
        {
            // jump = Input.GetAxis("Jump") * Time.deltaTime * jumpspeed;
        
            // GetComponent<Rigidbody>().AddForce(transform.up * jump, ForceMode.Impulse);
        }
        transform.Translate(new Vector3(hor, 0, ver));
    }

    private void Update()
    {
        Quaternion rotY = Quaternion.AngleAxis(rothor, Vector3.up);
        Quaternion rotX = Quaternion.AngleAxis(-rotver, Vector3.right);
        
        cam.transform.rotation = startingRotation * transform.rotation * rotX;
        transform.rotation = startingRotation * rotY;
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 v = context.ReadValue<Vector2>();
        hor = v.x * currentSpeed;
        ver = v.y * currentSpeed;
        Debug.Log(v);
    }

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 v = context.ReadValue<Vector2>();

        rothor += v.x * sensetivity;
        rotver += v.y * sensetivity;
        
        rotver = Mathf.Clamp(rotver, -60, 60);
        
        Debug.Log(v);
    }

    public void Use(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }
}