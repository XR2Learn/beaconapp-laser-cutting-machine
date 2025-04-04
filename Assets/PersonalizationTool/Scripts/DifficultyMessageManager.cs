//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System.Collections;
using UnityEngine;
using TMPro;
using XR2Learn.Common;

namespace PersonalizationTool
{
	public class DifficultyMessageManager : MonoBehaviour
	{
		[SerializeField]
		private PersonalizationManager m_personalizationManager;

		[SerializeField]
		private GameObject m_popup;
		
		[SerializeField]
		private TMP_Text m_difficultyText;
		

		[SerializeField]
		private string m_easyText;

		[SerializeField]
		private string m_mediumText;

		[SerializeField]
		private string m_hardText;

		private int m_currentActivityLevel;

		private void Awake()
		{
			m_currentActivityLevel = -1;
			m_popup.SetActive(false);
		}
		private void OnEnable()
		{
			m_personalizationManager.DifficultyChanged += OnDifficultyChanged;
		}

		private void OnDisable()
		{
			m_personalizationManager.DifficultyChanged -= OnDifficultyChanged;
		}

		private void OnDifficultyChanged(int p_difficultyLevel)
		{
			if (m_currentActivityLevel == p_difficultyLevel)
				return;

			m_currentActivityLevel = p_difficultyLevel;

			switch (p_difficultyLevel)
			{
				case 0:
					m_difficultyText.text = m_easyText;
					break;
				case 1:
					m_difficultyText.text = m_mediumText;
					break;
				case 2:
					m_difficultyText.text = m_hardText;
					break;
				default:
					m_difficultyText.text = m_hardText;
					break;
			}

			SoundManager.PlaySound("ActivityLevelChanged");
			m_popup.SetActive(true);
		}

	}
}