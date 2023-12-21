using System;
using HPhysic;
using Player;
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
            Debug.Log(_connector);
            Debug.Log(_targetConnector);
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