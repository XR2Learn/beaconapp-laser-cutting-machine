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
		private HelpController m_helpController;
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
			m_helpController.ToggleNextButton(false);
			m_helpController.DisplayToDoListContent(false);
			m_helpController.HideVisualGuidelines();
		}

		private void SetGameAsMedium()
		{
			m_helpController.ToggleNextButton(false);
			m_helpController.OnClickTodoList();
			m_helpController.DisplayToDoListContent(true);
			m_helpController.HideVisualGuidelines();
		}

		private void SetGameAsEasy()
		{
			m_helpController.ToggleNextButton(true);
			m_helpController.OnClickVisualGuides();
			m_helpController.OnClickTodoList();
			m_helpController.DisplayToDoListContent(true);
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