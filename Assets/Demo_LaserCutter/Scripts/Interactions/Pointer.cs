//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by CEA
//
//=============================================================================

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XdeEngine.Core;
using Outline = Outliner.Outline;

namespace VMachina
{
    public class Pointer : MonoBehaviour
    {
        [SerializeField] protected float defaultLength = 100f;

        private LineRenderer lineRenderer = null;

        public XdeMeshCollider targetColliderXde;
        public XdeMeshCollider lastColliderXde;
        public Collider targetCollider;
        public Collider lastCollider;
        bool hadOutline;
        int outlineColor;

        EventSystem m_EventSystem;

        public GameObject sphere;

        public bool isLeft;
        public Material sphereMaterial;

        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.005f;
            lineRenderer.endWidth = 0.005f;

            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            sphere.GetComponent<MeshRenderer>().material = sphereMaterial;

            Destroy(sphere.GetComponent<SphereCollider>());
        }

        private void Update()
        {
            UpdateLength();

            if (lastColliderXde && lastColliderXde != targetColliderXde)
            {
                if (lastColliderXde.GetComponent<Outline>())
                {
                    if (hadOutline)
                    {
                        //foreach (Outline outline in lastColliderXde.body.GetComponentsInChildren<Outline>())
                        //    outline.color = outlineColor;

                        foreach (Outline outline in lastColliderXde.GetComponentsInChildren<Outline>())
                            outline.color = outlineColor;
                    }
                    else
                    {
                        //foreach (Outline outline in lastColliderXde.body.GetComponentsInChildren<Outline>())
                        //    Destroy(outline);

                        foreach (Outline outline in lastColliderXde.GetComponentsInChildren<Outline>())
                            Destroy(outline);
                    }

                }

                lastColliderXde = null;
            }

            if (lastCollider && lastCollider != targetCollider)
            {
                Button lButton = lastCollider.GetComponent<Button>();
                if (lButton)
                    lButton.OnPointerExit(new PointerEventData(EventSystem.current));

                Toggle lToggle = lastCollider.GetComponent<Toggle>();
                if (lToggle)
                    lToggle.OnPointerExit(new PointerEventData(EventSystem.current));

                lastCollider = null;
            }

            //if (targetColliderXde != null && targetColliderXde.body.GetComponent<XdeAsbSelectablePart>())
            if (targetColliderXde != null && targetColliderXde.GetComponent<XdeAsbSelectablePart>())
            {
                if (targetColliderXde.GetComponent<Outline>() == null)
                {
                    //foreach (XdeMeshCollider mesh in targetColliderXde.body.GetComponentsInChildren<XdeMeshCollider>())
                    foreach (XdeMeshCollider mesh in targetColliderXde.GetComponentsInChildren<XdeMeshCollider>())
                    {
                        hadOutline = false;
                        Outline lOutline = mesh.gameObject.AddComponent<Outline>();
                        lOutline.color = 2;
                    }
                }
                else if (targetColliderXde != lastColliderXde)
                {
                    //foreach (XdeMeshCollider mesh in targetColliderXde.body.GetComponentsInChildren<XdeMeshCollider>())
                    foreach (XdeMeshCollider mesh in targetColliderXde.GetComponentsInChildren<XdeMeshCollider>())
                    {
                        Outline lOutline = mesh.GetComponent<Outline>();

                        //var outline = mesh.GetComponent<Outline>();
                        if (lOutline)
                        {
                            outlineColor = lOutline.color;
                            lOutline.color = 2;
                        }

                        hadOutline = true;
                    }
                }

                lastColliderXde = targetColliderXde;
            }

            if (targetCollider)
            {
                lastCollider = targetCollider;

                Button lButton = targetCollider.GetComponent<Button>();
                if (lButton)
                {
                    lButton.OnPointerEnter(new PointerEventData(EventSystem.current));
                    return;
                }

                Toggle lToggle = targetCollider.GetComponent<Toggle>();
                if (lToggle)
                {
                    lToggle.OnPointerEnter(new PointerEventData(EventSystem.current));
                    return;
                }
            }
        }

        public void TryDestroy()
        {
            if (lastColliderXde.GetComponent<Outline>())
            {
                if (hadOutline)
                {
                    //foreach (Outline outline in lastColliderXde.body.GetComponentsInChildren<Outline>())
                    //    outline.color = outlineColor;

                    foreach (Outline outline in lastColliderXde.GetComponentsInChildren<Outline>())
                        outline.color = outlineColor;
                }
                else
                {
                    //foreach (Outline outline in lastColliderXde.body.GetComponentsInChildren<Outline>())
                    //    Destroy(outline);

                    foreach (Outline outline in lastColliderXde.GetComponentsInChildren<Outline>())
                        Destroy(outline);
                }

            }

            lastColliderXde = null;
        }

        private void UpdateLength()
        {
            if (lineRenderer == null)
                return;

            Vector3 lHitPos = CalculateEnd();

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, lHitPos);

            sphere.transform.position = lHitPos;
        }

        protected Vector3 CalculateEnd()
        {
            Vector3 endPosition = DefaultEnd(defaultLength);

            Ray ray = new Ray(transform.position, transform.forward);
            Physics.Raycast(ray, out RaycastHit hit, defaultLength);

            targetCollider = null;
            targetColliderXde = null;

            if (hit.collider)
            {
                XdeMeshCollider lMeshCollider = hit.collider.GetComponent<XdeMeshCollider>();

                if (lMeshCollider)
                    targetColliderXde = lMeshCollider;
                else if (hit.collider.CompareTag("UI"))
                    targetCollider = hit.collider;

                endPosition = hit.point;
            }

            return endPosition;
        }

        private Vector3 DefaultEnd(float length)
        {
            return transform.position + (transform.forward * length);
        }
    }
}