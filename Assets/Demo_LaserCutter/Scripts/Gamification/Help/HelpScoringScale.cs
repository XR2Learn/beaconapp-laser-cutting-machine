//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamification.Help
{
	public class HelpScoringScale : MonoBehaviour
	{
		[Header("Cost coefficent of help by AutonomyStep ")]
		[Range(0, 1)]
		[SerializeField]
		private float m_todoListCoef = 0.5f;

		[Range(0, 1)]
		[SerializeField]
		private float m_visualGuidesCoef= 0.5f;
	
		public float TodoListCoef
		{
			get { return m_todoListCoef; }
		}
	
		public float VisualGuidesCoef
		{
			get { return m_todoListCoef; }
		}
	
		public float CoefSum
		{
			get { return m_todoListCoef+m_visualGuidesCoef; }
		}
	}
}
