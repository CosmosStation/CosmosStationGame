using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;


public enum EqupableType
{
    Individual,
    ByTag
}

namespace Interactables
{
	[RequireComponent(typeof(Rigidbody))]
	public class Equipable : InteractableObject
    {
        [Header("Action interactions"), ValueDropdown("EquipableTypeOptions")]
        public EqupableType selectedEquipableType;
        [ShowIf("IsIndividualSelected")]
        public SerializedDictionary<GameObject, UnityEvent> interactionsForIndivivduals =
            new SerializedDictionary<GameObject, UnityEvent>();
        [ShowIf("IsByTagSelected"), SerializeField]
        private UnityEvent<GameObject> interactionsForTag = new UnityEvent<GameObject>();
        [ShowIf("IsByTagSelected"), ValueDropdown("GetAllTags"), SerializeField] 
        private string selectedTag;

        private ValueDropdownList<string> GetAllTags()
        {
            ValueDropdownList<string> tags = new ValueDropdownList<string>();
            foreach (var tag in UnityEditorInternal.InternalEditorUtility.tags)
            {
                tags.Add(tag, tag);
            }

            return tags;
        }

        private static readonly ValueDropdownList<EqupableType> EquipableTypeOptions = new ValueDropdownList<EqupableType>
        {
            { "Individual", EqupableType.Individual },
            { "By Tag", EqupableType.ByTag }
        };

        private bool IsIndividualSelected() => selectedEquipableType == EqupableType.Individual;
        private bool IsByTagSelected() => selectedEquipableType == EqupableType.ByTag;
        
        [Header("Parameters")]
        [SerializeField] int _force = 30;
        [SerializeField] Collider _connectedTrigger;
        [SerializeField] bool _onlyOneTimeConnect;
        [SerializeField] private Transform _playerGrabPoint;
        [SerializeField] private Transform _playerCameraRoot;

        bool _oneTimeConnected, _avoidTriggerFix;
        bool _isMove;
        Rigidbody _rb;
        private BoxCollider _boxCollider;
        Transform _camTransform;
        float _targetDistanceToCam;

        Vector3 _targetPos => _camTransform.position + _camTransform.forward * _targetDistanceToCam;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _boxCollider = GetComponent<BoxCollider>();
            _camTransform = Camera.main.transform;

            interactType = "Equipable";

            if (_connectedTrigger != null && !_connectedTrigger.isTrigger)
            	_connectedTrigger.isTrigger = true;
        }

        void FixedUpdate()
        {
            if (!_isMove) return;

            Vector3 force = _playerGrabPoint.position - HitPos;
            force *= _force * 2;
            transform.rotation = _playerCameraRoot.transform.rotation;
            _rb.AddForceAtPosition(force, HitPos);
        }

        public override void InteractStart(RaycastHit hit)
        {
            Debug.Log("Equipable interaction");
            base.InteractStart(hit);
            if (_onlyOneTimeConnect && _oneTimeConnected) return;
            
            if (_rb.isKinematic)
            {
                _avoidTriggerFix = true;
            	_rb.isKinematic = false; // OnTriggerEnter() after this line! It is reason to use _avoidTriggerFix
                
                if (_mainExecutor != null) _mainExecutor.Execute(0);
            }
            _rb.useGravity = false;
            _rb.drag = 8;
            _rb.angularDrag = 8;
            _targetDistanceToCam = hit.distance;
            _isMove = true;
        }
        public override void InteractEnd()
        {
            base.InteractEnd();
            _rb.useGravity = true;
            _rb.drag = 1;
            _rb.angularDrag = 1;
            _isMove = false;
        }
        
        void OnTriggerEnter(Collider other)
        {
        	if (other != _connectedTrigger) return;
            if (_avoidTriggerFix)
            {
                _avoidTriggerFix = false;
                return;
            }

        	_rb.isKinematic = true;
        	InteractEnd();

        	transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;

            if (_mainExecutor != null) _mainExecutor.Execute(1);
        	if (_onlyOneTimeConnect) _oneTimeConnected = true;
        }

        public void InteractWith(GameObject target)
        {
            Debug.Log("Interact through equipped");
            if (IsIndividualSelected() &&
                interactionsForIndivivduals.TryGetValue(target, out UnityEvent interactionEventForGameObject))
            {
                Debug.Log("There is a match");
                interactionEventForGameObject?.Invoke();
            } else if (IsByTagSelected() && target.CompareTag(selectedTag))
            {
                Debug.Log("By Tag");
                interactionsForTag.Invoke(target);
            } 
        }
    }
}
