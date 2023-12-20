using DG.Tweening;
using UnityEngine;
using Player;

public class PositionCamera : MonoBehaviour
{
    [SerializeField] private FirstPersonController PlayerController;

    public Transform cameraPositionTarget;
    public GameObject camera;

    private Vector3 _initialPosition;

    public void SetCameraPosition()
    {
        PlayerController.LockCamera();
        PlayerController.LockPlayer();

        _initialPosition = new Vector3(
            camera.transform.position.x,
            camera.transform.position.y,
            camera.transform.position.z
            );
        Debug.Log(camera.transform.position);
        Debug.Log(cameraPositionTarget.position);
        camera.transform.DOMove(cameraPositionTarget.position, 2, false);
        camera.transform.DORotate(cameraPositionTarget.forward, 2);
    }

    public void ResetCameraPosition()
    {
        camera.transform.DOMove(_initialPosition, 2, true);
        
        PlayerController.UnlockCamera();
        PlayerController.UnlockPlayer();
    }
}
