using System;
using Wires;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Interactables.Equipables
{
    public class connectorToConnectorHandler : AbstractEquipToTargetHandler
    {
        [SerializeField] private InteractionController _interaction;
        
        private Connector _connector;
        private Connector _targetConnector;
        
        private void Start()
        {
            _connector = GetComponent<Connector>();
        }

        public override void HandleInteraction(GameObject target)
        {
            _targetConnector = target.GetComponent<Connector>();
            if (!_targetConnector)
            {
                Debug.LogError("No connector at target");
            }
            else
            {
                HandleConnection();
            }
        }

        private void HandleConnection()
        {
            Debug.Log("Handling connection");
            Debug.Log(_connector.GameObject().name);
            Debug.Log(_targetConnector.GameObject().name);
            bool isSameSex = _connector.ConnectionType == _targetConnector.ConnectionType;
            if (!isSameSex)
            {
                _interaction.DropEquipped();
                _connector.Connect(_targetConnector);
            }
            else
            {
                Debug.LogWarning("Same sex connection");
            }
        }
    }
}