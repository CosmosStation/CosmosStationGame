using System.Collections;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using Player;

namespace Interactables
{
    public class DialogueNPC : InteractableObject
    {
        [Header("Dialogue actor")]
        [SerializeField] private GameObject Actor;
        [SerializeField] private GameObject CinemachineTarget;

        [Header("PlayerController")]
        [SerializeField] private FirstPersonController PlayerController;


        private DialogueSystemTrigger dialogueTrigger;

        public override void InteractStart(RaycastHit hit)
        {
            if (CinemachineTarget)
            {
                PlayerController.FocusOnTarget(CinemachineTarget);
            }
            PlayerController.LockPlayer();

            dialogueTrigger = Actor.GetComponent<DialogueSystemTrigger>();
            dialogueTrigger.OnUse();
        }

        private void Start()
        {
            DialogueManager.Instance.conversationEnded += EndConversation;
        }

        private void EndConversation(Transform actor)
        {
            PlayerController.UnlockPlayer();
        }
    }
}

