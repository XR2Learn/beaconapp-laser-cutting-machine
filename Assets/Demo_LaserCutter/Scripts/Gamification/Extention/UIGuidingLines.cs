//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by CEA
//
//=============================================================================

using UnityEngine;
using Outliner;
using XdeEngine.Assembly;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace VMachina
{
  public class UIGuidingLines : XdeAsbGuidingLine
  {
    private List<XdeAsbBezierLaserBeam> guidingLines;
    private XdeAsbBezierLaserBeam guidingHandLine;
    public XdeAsbOperatorGraspManipulator operatorHands;
    public bool leftHand = true;
    private Transform palm;

    private void Start()
    {
      if (operatorHands != null)
        if (leftHand)
          palm = operatorHands.physicPalmLeft.transform;
        else
          palm = operatorHands.physicPalmRight.transform;

      if (palm == null)
        operatorHands = null;

      guidingLines = new List<XdeAsbBezierLaserBeam>();

      if (operatorHands != null)
      {
        GameObject guidingLine = new GameObject("Asb Guiding User");
        LineRenderer lineRenderer = guidingLine.AddComponent<LineRenderer>();
        lineRenderer.material = Resources.Load<Material>("LaserBeam");
        lineRenderer.material.color = color;
        lineRenderer.widthMultiplier = 0.015f;

        guidingHandLine = guidingLine.AddComponent<XdeAsbBezierLaserBeam>();
        guidingHandLine.line = lineRenderer;
        guidingHandLine.lineResolution = 50;

        guidingLine.transform.SetParent(palm);
        guidingLine.transform.localPosition = Vector3.zero;
        guidingLine.transform.localRotation = Quaternion.identity;

        guidingHandLine.origin = guidingLine.transform;
      }
    }

    private void Update()
    {
      if (operatorHands != null)
      {
        guidingHandLine.gameObject.SetActive(true);
        foreach (XdeAsbBezierLaserBeam line in guidingLines)
          line.gameObject.SetActive(true);

      }
    }

    // Call this to create a Bezier laser beam GameObject at an assemblyPart of a specific serie
    public override void UpdateVisualGuide(XdeAsbStep step)
    {
      // If the Assembly Step is an Assembly Placing
      if (step.GetType() == typeof(UIStep))
      {
        UIStep uiStep = step as UIStep;
        // If the Part isn't placed and has an Assigned Keypoint
        if (uiStep.IsActive)
        {
          XdeAsbBezierLaserBeam bezierLaserBeam = uiStep.buttonToUse.GetComponentInChildren<XdeAsbBezierLaserBeam>();
          // If the Part doesn't already have a Bezier Laser Beam as child, creating one
          if (bezierLaserBeam == null)
          {
            // The prefab "BezierLaserBeam" has to be located in Resources folder
            GameObject guidingLine = new GameObject("Asb Guiding Line");
            LineRenderer lineRenderer = guidingLine.AddComponent<LineRenderer>();
            lineRenderer.material = Resources.Load<Material>("LaserBeam");
            lineRenderer.material.color = color;
            lineRenderer.widthMultiplier = 0.015f;
            bezierLaserBeam = guidingLine.AddComponent<XdeAsbBezierLaserBeam>();
            bezierLaserBeam.line = lineRenderer;

            if (guidingLines == null)
              Start();

            guidingLines.Add(bezierLaserBeam);

            //The Bezier laser beam GameObject is child of the Part GameObject
            guidingLine.transform.SetParent(uiStep.buttonToUse.transform);

            // ?? Can the "BezierLaserBeamOrigin" change through the Assembly ??
            // If there's a "BezierLaserBeamOrigin" game object, it becomes the origin of the Bezier Laser Beam
            Transform origin = palm;
            origin.Rotate(new Vector3(0f, 90f, 0f));
            bezierLaserBeam.origin = origin; 
          }
          // If the Bezier Laser Beam doesn't have a destination or its destination changed
          if (bezierLaserBeam.destination == null)
          {
            // If there's a "GuidingLineDestination" game object, it becomes the destination of the Bezier laser beam, else it's the mainKeypoint's transform
            Transform destination = uiStep.buttonToUse.transform.Find(destinationName);
            if (destination != null)
            {
              bezierLaserBeam.destination = destination;
            }
            else
            {
              GameObject destinationObject = new GameObject(destinationName);
              destinationObject.transform.position = uiStep.buttonToUse.transform.position;
              destinationObject.transform.SetParent(uiStep.buttonToUse.transform);
              bezierLaserBeam.destination = destinationObject.transform;
            }
          }
        }
      }
    }

    // Call this to destroy all Bezier Laser Beams of this Step
    public override void Clear(XdeAsbStep step)
    {
      if (step.GetType() == typeof(UIStep))
      {
        UIStep uiStep = step as UIStep;
        // Destroying the Bezier Laser Beam if the Part has one
        XdeAsbBezierLaserBeam bezierLazerBeam = uiStep.buttonToUse.GetComponentInChildren<XdeAsbBezierLaserBeam>();
        if (bezierLazerBeam != null)
        {
          guidingLines.Remove(bezierLazerBeam);
          Destroy(bezierLazerBeam.gameObject);
        }

      }
    }
  }
}
