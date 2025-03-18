//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

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
		[Tooltip("0 = easy, 1 = medium, 2 = hard")]
		private int m_activityLevel = 1;

		[SerializeField]
		[Tooltip("0 = novice, 1 = confirmed, 2 = expert")]
		private int m_userLevel = 1;

		[SerializeField]
		private HelpController m_helpController;

		[SerializeField]
		private XdeAsbScenario m_mainScenario;
		
		private RedisManager m_redisManager;

		private int m_currentActivity = 0;
		private bool m_activityFlag = false;
		private bool m_firstActivityFlag = true;
		
		private bool m_mustProcessNewActivityLevel = false;
		

		private void Awake()
		{
			m_mainScenario.completedEvent.AddListener(OnMainScenarioComplete);
			m_redisManager = new RedisManager();
			if (!Application.isEditor)
			{
				m_redisManager.SetConnectionData(m_redisServerIp, m_redisServerPort);
			}
			m_redisManager.ConnectRedis();
			m_redisManager.NewNextActivityData += OnNewNextActivityData;
			
		}

		private void OnMainScenarioComplete(XdeAsbStep p_arg0)
		{
			m_firstActivityFlag = true;
		}

		private void OnDestroy()
		{
			m_mainScenario.completedEvent.RemoveListener(OnMainScenarioComplete);

			if (m_currentActivity > 0)
			{
				m_redisManager.StopActivity(m_currentActivity);
			}
			m_redisManager.NewNextActivityData -= OnNewNextActivityData;
			m_redisManager.DisconnectRedis();
		}

		private void Update()
		{
			if (m_mustProcessNewActivityLevel)
			{
				SetupActivityLevel();
				m_mustProcessNewActivityLevel = false;
			}
		}

		private void OnNewNextActivityData(RedisManager.NextActivityData p_obj)
		{

			Debug.Log("New Next Activity Data " + p_obj.next_activity_level);
			m_activityLevel = p_obj.next_activity_level;
			
			if(m_activityFlag)
				m_redisManager.StartActivity(m_currentActivity, m_activityLevel, m_userLevel);

			m_mustProcessNewActivityLevel = true;
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
					// m_helpController.DisplayTodoList(true);
					break;
				case 2:
					// m_helpController.DisplayTodoList(false);
					break;
				default:
					break;
			}
		}
		
		public void OnStepActivated(XdeAsbStep p_step)
		{
			m_currentActivity = p_step.GetInstanceID();
			Debug.Log($"OnStepActivated: {m_currentActivity} {p_step.name}");
			m_activityFlag = true;
			if (m_firstActivityFlag)
			{
				m_firstActivityFlag = false;
				m_redisManager.StartActivity(m_currentActivity, m_activityLevel, m_userLevel);
				m_activityFlag = false;
				SetupActivityLevel();
			}
		}

		public void OnStepDeactivated(XdeAsbStep p_step)
		{
			Debug.Log($"OnStepDeactivated: {p_step.GetInstanceID()} {p_step.name}");

			if (m_currentActivity == p_step.GetInstanceID())
			{
				m_redisManager.StopActivity(p_step.GetInstanceID());
			}
		}

		public void SetStartupAppLevel(int p_userLevel)
		{
			Debug.Log($"SetStartupAppLevel: {p_userLevel}");
			m_userLevel = p_userLevel;
			m_activityLevel = p_userLevel;
		}

	
	}
}