using Gamification.View;
using UnityEngine;

public class StartConfirmationPopup : MonoBehaviour
{
	[SerializeField]
	private GameObject m_popup;

	[SerializeField]
	private StartupScreen m_startupScreen;
	
	public void OnStartButtonClick()
	{
		m_popup.SetActive(false);
		m_startupScreen.StartScene();
	}
}