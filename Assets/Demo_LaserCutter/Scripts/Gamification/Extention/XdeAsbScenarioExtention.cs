//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System.Collections.Generic;
using XdeEngine.Assembly;

namespace Gamification.Extention
{
	public static class XdeAsbScenarioExtention
	{
		public static List<XdeAsbStep> GetActiveSteps(this XdeAsbScenario p_scenario , bool p_includingScenario = false)
		{
			List<XdeAsbStep> l_steps = new List<XdeAsbStep>();
			int l_nbSteps = p_scenario.ActiveStepsCount;

			XdeAsbStep l_step;
			for (int i = 0; i < l_nbSteps; i++)
			{
				l_step = p_scenario.GetActiveStep(i);
				if (!l_step.IsCompleted)
				{
					l_steps.Add(l_step);
				}
			}

			if (p_includingScenario && p_scenario.IsActive)
			{
				l_steps.Add(p_scenario);
			}
			
			return l_steps;
		}
		
		public static List<XdeAsbStep> GetActiveSteps(this List<XdeAsbScenario> p_scenarios, bool p_includingScenario = false)
		{
			List<XdeAsbStep> l_steps = new List<XdeAsbStep>();
			foreach (XdeAsbScenario l_scenario in p_scenarios)
			{
				l_steps.AddRange(GetActiveSteps(l_scenario,p_includingScenario));
			}
			return l_steps;
		}
		
		public static void Activate(this List<XdeAsbScenario> p_scenarios, bool p_activation)
		{
			foreach (XdeAsbScenario l_scenario in p_scenarios)
			{
				l_scenario.gameObject.SetActive(p_activation);
			}
		}
		
		public static List<XdeAsbStep> GetSteps(this List<XdeAsbScenario> p_scenarios)
		{
			List<XdeAsbStep> l_steps = new List<XdeAsbStep>();
			foreach (XdeAsbScenario l_scenario in p_scenarios)
			{
				l_steps.AddRange(l_scenario.steps);
			}
			return l_steps;
		}
		
		public static void Hide(this List<XdeAsbVisualGuide> p_visualGuides, XdeAsbStep step)
		{
			foreach (XdeAsbVisualGuide visualGuide in p_visualGuides)
			{
				visualGuide.Clear(step);
			}
		}
		
		public static void Show(this List<XdeAsbVisualGuide> p_visualGuides, XdeAsbStep p_step)
		{
			foreach (XdeAsbVisualGuide visualGuide in p_visualGuides)
			{
				visualGuide.UpdateVisualGuide(p_step);
			}
		}
	}
}
