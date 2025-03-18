//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using System.Threading.Tasks;
using Gamification.Help;
using Gamification.View;
using PersonalizationTool.Redis;
using UnityEngine;
using XdeEngine.Assembly;

namespace PersonalizationTool.Scripts
{
	public class PersonalizationManager : MonoBehaviour
	{
		
		[SerializeField]
		private string m_redisServerIp;
		[SerializeField]
		private string m_redisServerPort;
		
		[SerializeField]
		[Tooltip("0 = easy, 1 = medium, 2 = hard")]
		private int m_activityLevel = 1;

		[SerializeField]
		[Tooltip("0 = novice, 1 = confirmed, 2 = expert")]
		private int m_userLevel = 1;

		[SerializeField]
		private HelpController m_helpController;

		[SerializeField]
		private StartupScreen m_startupScreen;
		
		[SerializeField]
		private XdeAsbScenario m_xdeAsbScenario;
		private RedisManager m_redisManager;

		private int m_currentActivity = 0;

		private void Awake()
		{
			m_helpController.OnNextStepAction += OnNextStepAction;
			m_startupScreen.StartScenarioAction += SetupActivityLevel;
			
			m_redisManager = new RedisManager();
			if (!Application.isEditor)
			{
				m_redisManager.SetConnectionData(m_redisServerIp, m_redisServerPort);
			}
			m_redisManager.ConnectRedis();
			m_redisManager.NewNextActivityData += OnNewNextActivityData;
			
		}

		private void OnDestroy()
		{
			m_helpController.OnNextStepAction -= OnNextStepAction;
			m_startupScreen.StartScenarioAction -= SetupActivityLevel;


			if (m_currentActivity > 0)
			{
				m_redisManager.StopActivity(m_currentActivity);
			}
			m_redisManager.NewNextActivityData -= OnNewNextActivityData;
			m_redisManager.DisconnectRedis();
		}

		private void OnNewNextActivityData(RedisManager.NextActivityData p_obj)
		{
			m_activityLevel = p_obj.next_activity_level;
		
			SetupActivityLevel();
		}

		private void SetupActivityLevel()
		{
			Debug.Log($"SetActvityLevel: {m_activityLevel}");
			switch (m_activityLevel)
			{
				case 0:
					m_helpController.OnClickVisualGuides();
					m_helpController.OnClickTodoList();
					break;
				case 1:
					m_helpController.OnClickTodoList();
					break;
				case 2:
					break;
				default:
					break;
			}
		}

		private void OnNextStepAction()
		{
			OnStepDeactivated();
			m_currentActivity = m_xdeAsbScenario.GetActiveStep(0).GetInstanceID();
			Debug.Log($"SetActivityLevel: {m_currentActivity}");
			m_activityLevel -= 1;
			m_redisManager.StartActivity(m_currentActivity, m_activityLevel, m_userLevel);
		}
		public void OnStepActivated(XdeAsbStep p_step)
		{
			Debug.Log($"OnStepActivated: {p_step.GetInstanceID()}");
			if (m_currentActivity != 0)
			{
				m_redisManager.StopActivity(m_currentActivity);
			}
			m_currentActivity = p_step.GetInstanceID();
			m_redisManager.StartActivity(m_currentActivity, m_activityLevel, m_userLevel);
		}

		public void OnStepDeactivated()
		{
			Debug.Log($"OnStepDeactivated: {m_currentActivity}");
			m_redisManager.StopActivity(m_currentActivity);
			m_currentActivity = 0;
		}

		public void SetStartupAppLevel(int p_userLevel)
		{
			Debug.Log($"SetStartupAppLevel: {p_userLevel}");
			m_userLevel = p_userLevel;
			m_activityLevel = p_userLevel;
		}

	
	}
}