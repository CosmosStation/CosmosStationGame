using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class InspectableItem : MonoBehaviour
    {
        public bool isInspecting = false;

        [SerializeField] private float distance = 1;
        [SerializeField] private float smooth = 1;
        [SerializeField] private Transform cameraTransform;

        private Vector3 initialPosition;
        private Quaternion initialRotation;

        void Start()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }

        private void Update()
        {
            if (isInspecting)
            {
                transform.LookAt(cameraTransform);
                // transform.position = Vector3.Lerp(transform.position, cameraTransform.position + cameraTransform.forward * distance, Time.deltaTime * smooth);
                transform.position = cameraTransform.position + cameraTransform.forward * distance;
            }
        }

        public void Inspect(bool shouldInspect)
        {
            isInspecting = shouldInspect;
            if (!shouldInspect)
            {
                transform.position = initialPosition;
                transform.rotation = initialRotation;
            }
        }
    }
}
