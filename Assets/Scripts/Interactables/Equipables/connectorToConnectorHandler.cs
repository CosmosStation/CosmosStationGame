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
            _interaction.DropEquipped();
            _connector.Connect(_targetConnector);
        }
    }
}