//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by CEA
//
//=============================================================================

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR;
using XdeEngine.Core;
using XdeNetwork;

namespace VMachina
{
    public class LaserPointerRaycast : MonoBehaviour
    {
        GraphicRaycaster m_Raycaster;
        PointerEventData m_PointerEventData;
        EventSystem m_EventSystem;
        public Material raycastMaterial;
        public Color m_LaserColor;

        public SteamVR_Input_Sources handLeft;
        public SteamVR_Input_Sources handRight;
        public SteamVR_Action_Boolean triggerAction;
        public SteamVR_Action_Boolean shootRaycast;
        
        [SerializeField]
        private Transform m_leftRaycastOrigin;
        [SerializeField]
        private Transform m_rightRaycastOrigin;

        private Pointer leftPointer;
        private Pointer rightPointer;
        
        private GameObject leftRaycast;
        private GameObject rightRaycast;

        private void Start()
        {
            leftRaycast = GenerateRaycast(true);
            leftRaycast.SetActive(false);
            rightRaycast = GenerateRaycast(false);
            rightRaycast.SetActive(false);
            
            triggerAction.AddOnStateDownListener(TriggerDown, handRight);
            triggerAction.AddOnStateDownListener(TriggerDown, handLeft);
            shootRaycast.AddOnStateDownListener(UpdateRaycast, handRight);
            shootRaycast.AddOnStateUpListener(HideRaycast, handRight);
            shootRaycast.AddOnStateDownListener(UpdateRaycast, handLeft);
            shootRaycast.AddOnStateUpListener(HideRaycast, handLeft);
        }

        private void OnDestroy()
        {
            Destroy(leftRaycast);
            Destroy(rightRaycast);

            triggerAction.RemoveOnStateDownListener(TriggerDown, handRight);
            triggerAction.RemoveOnStateDownListener(TriggerDown, handLeft);
            shootRaycast.RemoveOnStateDownListener(UpdateRaycast, handRight);
            shootRaycast.RemoveOnStateUpListener(HideRaycast, handRight);
            shootRaycast.RemoveOnStateDownListener(UpdateRaycast, handLeft);
            shootRaycast.RemoveOnStateUpListener(HideRaycast, handLeft);
        }

        private void Update()
        {
            leftRaycast.transform.position = m_leftRaycastOrigin.position;
            rightRaycast.transform.position = m_rightRaycastOrigin.position;

            leftRaycast.transform.rotation = m_leftRaycastOrigin.rotation * Quaternion.Euler(0f, 90f, 0f);
            rightRaycast.transform.rotation = m_rightRaycastOrigin.rotation * Quaternion.Euler(0f, 90f, 0f);
        }

        public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            if (fromSource == handLeft && leftPointer)
                PerformPointerTriggerDownDetection(leftPointer, fromSource);

            if (fromSource == handRight && rightPointer)
                PerformPointerTriggerDownDetection(rightPointer, fromSource);
        }

        private void PerformPointerTriggerDownDetection(Pointer pPointer, SteamVR_Input_Sources pFromSource)
        {
            string lPointerLabel = (pFromSource == handLeft ? "Left" : "Right");

            XdeMeshCollider lMeshCollider = pPointer.targetColliderXde?.GetComponent<XdeMeshCollider>();
            if (lMeshCollider)
            {
                Debug.Log("[" + lPointerLabel + "Pointer] Selecting object " + lMeshCollider.name);
                TrySelect(lMeshCollider);
                return;
            }

            Button lButton = pPointer.targetCollider?.GetComponent<Button>();
            if (lButton)
            {
                Debug.Log("[" + lPointerLabel + "Pointer] Selecting UI Button " + lButton.name);
                lButton.OnPointerClick(new PointerEventData(EventSystem.current));
            }

            Toggle lToggle = pPointer.targetCollider?.GetComponent<Toggle>();
            if (lToggle)
            {
                Debug.Log("[" + lPointerLabel + "Pointer] Selecting UI Toggle " + lToggle.name);
                lToggle.OnPointerClick(new PointerEventData(EventSystem.current));
            }
        }

        private void TrySelect(XdeMeshCollider mesh)
        {
            XdeAsbSelectablePart lPart = mesh.GetComponent<XdeAsbSelectablePart>();

            if (lPart)
            {
                lPart.Select();
                return;
            }

            lPart = mesh.body.GetComponent<XdeAsbSelectablePart>();

            if (lPart)
                lPart.Select();
        }

        public GameObject GenerateRaycast(bool isLeft)
        {
            GameObject lRaycast = new GameObject("raycast_" + (isLeft ? "left" : "right"));
            lRaycast.transform.parent = this.transform;

            LineRenderer line = lRaycast.AddComponent<LineRenderer>();
            line.startColor = m_LaserColor;
            line.endColor = Color.black;
            line.material = raycastMaterial;
            line.endWidth = 0.01f;
            line.startWidth = 0.01f;

            Pointer lPointer = lRaycast.AddComponent<Pointer>();
            lPointer.sphereMaterial = raycastMaterial;
            lPointer.isLeft = isLeft;

            if (isLeft)
                leftPointer = lPointer;
            else
                rightPointer = lPointer;

            return lRaycast;
        }

        public void UpdateRaycast(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            if (fromSource == handLeft)
                leftRaycast.SetActive(true);
            else
                rightRaycast.SetActive(true);
        }

        public void HideRaycast(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            bool isLeft = (fromSource == handLeft);

            if (isLeft && leftPointer != null)
            {
                leftRaycast.SetActive(false);
                if (leftPointer.lastCollider != null && leftPointer.lastCollider.GetComponent<Button>())
                    leftPointer.lastCollider.GetComponent<Button>()
                               .OnPointerExit(new UnityEngine.EventSystems.PointerEventData(EventSystem.current));

                if (leftPointer.lastCollider != null && leftPointer.lastCollider.GetComponent<Toggle>())
                    leftPointer.lastCollider.GetComponent<Toggle>()
                               .OnPointerExit(new UnityEngine.EventSystems.PointerEventData(EventSystem.current));

                if (leftPointer.lastColliderXde != null)
                    leftPointer.TryDestroy();

                leftPointer.targetColliderXde = null;
                leftPointer.targetCollider = null;
            }
            else if (rightPointer != null)
            {
                rightRaycast.SetActive(false);
                if (rightPointer.lastCollider != null && rightPointer.lastCollider.GetComponent<Button>())
                    rightPointer.lastCollider.GetComponent<Button>()
                                .OnPointerExit(new UnityEngine.EventSystems.PointerEventData(EventSystem.current));

                if (rightPointer.lastCollider != null && rightPointer.lastCollider.GetComponent<Toggle>())
                    rightPointer.lastCollider.GetComponent<Toggle>()
                                .OnPointerExit(new UnityEngine.EventSystems.PointerEventData(EventSystem.current));

                if (rightPointer.lastColliderXde != null)
                    rightPointer.TryDestroy();

                rightPointer.targetColliderXde = null;
                rightPointer.targetCollider = null;
            }
        }

        public void UpdateRaycastCosmetics(Material pMaterial)
        {
            if (leftPointer)
                ApplyMaterialToPointer(leftPointer, pMaterial);

            if (rightPointer)
                ApplyMaterialToPointer(rightPointer, pMaterial);
        }

        private void ApplyMaterialToPointer(Pointer pPointer, Material pMaterial)
        {
            LineRenderer lLineRenderer = pPointer.GetComponent<LineRenderer>();
            lLineRenderer.startColor = pMaterial.color;
            lLineRenderer.material = pMaterial;

            pPointer.sphereMaterial = pMaterial;
        }
    }
}