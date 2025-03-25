//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================


using UnityEngine;
using Gamification.Help;

namespace PersonalizationTool
{
	public class DifficultyManager : MonoBehaviour
	{
		[SerializeField]
		private PersonalizationManager m_personalizationManager;

		[SerializeField]
		private GameObject m_visualGuidesButton;

		[SerializeField]
		private GameObject m_todoListButton;

		private void OnEnable()
		{
			m_personalizationManager.DifficultyChanged += OnDifficultyChanged;
		}

		private void OnDisable()
		{
			m_personalizationManager.DifficultyChanged -= OnDifficultyChanged;
		}

		private void SetGameAsHard()
		{
			m_todoListButton.SetActive(false);
			m_visualGuidesButton.SetActive(false);
		}

		private void SetGameAsMedium()
		{
			m_todoListButton.SetActive(true);
			m_visualGuidesButton.SetActive(false);
		}

		private void SetGameAsEasy()
		{
			m_todoListButton.SetActive(true);
			m_visualGuidesButton.SetActive(true);
		}

		private void OnDifficultyChanged(int p_activeDifficulty)
		{
			switch (p_activeDifficulty)
			{
				case 0:
					SetGameAsEasy();
					break;
				case 1:
					SetGameAsMedium();
					break;
				case 2:
					SetGameAsHard();
					break;
			}
		}

	}
}