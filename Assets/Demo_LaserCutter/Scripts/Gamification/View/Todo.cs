//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using Gamification.Evaluations;
using Gamification.Evaluations.Metrics;
using UnityEngine;
using UnityEngine.UI;
using XdeEngine.Assembly;

namespace Gamification.View
{
	public class Todo : MonoBehaviour
	{
		[SerializeField]
		private Text m_name = null;
		[SerializeField]
		private Toggle m_toggle = null;
		[SerializeField]
		private Text m_chronoLabel = null;
		
		private bool m_isSynchro = false;
		private Chronometer m_chrono;
		private List<Texture2D> m_stepResults;

		public Func<XdeAsbStep,List<Texture2D>> GetStepPictoResult;

		public string Name
		{
			get { return m_name.text; }
			set { m_name.text = value; }
		}

		public Chronometer Chrono 
		{
			set
			{
				if (value != null)
				{
					m_chrono = value;
					TimeSpan l_timeSpan = TimeSpan.FromSeconds(m_chrono.TimeElapsed);
					m_chronoLabel.text = l_timeSpan.ToString(@"mm\:ss");
					m_isSynchro = true;
				}
			}
		}

		public bool IsDone
		{
			get { return m_toggle.isOn; }
			set { m_toggle.isOn = value; }
		}

		void Update()
		{
			if (m_isSynchro)
			{
				TimeSpan l_timeSpan = TimeSpan.FromSeconds(m_chrono.TimeElapsed);
				m_chronoLabel.text = l_timeSpan.ToString(@"mm\:ss");
			}
		}
		
		public void OnStepComplete(XdeAsbStep p_step)
		{
			if (!IsDone)
			{
				m_stepResults = GetStepPictoResult(p_step);
				UpdateBonusMalusPicto();
			}
		}

		private void UpdateBonusMalusPicto()
		{
			m_isSynchro = false;
			m_toggle.isOn = true;
			this.GetComponent<Image>().color = new Color(33f/255f, 33f/255f, 33f/255f);
			this.transform.Find("IconArea").gameObject.SetActive(true);

			foreach (Texture2D l_texture2D in m_stepResults)
			{
				if (l_texture2D != null)
				{
					GameObject l_img = new GameObject("image");
					l_img.transform.SetParent(this.transform.Find("IconArea"));
					l_img.transform.localPosition = Vector3.zero;
					l_img.transform.localRotation = Quaternion.identity;
					l_img.transform.localScale = Vector3.one;
					l_img.AddComponent<RawImage>().texture = l_texture2D;
					l_img.GetComponent<RectTransform>()
						.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, l_texture2D.width);
					l_img.GetComponent<RectTransform>()
						.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, l_texture2D.height);
				}
			}

			Transform parent = this.transform.parent;
			Transform rect = this.transform.parent.parent.parent;
			float sizeparent = parent.GetComponent<RectTransform>().sizeDelta.y;
			float sizerect = rect.GetComponent<RectTransform>().sizeDelta.y;
			
			if (sizeparent > sizerect &&
			    parent.transform.localPosition.y < sizeparent - sizerect &&
			    this.transform.GetSiblingIndex() > 0)
			{
				parent.localPosition = new Vector3(parent.localPosition.x, parent.localPosition.y + 114, parent.localPosition.z);
			}
			
			StartCoroutine(WaitAndHideIcon());
		}

		IEnumerator WaitAndHideIcon()
		{
			yield return new WaitForSeconds(15f);
			this.transform.Find("IconArea").gameObject.SetActive(false);
		}
	}
}


