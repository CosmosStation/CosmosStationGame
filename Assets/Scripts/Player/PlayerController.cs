using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cam;

    Quaternion startingRotation;

    float ver, hor, jump, rothor, rotver;

    bool isGround;

    float currentSpeed = 6, jumpspeed = 100, senssensitivity = 5;

    [SerializeField] float runSpeed = 15, stepSpeed = 5, normalSpeed = 10;

    void Start()
    {
        startingRotation = transform.rotation;
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


    void Update()
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
        //
        // rothor += Input.GetAxis("Mouse X") * senssensitivity;
        // rotver += Input.GetAxis("Mouse Y") * senssensitivity;
        //
        // rotver = Mathf.Clamp(rotver, -60, 60);
        //
        // Quaternion rotY = Quaternion.AngleAxis(rothor, Vector3.up);
        // Quaternion rotX = Quaternion.AngleAxis(-rotver, Vector3.right);
        //
        // cam.transform.rotation = startingRotation * transform.rotation * rotX;
        // transform.rotation = startingRotation * rotY;
        //
        // if (isGround)
        // {
        //     ver = Input.GetAxis("Vertical") * Time.deltaTime * currentSpeed;
        //     hor = Input.GetAxis("Horizontal") * Time.deltaTime * currentSpeed;
        //     jump = Input.GetAxis("Jump") * Time.deltaTime * jumpspeed;
        //
        //     GetComponent<Rigidbody>().AddForce(transform.up * jump, ForceMode.Impulse);
        // }
        //
        // transform.Translate(new Vector3(hor, 0, ver));
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    public void Look(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }
}