//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using System.Collections;
using UnityEngine;
using PersonalizationTool.Redis;
using XdeEngine.Assembly;

namespace PersonalizationTool
{
	public class PersonalizationManager : MonoBehaviour
	{
		public event Action<int> DifficultyChanged;
		
		[SerializeField]
		private int m_intervalInSeconds;

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
		private XdeAsbScenario m_mainScenario;

		private RedisManager m_redisManager;

		private int m_currentActivity = 0;

		private bool m_mustProcessNewActivityLevel = false;
		
		private DateTime m_lastActivityTime;
		private bool m_initialized;

		private int m_currentStepInstance = 0;

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
			m_redisManager.StopActivity(m_currentActivity);
			m_currentActivity = 0;
		}

		private void OnDestroy()
		{
			m_mainScenario.completedEvent.RemoveListener(OnMainScenarioComplete);


			m_redisManager.StopActivity(m_currentActivity);

			m_redisManager.NewNextActivityData -= OnNewNextActivityData;
			m_redisManager.DisconnectRedis();
		}

		private void Update()
		{
			if(!m_initialized) return;
			if (m_lastActivityTime < DateTime.Now.AddSeconds(-m_intervalInSeconds))
			{
				RedisStopActivity();
			}
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

			m_redisManager.StartActivity(m_currentActivity, m_activityLevel, m_userLevel);

			m_mustProcessNewActivityLevel = true;
		}

		[ContextMenu("Display Hints")]
		private void SetupActivityLevel()
		{
			Debug.Log($"SetActvityLevel: {m_activityLevel}");

			DifficultyChanged?.Invoke(m_activityLevel);
		}

		
		private void RedisStopActivity()
		{
			m_lastActivityTime = DateTime.Now;
			m_redisManager.StopActivity(m_currentActivity);
			m_currentActivity++;
		}
		
		public void OnStepActivated(XdeAsbStep p_step)
		{
			m_currentStepInstance = p_step.GetInstanceID();
			if (m_currentActivity == 0)
			{
				StartCoroutine(SetupActivityLevelCoroutine());
			}
		}

		public void OnStepDeactivated(XdeAsbStep p_step)
		{
			if (m_currentStepInstance == p_step.GetInstanceID())
			{
				Debug.Log("Step Deactivated");
				RedisStopActivity();
			}
		}

		private IEnumerator SetupActivityLevelCoroutine()
		{
			yield return null;
			SetupActivityLevel();
		}

		public void SetStartupAppLevel(int p_userLevel)
		{
			Debug.Log($"SetStartupAppLevel: {p_userLevel}");
			m_userLevel = p_userLevel;
			m_activityLevel = p_userLevel;
			m_lastActivityTime = DateTime.Now;
			m_redisManager.StartActivity(m_currentActivity, m_activityLevel, m_userLevel);
			m_initialized = true;
		}
	}
}