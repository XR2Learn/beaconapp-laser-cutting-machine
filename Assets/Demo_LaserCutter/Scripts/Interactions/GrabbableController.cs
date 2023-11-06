//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by CEA
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XdeEngine.Assembly;
using System.Linq;
using XdeEngine.Core;
using XdeEngine.Core.Monitoring;

public class GrabbableController : MonoBehaviour
{
    private XdeAsbOperatorGraspManipulator[] _handsOperators;

    public List<XdeAsbPart> notGrabableAtStartParts;

    public XdeAsbOperatorGraspManipulator[] HandsOperators
    {
        get
        {
            if (_handsOperators == null || _handsOperators.Count() ==0)
                UpdateOperators();
            return _handsOperators;
        }
    }

    private void Start()
    {
        foreach (XdeAsbPart part in notGrabableAtStartParts)
            MarkAsNotGrabbable(part);
    }

    public void MarkAsGrabbable(XdeAsbPart part)
    {
        part.Grabbable = true;
        foreach (var handsOperator in HandsOperators)
            handsOperator.UpdateParts();
        
    }

    public void MarkAsNotGrabbable(XdeAsbPart part)
    {
        part.Grabbable = false;
        foreach (var handsOperator in HandsOperators)
        {
            handsOperator.UpdateParts();
        }
    }

    public void MarkAsNotDismountable(XdeAsbPart part)
    {
        part.dismountable = false;
        foreach (var handsOperator in HandsOperators)
        {
            handsOperator.UpdateParts();
        }
    }

    public void MarkAsDismountable(XdeAsbPart part)
    {
        part.dismountable = true;
        foreach (var handsOperator in HandsOperators)
        {
            handsOperator.UpdateParts();
        }
    }

    public void FreezeJoint(XdeUnitJoint joint)
    {
        var monitor = joint.GetComponent<XdeUnitJointMonitor>();
        if (monitor != null)
        {
            joint.SetMaxValue((float)monitor.currentPosition);
            joint.SetMinValue((float)monitor.currentPosition);
            joint.SetUseMax(true);
            joint.SetUseMax(false);
        }
    }

    public void UpdateOperators()
    {
        _handsOperators = FindObjectsOfType<XdeAsbOperatorGraspManipulator>();
    }
}