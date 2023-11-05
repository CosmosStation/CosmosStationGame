using UnityEngine;
using Interactables;

namespace Player
{
    public class InteractionController : MonoBehaviour
    {
        #region delcarations

        [Header("INTERACTION PARAMETERS")] public LayerMask layerMaskInteract;
        [SerializeField] HandUI hand;
        InteractableObject _interactable;
        RectTransform _handRect;

        [SerializeField] int interactRange = 3;
        public enum HandMode { canUse, grab, door, button}

        public float LookSpeedMultiply { get; private set; } = 1;

        public Equipable equipedItem = null;
        private bool isItemEquiped = false;
        
        Camera _camera;
        private InputHandler _input;
        private RaycastHit _currentHit;
        private bool _isReadyToInteract = false;
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

        }

        void FixedUpdate()
        {
            ManageInteractable();
            ManageDrop();
            ManageEquipedInteract();
            drawDebug();
        }
    
        private void ManageInteractable()
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
                        if (target.interactType.Equals("Equipable"))
                        {
                            if (equipedItem == null)
                            {
                                equipedItem = target.GetComponent<Equipable>();
                                equipedItem.InteractStart(_currentHit);
                            }
                        }
                        else
                        {
                            _interactable = target;
                            _interactable.InteractStart(_currentHit);
                            LookSpeedMultiply = _interactable.LookingSpeed;

                            if (hand) hand.SetTexture(_interactable.Hand);
                        }
                    }
                }
                else if (!_interactable && hand) hand.SetEnableImage(false);
            }
            else if (!_interactable && hand) hand.SetEnableImage(false);

            if (_interactable)
            {
                if (hand) _handRect.position = _camera.WorldToScreenPoint(_interactable.HitPos);
        
                if (!_input.interact && _interactable) //  || _interactCurerntDistance < _maxInteractDistance
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
            }
        }

        private void ManageDrop()
        {
            if (_input.drop && equipedItem)
            {
                DropEquipped();
            }
        }

        public void DropEquipped()
        {
            equipedItem.InteractEnd();
            equipedItem = null;
        }

        private void ManageEquipedInteract()
        {
            if (_input.action && equipedItem)
            {
                Debug.Log(_input.action, equipedItem);
                if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _currentHit,
                        interactRange, layerMaskInteract))
                {
                    equipedItem.InteractWith(_currentHit.transform.gameObject);
                }
            }
        }

        private void drawDebug()
        {
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
