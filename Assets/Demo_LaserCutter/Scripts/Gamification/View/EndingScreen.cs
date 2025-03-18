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
using UnityEngine.UI;

namespace Gamification.View
{
	public class EndingScreen : MonoBehaviour
	{
		[Header("UI - Results")]
		[SerializeField]
		private  GameObject m_resultPanel = null;
		[SerializeField]
		private  Text m_score = null;

		[Header("UI - Reference")]
		[SerializeField]
		private Transform m_criteriaList = null;
		[SerializeField]
		private Transform m_stepList = null;
		[SerializeField]
		private RawImage m_goal1Done = null;
		[SerializeField]
		private RawImage m_goal2Done = null;
		[SerializeField]
		private Text m_name = null;

		[Header("UI - Prefab")]
		[SerializeField]
		private GameObject m_prefabCriteraEnd = null;

		[SerializeField]
		private GameObject m_prefabStepCriteria = null;
		[SerializeField]
		private Texture2D m_textureAutonomy = null;
		[SerializeField]
		private Texture2D m_textureEfficiency = null;
		[SerializeField]
		private Texture2D m_textureSafety = null;
		[SerializeField]
		private Texture2D m_goalOk = null;

		[SerializeField]
		private Texture2D m_bonusAutonomy = null;
		[SerializeField]
		private Texture2D m_malusAutonomy = null;
		[SerializeField]
		private Texture2D m_bonusEfficiency = null;
		[SerializeField]
		private Texture2D m_malusEfficiency = null;
		[SerializeField]
		private Texture2D m_bonusSafety = null;
		[SerializeField]
		private Texture2D m_malusSafety = null;

		public void Init()
		{
			m_resultPanel.SetActive(false);
			// delete previous list of criteria to avoid duplicates
			if (m_criteriaList != null)
			{
				for (int i = m_criteriaList.childCount - 1; i >= 0; i--)
				{
					Destroy(m_criteriaList.GetChild(i).gameObject);
				}
			}
			// delete previous list of steps done to avoid duplicates
			if (m_stepList != null)
			{
				for (int i = m_stepList.childCount - 1; i >= 0; i--)
				{
					Destroy(m_stepList.GetChild(i).gameObject);
				}
			}
		}
		
		public void DisplayResultPanel(Evaluation p_evaluation, Chronometer p_chronometer = null)
		{
			if (p_evaluation == null)
				throw new Exception("Evaluation arg is empty.");

			// display score
			m_score.text = p_evaluation.Score.ToString("0.0")+"%";
			
			// Evaluate Goal
			if (p_evaluation.Score > 70f)
				m_goal1Done.texture = m_goalOk;

			int l_countGoal2 = 0;
			int l_count = 0;
			IOrderedEnumerable<KeyValuePair<Metric, double>> l_metricResults = p_evaluation.MetricsScore.OrderByDescending((x) => x.Key.Coefficient);
			foreach (KeyValuePair<Metric, double> l_metricResult in l_metricResults)
			{
				if (l_metricResult.Value > 90f)
					l_countGoal2 += 1;
				GameObject l_go = Instantiate(m_prefabCriteraEnd, m_criteriaList);
				l_go.transform.Find("Ref/Name").GetComponent<Text>().text = l_metricResult.Key.GetName();
				l_go.transform.Find("Ref/Index").GetComponent<Text>().text = (l_count + 1).ToString();
				l_go.transform.Find("Score").GetComponent<Text>().text = l_metricResult.Value.ToString("0.0") + "%";
				SetTextureAndSize(l_metricResult.Key, l_go.transform.Find("Image").GetComponent<RawImage>());
				if (l_metricResult.Key.GetType() == typeof(EfficiencyMetric) && p_chronometer != null)
				{
					TimeSpan l_timeSpan = TimeSpan.FromSeconds(p_chronometer.TimeElapsed);
					string l_time = $"{l_timeSpan.Minutes:D2}:{l_timeSpan.Seconds:D2}";
					l_go.transform.Find("Time/Value").GetComponent<Text>().text = l_time;
					l_go.transform.Find("Time").gameObject.SetActive(true);
				}

				l_count++;
			}
			if (l_countGoal2 == l_metricResults.Count())
				m_goal2Done.texture = m_goalOk;
			
			// Display Report
			foreach (StepResults l_stepReport in p_evaluation.StepsResults)
			{
				GameObject l_go = Instantiate(m_prefabStepCriteria, m_stepList);
				
				// Set the name && resize height
				Transform l_name = l_go.transform.Find("Description/Sub/Name");
				// if not chronometed step, disable time and enlarge Message or set the time
				if (l_stepReport.Chronometer != null)
				{
					TimeSpan l_timeSpan = TimeSpan.FromSeconds(l_stepReport.Chronometer.TimeElapsed);
					string l_time = $"{l_timeSpan.Minutes:D2}:{l_timeSpan.Seconds:D2}";
					l_go.transform.Find("Description/Sub/Time").GetComponent<Text>().text = l_time;
				}
				else
				{
					l_go.transform.Find("Description/Sub/Time").gameObject.SetActive(false);
					l_name.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 510);				
				}

				l_name.GetComponent<Text>().text = l_stepReport.Step.name;
				l_name.GetComponent<RectTransform>()
				      .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, l_name.GetComponent<Text>().preferredHeight);
				l_go.transform.Find("Description/Sub").GetComponent<RectTransform>()
				    .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, l_name.GetComponent<Text>().preferredHeight);
				// Set the Comment && resize height

				Transform l_bonus = l_go.transform.Find("Description/BonusMalus");
				Texture2D l_texture2D = null;
				foreach (KeyValuePair<Type, bool> l_result in l_stepReport.MetricsResult)
				{
					l_texture2D = GetBonusMalus(l_result.Key, l_result.Value);

					if (l_texture2D != null)
					{
						GameObject l_img = new GameObject("image");
						l_img.transform.SetParent(l_bonus);
						l_img.transform.localPosition = Vector3.zero;
						l_img.transform.localRotation = Quaternion.identity;
						l_img.transform.localScale = Vector3.one;
						l_img.AddComponent<RawImage>().texture = l_texture2D;
						l_img.GetComponent<RectTransform>()
						     .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, l_texture2D.width);
						l_img.GetComponent<RectTransform>()
						     .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, l_texture2D.height);
					}
				}
				
				if (l_bonus.childCount == 0)
					l_bonus.gameObject.SetActive(false);


				bool l_isNotLast = !p_evaluation.StepsResults.Last().Equals(l_stepReport);
				l_go.transform.Find("Description/Bar").gameObject.SetActive(l_isNotLast);

				Canvas.ForceUpdateCanvases();

				float l_sizeName = l_name.GetComponent<Text>().preferredHeight;
				// sizeY = sizenameY + sizeBonusmalusY + sizeBarY + offset
				float l_val = l_sizeName + (l_isNotLast ? 17 : 0) + (l_bonus.childCount > 0 ? 69 : 0);
				l_go.transform.Find("Description").GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, l_val);
				l_go.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, l_val);

			}
			Canvas.ForceUpdateCanvases();
			m_resultPanel.SetActive(true);
		}

		public Texture2D GetBonusMalus(Type p_metricType, bool p_isBonus)
		{
			if (p_metricType == typeof(AutonomyMetric))
				return (p_isBonus ? m_bonusAutonomy : m_malusAutonomy);
			else if (p_metricType == typeof(EfficiencyMetric))
				return (p_isBonus ? m_bonusEfficiency : m_malusEfficiency);
			else if (p_metricType == typeof(SafetyMetric))
				return (p_isBonus ? m_bonusSafety : m_malusSafety);
			else
				return null;
		}
		
		private void SetTextureAndSize(Metric p_metric, RawImage p_image)
		{
			Texture2D l_texture;
			if (p_metric.GetType() == typeof(AutonomyMetric))
				l_texture = m_textureAutonomy;
			else if (p_metric.GetType() == typeof(EfficiencyMetric))
				l_texture = m_textureEfficiency;
			else if (p_metric.GetType() == typeof(SafetyMetric))
				l_texture = m_textureSafety;
			else
				l_texture = m_textureAutonomy;
			p_image.texture = l_texture;
			p_image.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, l_texture.width);
			p_image.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, l_texture.height);
		}

		public void ChangeName(string p_name)
		{
			m_name.text = p_name;
		}

		private int m_steplistindex = 0;
		private List<float> m_stepListOffset;

		private void InitStepListOffset()
		{
			m_stepListOffset = new List<float>();
			float max = m_stepList.GetComponent<RectTransform>().sizeDelta.y;
			float val = m_stepList.parent.parent.GetComponent<RectTransform>().sizeDelta.y;
			foreach (Transform l_transform in m_stepList.transform)
			{
				if (val + l_transform.GetComponent<RectTransform>().sizeDelta.y + 15 < max)
				{
					val += l_transform.GetComponent<RectTransform>().sizeDelta.y;
					m_stepListOffset.Add(l_transform.GetComponent<RectTransform>().sizeDelta.y + 15);
				}
				else
				{
					m_stepListOffset.Add((val + l_transform.GetComponent<RectTransform>().sizeDelta.y) - max + 15);
					break;
				}
			}
		}
		
		public void UpStepListButton()
		{
			if (m_stepListOffset == null)
				InitStepListOffset();
			if (m_steplistindex > 0)
			{
				m_steplistindex -= 1;
				m_stepList.transform.localPosition += new Vector3(0, -m_stepListOffset[m_steplistindex], 0);
			}
		}

		public void DownStepListButton()
		{
			if (m_stepListOffset == null)
				InitStepListOffset();
			if (m_steplistindex < m_stepListOffset.Count)
			{
				m_stepList.transform.localPosition += new Vector3(0, m_stepListOffset[m_steplistindex], 0);
				m_steplistindex += 1;
			}
		}
		
		public void ExitVR()
		{
			Application.Quit();
		}
	}
}


