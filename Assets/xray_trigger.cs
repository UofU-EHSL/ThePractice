using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem
{
    public class xray_trigger : MonoBehaviour
    {
        public bool clicked;
        public Interactable interactable;
        public rendure_texture_to_texture2d screenCapture;
        [SteamVR_DefaultAction("Trigger")]
        public SteamVR_Action_Boolean trigger;

        private void Start()
        {
            if (interactable == null)
                interactable = GetComponent<Interactable>();
        }
        private void Update()
        {
            if (interactable.attachedToHand)
            {
                clicked = trigger.GetState(interactable.attachedToHand.handType);
                if (clicked == true)
                {
                    screenCapture.take();
                    clicked = false;
                }
            }
        }
    }
}