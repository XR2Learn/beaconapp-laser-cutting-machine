//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Gamification.Evaluations.Metrics;
using UnityEngine;
using VMachina;
using XdeEngine.Core;

namespace Gamification.Evaluations
{
	public class CollisionTracker : StepEvaluator<SafetyMetric>
	{
		private Action m_collideDangerousObjectAction;
		[SerializeField]
		private List<DangerousObject> m_dangerousObjects = null;
		private XdeCollisionUVGetter m_player;
		private float m_score;
		private string m_logs;

		public List<DangerousObject> DangerousObjects
		{
			get { return m_dangerousObjects; }
		}
		
		public Action OnCollideDangerousObject
		{
			get { return m_collideDangerousObjectAction; }
			set { m_collideDangerousObjectAction = value; }
		}
		
		protected override void OnEnable()
		{
			base.OnEnable();

			m_logs = string.Empty;
			m_score = PointValue;
			PointValue = m_dangerousObjects?.Sum(x => x.Penality) ?? 0;
			
			// TODO : s'abonner à une interface plutot 
			m_player = FindObjectOfType<XdeCollisionUVGetter>() ;
			if (m_player == null)
			{
				throw new ArgumentNullException(nameof(m_player));
			}
			m_player.OnStartCollision += OnCollideObject;
		}
		
		protected override void OnDisable()
		{
			base.OnDisable();
			m_player.OnStartCollision -= OnCollideObject;
		}
		
		public void OnCollideObject(XdeRigidBody p_body ,XdeRigidBody p_object)
		{
			if (!EvaluatedStep.IsCompleted)
			{
				DangerousObject dangerousObject = this.m_dangerousObjects.FirstOrDefault(x => 
					x.Target == p_object);

				if (dangerousObject != null)
				{
					m_score -= dangerousObject.Penality;
					m_logs += "Penality - collision with "+p_object.name+"\n";
					if (OnCollideDangerousObject != null)
					{
						OnCollideDangerousObject.Invoke();
					}
				}
			}
		}

		public override int BonusMalusLogic
		{
			get
			{
				return (int)BonusMalus.Malus;
			}
		}

		public override string GetLogs(bool p_displayPoints = false)
		{
			if (string.IsNullOrEmpty(m_logs))
			{
				return "0 collision";
			}
			
			return m_logs;
		}

		public override float GetScore()
		{
			if (EvaluatedStep.IsCompleted)
			{
				return Math.Max(0, this.m_score);
			}

			return 0;
		}
	}
}