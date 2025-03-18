//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System;
using XdeEngine.Assembly;

namespace Gamification.Evaluations
{
	public interface IStepEvaluator
	{
		XdeAsbStep EvaluatedStep { get;}
		Type MetricType { get; }
		int PointValue { get; }
		float GetScore();
		int BonusMalusLogic { get; }
		float BonusMalusPoints { get; }
		string GetLogs(bool p_displayPoints = false);
	}
}



