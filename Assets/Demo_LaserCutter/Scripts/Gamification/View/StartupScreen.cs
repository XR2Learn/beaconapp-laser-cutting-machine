//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Gamification.Evaluations;
using Gamification.Evaluations.Metrics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XdeEngine.Core;

namespace Gamification.View
{
	public class StartupScreen : MonoBehaviour
	{
		[Header("UI - Welcome")]
		[SerializeField]
		private  GameObject m_welcomePanel = null;
		public Action StartScenarioAction;

		[Header("Reference")]
		private XdeScene m_scene;
		[SerializeField]
		private Text m_name = null;
		[SerializeField]
		private Transform m_criteriaList = null;

		[Header("Prefab")]
		[SerializeField]
		private GameObject m_prefabCritera = null;
		[SerializeField]
		private Texture2D m_textureIndependency = null;
		[SerializeField]
		private Texture2D m_textureEfficiency = null;
		[SerializeField]
		private Texture2D m_textureSafety = null;

		[Header("Events")]
		[SerializeField]
		private UnityEvent _onStartScene;

		private IEnumerable<Metric> m_metrics;
		private GameObject m_loader;
		//private VMachina.PlayerManager playerManager;
		private XdeNetwork.XdeRPCApi rpcApi;

		private void InitMetrics(Chronometer p_chronometer)
		{
			List<Metric> l_orderMetrics = m_metrics.OrderByDescending(p_metric => p_metric.Coefficient).ToList();
			for (int i = 0; i < l_orderMetrics.Count; i++)
			{
				GameObject l_go = Instantiate(m_prefabCritera, m_criteriaList);
				l_go.transform.Find("Ref/Name").GetComponent<Text>().text = l_orderMetrics[i].GetName();
				l_go.transform.Find("Ref/Index").GetComponent<Text>().text = (i + 1).ToString();
				SetTextureAndSize(l_orderMetrics[i], l_go.transform.Find("Image").GetComponent<RawImage>());
				if (l_orderMetrics[i].GetType() == typeof(EfficiencyMetric) && p_chronometer != null)
				{
					TimeSpan l_timeSpan = TimeSpan.FromSeconds(p_chronometer.EstimatedDuration);
					string l_time = $"{l_timeSpan.Minutes:D2}:{l_timeSpan.Seconds:D2}";
					l_go.transform.Find("Time/Value").GetComponent<Text>().text = l_time;
					l_go.transform.Find("Time").gameObject.SetActive(true);
				}
			}
		}

		private void SetTextureAndSize(Metric p_metric, RawImage p_image)
		{
			Texture2D l_texture;
			if (p_metric.GetType() == typeof(AutonomyMetric))
				l_texture = m_textureIndependency;
			else if (p_metric.GetType() == typeof(EfficiencyMetric))
				l_texture = m_textureEfficiency;
			else if (p_metric.GetType() == typeof(SafetyMetric))
					l_texture = m_textureSafety;
			else
				l_texture = m_textureIndependency;
			p_image.texture = l_texture;
			p_image.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, l_texture.width);
			p_image.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, l_texture.height);
		}

		
		public void ChangeLoading(GameObject p_loader)
		{
			m_loader = p_loader;
		}

		public void StartScene()
		{
			m_welcomePanel.SetActive(false);
			if (StartScenarioAction != null)
				StartScenarioAction.Invoke();
		}

		public void ChangeName(string p_name)
		{
			m_name.text = p_name;
		}
		
		public void Init(IEnumerable<Metric> p_metrics, Chronometer p_chronometer = null)
		{
			if (p_metrics == null)
			{
				throw new Exception("Null metrics given to UI_StartupScreen");
			}

			m_scene = FindObjectOfType<XdeScene>();
			m_metrics = p_metrics;
			InitMetrics(p_chronometer);
			m_welcomePanel.SetActive(true);
		}

		public void DownButtonDesc()
		{
			m_welcomePanel.transform.Find("RightPanel/Description/Scroll View/Viewport/Content").localPosition += new Vector3(0, -100, 0);
		}

		public void UpButtonDesc()
		{
			m_welcomePanel.transform.Find("RightPanel/Description/Scroll View/Viewport/Content").localPosition += new Vector3(0, 100, 0);
		}
		
		public void ExitVR()
		{
			Application.Quit();
		}

		private void Start()
		{
			//playerManager = FindObjectOfType<VMachina.PlayerManager>();
		}
  }
}

