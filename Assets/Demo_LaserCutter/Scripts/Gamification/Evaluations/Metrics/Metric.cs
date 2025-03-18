//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using UnityEngine;

namespace Gamification.Evaluations.Metrics
{
	public abstract class Metric : MonoBehaviour
	{
		[Tooltip("Coefficient value between 0 and 1.")]
		[SerializeField]
		protected int m_coefficient = 1;
	
		public int Coefficient
		{
			get { return m_coefficient; }
		}

		public abstract string GetName();
	}
}

