using UnityEngine;

public class interactivity : MonoBehaviour
{
    private float distance = 3;
    [SerializeField] Transform pos;
    private Rigidbody rigidBody;
    private Options param;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        param = GameObject.Find("First person controller").GetComponent<Options>();
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, distance) && param.take == false)
        {
            rigidBody.isKinematic = true;
            param.take = true;
            rigidBody.MovePosition(pos.position);
        }
    }

    private void FixedUpdate()
    {
        if (rigidBody.isKinematic)
        {
            gameObject.transform.position = pos.position;
            if (Input.GetKeyDown(KeyCode.G))
            {
                param.take = false;
                rigidBody.useGravity = true;
                rigidBody.isKinematic = false;
                rigidBody.AddForce(Camera.main.transform.forward * 500);
            }
        }
    }
}