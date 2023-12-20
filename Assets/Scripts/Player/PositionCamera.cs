using DG.Tweening;
using UnityEngine;
using Player;

public class PositionCamera : MonoBehaviour
{
    [SerializeField] private FirstPersonController PlayerController;

    public Transform cameraPositionTarget;
    public GameObject camera;

    private Transform _initialPosition;

    public void SetCameraPosition()
    {
        PlayerController.LockCamera();
        PlayerController.LockPlayer();

        _initialPosition = camera.transform;
        camera.transform.DOMove(cameraPositionTarget.position, 2, true);
        camera.transform.DORotate(cameraPositionTarget.forward, 2);
    }

    public void ResetCameraPosition()
    {
        camera.transform.DOMove(_initialPosition.position, 2, true);
        
        PlayerController.UnlockCamera();
        PlayerController.UnlockPlayer();
    }
}
