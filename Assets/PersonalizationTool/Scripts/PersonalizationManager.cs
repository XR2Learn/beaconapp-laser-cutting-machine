//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using System.Threading.Tasks;
using Gamification.Help;
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
		//[Tooltip("0 = easy, 1 = medium, 2 = hard")]
		private int m_activityLevel = 1;

		[SerializeField]
		//[Tooltip("0 = novice, 1 = medium, 2 = expert")]
		private int m_userLevel = 1;

		[SerializeField]
		private HelpController m_helpController;

		// private RedisManager m_redisManager;

		private void Awake()
		{
			m_helpController.OnNextStepAction += OnNextStepAction;
			// m_redisManager = new RedisManager();
			// if (!Application.isEditor)
			// {
			// 	m_redisManager.SetConnectionData(m_redisServerIp, m_redisServerPort);
			// }
			// m_redisManager.NewNextActivityData += OnNewNextActivityData;
		}

		private void OnDestroy()
		{
			// m_redisManager.NewNextActivityData -= OnNewNextActivityData;
		}

		

		private void OnNewNextActivityData(RedisManager.NextActivityData p_obj)
		{
			m_activityLevel = p_obj.next_activity_level;
		
			SetupActivityLevel();
		}

		private void SetupActivityLevel()
		{
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
			// Redis send a lot of user frustrated
		}
		public void OnStepActivated(XdeAsbStep p_step)
		{
			// m_redisManager.StartActivity(p_step.name, 0, m_userLevel);
		}

		public void OnStepDeactivated(XdeAsbStep p_step)
		{
			// m_redisManager.StopActivity(p_step.name);
		}

		public void SetUserLevel(int p_userLevel)
		{
			m_userLevel = p_userLevel;
		}

	
	}
}