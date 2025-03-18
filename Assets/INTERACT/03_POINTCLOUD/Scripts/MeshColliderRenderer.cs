// Copyright (C) 2018 CEA LIST, DRT/DIASI/LSI
// Author: Jeremie Le Garrec
// jeremie_legarrec@hotmail.com

using UnityEngine;

namespace xde.unity
{
    public class MeshColliderRenderer : MonoBehaviour
    {
        private Color32[] m_cachedColors;
        public Color32 m_color = Color.yellow;
        private Material[] m_cachedMaterials;
        public OctopclMeshCollider m_meshCollider;

        private void Start()
        {
            m_meshCollider.IntersectTriggerOn += TriggerOn;
            m_meshCollider.IntersectTriggerOff += TriggerOff;
            m_cachedMaterials = GetComponent<Renderer>().materials;
            m_cachedColors = new Color32[m_cachedMaterials.Length];
            for (int l_i = 0; l_i < m_cachedMaterials.Length; l_i++)
                m_cachedColors[l_i] = m_cachedMaterials[l_i].color;
        }

        private void OnDisable()
        {
            m_meshCollider.IntersectTriggerOn -= TriggerOn;
            m_meshCollider.IntersectTriggerOff -= TriggerOff;
        }

        private void TriggerOn()
        {
            foreach (Material l_mat in m_cachedMaterials)
                l_mat.color = m_color;
        }
        private void TriggerOff()
        {
            for (int l_i = 0; l_i < m_cachedMaterials.Length; l_i++)
                m_cachedMaterials[l_i].color = m_cachedColors[l_i];

        }
    }
}
