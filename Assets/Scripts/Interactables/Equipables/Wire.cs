using Wires;
using Unity.VisualScripting;
using UnityEngine;

namespace Interactables
{
    namespace Equipables
    {
        public class Wire : Equipable
        {
            private Connector _connector;

            void Awake()
            {
                _connector = GetComponent<Connector>();
            }
            
            public override void InteractStart(RaycastHit hit)
            {
                Debug.Log("Wire grabbed");
                if (_connector.IsConnected)
                {
                    _connector.Disconnect(_connector.ConnectedTo);
                }
                base.InteractStart(hit);
            }
        }
    }
}