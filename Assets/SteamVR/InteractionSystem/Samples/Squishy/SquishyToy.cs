﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem
{
    public class SquishyToy : MonoBehaviour
    {
        public Interactable interactable;
        public new SkinnedMeshRenderer renderer;
        public float gripForce;
        public float pinchForce;
        public bool affectMaterial = true;

        [SteamVR_DefaultAction("Squeeze")]
        public SteamVR_Action_Single gripSqueeze;

        [SteamVR_DefaultAction("Trigger")]
        public SteamVR_Action_Boolean trigger;

        [SteamVR_DefaultAction("Squeeze")]
        public SteamVR_Action_Single pinchSqueeze;


        private new Rigidbody rigidbody;

        public bool triggered;
        private void Start()
        {
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody>();

            if (interactable == null)
                interactable = GetComponent<Interactable>();

            if (renderer == null)
                renderer = GetComponent<SkinnedMeshRenderer>();
        }

        private void Update()
        {
            float grip = 0;
            float pinch = 0;
            
            if (interactable.attachedToHand)
            {
                grip = gripSqueeze.GetAxis(interactable.attachedToHand.handType);
                pinch = pinchSqueeze.GetAxis(interactable.attachedToHand.handType);
                gripForce = grip;
                pinchForce = pinch;

                triggered = trigger.GetState(interactable.attachedToHand.handType);
            }

            renderer.SetBlendShapeWeight(0, Mathf.Lerp(renderer.GetBlendShapeWeight(0), grip * 150, Time.deltaTime * 10));

            if (renderer.sharedMesh.blendShapeCount > 1) // make sure there's a pinch blend shape
                renderer.SetBlendShapeWeight(1, Mathf.Lerp(renderer.GetBlendShapeWeight(1), pinch * 200, Time.deltaTime * 10));

            if (affectMaterial)
            {
                renderer.material.SetFloat("_Deform", Mathf.Pow(grip * 1.5f, 0.5f));
                if (renderer.material.HasProperty("_PinchDeform"))
                {
                    renderer.material.SetFloat("_PinchDeform", Mathf.Pow(pinch * 2.0f, 0.5f));
                }
            }
        }
    }
}