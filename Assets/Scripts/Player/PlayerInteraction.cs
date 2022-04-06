using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] int GRABI;
    [SerializeField] float grabPower = 10f; // grab
    [SerializeField] float throwingSpeed = 10f; //velocity of force
    [SerializeField] float RayDistance = 5f; //distance

    private bool Grab = false;
    private bool Throw = false;
    public Transform offset;
    [SerializeField] Camera camera;
    RaycastHit hit; //луч


    private void Start()
    {
        GRABI = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, RayDistance);
            if (hit.rigidbody)
            {
                GRABI += 1;
                switch (GRABI)
                {
                    case 1:
                        Grab = true;
                        break;
                    case 2:
                        Grab = false;
                        break;
                }

                if (GRABI == 3 || Grab == false)
                {
                    GRABI = 0;
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && Grab)
        {
            GRABI = 0;
            Grab = false;
            Throw = true;
        }


        if (Grab && hit.rigidbody)
        {
            hit.rigidbody.velocity = (offset.position - (hit.transform.position + hit.rigidbody.centerOfMass)) *
                                     grabPower;
        }

        if (Throw && hit.rigidbody)
        {
            //ф-ция толчка
            hit.rigidbody.velocity = camera.ScreenPointToRay(Input.mousePosition).direction * throwingSpeed;
            Throw = false;
        }
    }

    private void Grabb()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, RayDistance);
        if (hit.rigidbody)
        {
            Grab = true;
        }
    }
}