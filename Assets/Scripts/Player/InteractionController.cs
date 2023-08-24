using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Interactables;

namespace Player
{
    public class InteractionController : MonoBehaviour
    {
        #region delcarations
        [Header("OUTSIDE INFLUENCE")] [SerializeField]
        private GrabMethods monsterGrabManager;

        private bool _isGrabbed = false;

        [Header("INTERACTION PARAMETERS")] public LayerMask layerMaskInteract;
        [SerializeField] HandUI hand;
        InteractableObject _interactable;
        RectTransform _handRect;

        [SerializeField] int interactRange = 3;
        public enum HandMode { canUse, grab, door, button}

        Camera _camera;
        private InputHandler _input;
        private RaycastHit _currentHit;
        private bool _isReadyToInteract = false;

        private AbsorbItem _absorbItem;

        // private ChangeColorItem _changeColorItem;
        private ItemPickUp _pickUpItem;
        private InspectableItem _inspectableItem;
        private List<Tween> _tweens;
        private GameObject _objectHeld;
        private bool _isObjectHeld;
        private bool _tryPickupObject;

        public float LookSpeedMultiply { get; private set; } = 1;
        #endregion

        void Start()
        {
            _input = GetComponent<InputHandler>();
            _camera = Camera.main;
            Cursor.lockState = CursorLockMode.Locked; // TODO displace to GameManager

            if (hand)
            {
                hand.SetEnableImage(false);
                _handRect = hand.GetComponent<RectTransform>();
                _handRect.position = new Vector3(Screen.width / 2, Screen.height / 2);
            }

            GameEvents.current.onPlayerGrabbed += onGrabbed;
        }

        void onGrabbed()
        {
            Debug.Log("Player Grabbed");
            _isGrabbed = true;
        }

        void FixedUpdate()
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _currentHit, interactRange, layerMaskInteract))
            {
                var target = _currentHit.transform.GetComponent<InteractableObject>();
                if (target)
                {
                    if (hand) hand.SetEnableImage(true);
                    if (!_interactable && hand) hand.SetTexture(HandMode.canUse);

                    if (_input.interact)
                    {
                        Debug.Log("INTERACT");
                        _interactable = target;
                        _interactable.InteractStart(_currentHit);
                        LookSpeedMultiply = _interactable.LookingSpeed;

                        if (hand) hand.SetTexture(_interactable.Hand);
                    }
                }
                else if (!_interactable && hand) hand.SetEnableImage(false);
            }
            else if (!_interactable && hand) hand.SetEnableImage(false);

            if (_interactable == null) return;

            if (hand) _handRect.position = _camera.WorldToScreenPoint(_interactable.HitPos);
        
            if (!_input.interact) //  || _interactCurerntDistance < _maxInteractDistance
            {
                _interactable.InteractEnd();
                _interactable = null;
                LookSpeedMultiply = 1;
                if (hand != null)
                {
                    hand.SetEnableImage(false);
                    _handRect.position = new Vector3(Screen.width / 2, Screen.height / 2);
                }
            }

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _currentHit,
                    interactRange,
                    layerMaskInteract.value))
            {
                Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _currentHit.distance,
                    Color.green);
                // Debug.Log("Ray hit");
                hand.transform.localEulerAngles = new Vector3(-35, -39, -121);
                _isReadyToInteract = true;
            }
            else
            {
                Debug.DrawRay(_camera.transform.position, _camera.transform.forward * interactRange,
                    Color.red);
                // Debug.Log("Ray did not hit");
                hand.transform.localEulerAngles = new Vector3(0, -13, 0);
                _isReadyToInteract = false;
            }
        }
    }
}
