using UnityEngine;

public class Take : MonoBehaviour
{
    private float distance = 3;
    [SerializeField] Transform position;
    private Rigidbody _rigidBody;
    private Global _fpc;
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _fpc = GameObject.Find("Capsule").GetComponent<Global>();
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, distance) && _fpc.take == false)
        {
            _rigidBody.isKinematic = true;
            _fpc.take = true;
            _rigidBody.MovePosition(position.position);
        }
    }

    void Update()
    {
        if (_rigidBody.isKinematic)
        {
            gameObject.transform.position = position.position;
            if (Input.GetKeyDown(KeyCode.G))
            {
                _fpc.take = false;
                _rigidBody.useGravity = true;
                _rigidBody.isKinematic = false;
                _rigidBody.AddForce(Camera.main.transform.forward * 500);
            }
        }
    }
}
