//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System.Collections;
using UnityEngine;
using TMPro;

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
		private int m_durationOfDisplay;

		[SerializeField]
		private string m_easyText;

		[SerializeField]
		private string m_mediumText;

		[SerializeField]
		private string m_hardText;

		
		private Coroutine m_displayCoroutine;

		private void Awake()
		{
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

			m_popup.SetActive(true);

			if(m_displayCoroutine != null)
				StopCoroutine(m_displayCoroutine);

			m_displayCoroutine = StartCoroutine(HideTextAfterTime());
		}

		private IEnumerator HideTextAfterTime()
		{
			yield return new WaitForSeconds(m_durationOfDisplay);
			m_popup.SetActive(false);
		}
	}
}