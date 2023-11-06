//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using UnityEngine;
using XdeEngine.Core;

namespace Gamification.Evaluations
{
	[System.Serializable]
	public class DangerousObject
	{
		[SerializeField]
		private XdeRigidBody m_target;

		public XdeRigidBody Target
		{
			get => m_target;
			set => m_target = value;
		}
		[SerializeField]
		private int m_penality;

		public int Penality
		{
			get => m_penality;
			set => m_penality = value;
		}

		public DangerousObject()
		{
			m_penality = 1;
			m_target = null;
		}
	}
}

