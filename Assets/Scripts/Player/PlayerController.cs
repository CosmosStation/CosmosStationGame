using System;
using PixelCrushers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cam;

    Quaternion startingRotation;

    float ver, hor, jump, rothor, rotver;

    bool isGround;

    [Header("MOVEMENT PARAMETERS")] 
    [SerializeField] float currentSpeed = 0.1f;
    [SerializeField] float jumpspeed = 100, sensetivity = 0.01f, runSpeed = 15, stepSpeed = 5, normalSpeed = 10;
    
    [Header("INTERACTION PARAMETERS")]
    public LayerMask layerMaskInteract;
    public Image interactiveCursor;
    
    private RaycastHit currentHit;

    [SerializeField]
    private float interactRange = 30f;
    private bool isReadyToInteract = false;

    [Header("PICKUP PARAMETERS")] 
    [SerializeField] private Transform holdArea;

    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;
    
    
    private GameObject heldObj;
    private Rigidbody heldObjRB;
    
    
    // Story related flags
    private bool isSitting;
    [Header("STORY PARAMETERS")]
    public bool isAbleToStand;

    void Start()
    {
        Cursor.visible = false;
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

        RaycastHit hit;

        Transform cameraTransform = GetComponentInChildren<Camera>().transform;
        
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactRange, layerMaskInteract.value))
        {
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * hit.distance, Color.green);
            // Debug.Log("Ray hit");
            currentHit = hit;
            interactiveCursor.color = Color.green;
            isReadyToInteract = true;
        }
        else
        {
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * interactRange, Color.red);
            // Debug.Log("Ray did not hit");
            interactiveCursor.color = Color.white;
            isReadyToInteract = false;
        }
        
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

        if (heldObj)
        {
            MoveObject();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 v = context.ReadValue<Vector2>();
        hor = v.x * currentSpeed;
        ver = v.y * currentSpeed;
        // Debug.Log(v);
    }

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 v = context.ReadValue<Vector2>();

        rothor += v.x * sensetivity;
        rotver += v.y * sensetivity;
        
        rotver = Mathf.Clamp(rotver, -60, 60);
        
        // Debug.Log(v);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!isReadyToInteract || context.phase != InputActionPhase.Performed) return;
        Debug.Log(currentHit);

        if (heldObj == null)
        {
            switch (currentHit.transform.tag)
            {
                case "PickableItem":
                    Debug.Log("Picked up");
                
                    heldObj = currentHit.transform.gameObject;
                    heldObjRB = currentHit.rigidbody;

                    heldObjRB.useGravity = false;
                    heldObjRB.drag = 10;
                    heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

                    heldObjRB.transform.parent = holdArea;
                    break;
                case "ButtonItem":
                    Debug.Log("Button pressed");

                    GameObject gameObj = currentHit.transform.gameObject;
                    ButtonItem buttonItm = gameObj.GetComponent<ButtonItem>();
                    buttonItm.pressButton();
                    
                    break;
            }
        }
        else
        {
            heldObjRB.useGravity = true;
            heldObjRB.drag = 1;
            heldObjRB.constraints = RigidbodyConstraints.None;

            heldObjRB.transform.parent = null;
            heldObjRB = currentHit.rigidbody;
            heldObj = null;
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = holdArea.position - heldObj.transform.position;
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }
}