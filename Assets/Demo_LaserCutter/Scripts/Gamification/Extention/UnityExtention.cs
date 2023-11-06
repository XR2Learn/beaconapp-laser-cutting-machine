//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gamification.Extention
{
	public static class UnityExtention
	{
		public static C GetComponentInChildrenFDS<C>(this Component p_component) where C:Component
		{
			List<C> l_components = p_component.GetComponentsInChildren<C>().ToList();
			return l_components.FirstOrDefault(x=>x.transform.parent == p_component.transform);
		}
	}
}