//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by CEA
//
//=============================================================================

using UnityEngine;
using Outliner;
using XdeEngine.Assembly;
using XdeEngine.Core;

namespace VMachina
{
  public class XdeAsbSelectionTarget : XdeAsbTarget
  {
    private XdeScene xdeScene;
    public void Start()
    {
      xdeScene = FindObjectOfType<XdeScene>();
      xdeScene.SimulationStateChangedEvent += Initialize;
   
    }

    void Initialize(SIMULATION_STATE state)
    {
      if (state == SIMULATION_STATE.ACTIVE)
      {
        OutlineEffect outline = FindObjectOfType<OutlineEffect>();
        if (outline == null)
        {
          outline = Camera.main.gameObject.AddComponent<OutlineEffect>();
          outline.lineThickness = 0.75f;
          outline.sourceCamera = Camera.main;
        }

        outline.lineColor0 = color;
      }
    }

    public void SetOutlineEffect(OutlineEffect outlineEffect)
    {
      if (outlineEffect != null)
      {
        outlineEffect.lineThickness = 0.75f;
        outlineEffect.lineColor0 = color;
      }
    }

    // Call this to create an Outline Component at an Assembly Part of a specific serie
    public override void UpdateVisualGuide(XdeAsbStep step)
    {
      Debug.LogError("Update Visual Help");
      // If the Assembly Step is an Assembly Placing
      if (step.GetType() == typeof(XdeAsbPartSelectionStep))
      {
        XdeAsbPartSelectionStep selectionStep = step as XdeAsbPartSelectionStep;
        for (int i = 0; i< selectionStep.selectableParts.Count; i++)
        {
          if (selectionStep.selectableParts[i].GetComponent<Outliner.Outline>() == null)
          {
            AddSelectablePartOutline(selectionStep.selectableParts[i], 0);
          }
          else
            DestroySelectablePartOutline(selectionStep.selectableParts[i]);
        }
      }
    }

    // Adding an Outline to each of the GameObject's child with active Mesh Renderer
    public bool AddSelectablePartOutline(XdeAsbSelectablePart part, int idColor)
    {
      if (part == null)
        return false;

      bool outlineAdded = false;

      foreach (MeshFilter mesh in part.GetComponentsInChildren<MeshFilter>())
      {
        if (mesh.GetComponent<Outliner.Outline>() == null)
        {
          mesh.gameObject.AddComponent<Outliner.Outline>().color = idColor;
          outlineAdded = true;
        }
        else
          mesh.gameObject.GetComponent<Outliner.Outline>().color = idColor;
      }
      return outlineAdded;
    }

    public void DestroySelectablePartOutline(XdeAsbSelectablePart part)
    {
      if (part == null)
        return;

      foreach (MeshFilter mesh in part.GetComponentsInChildren<MeshFilter>())
      {
        if (mesh.GetComponent<Outliner.Outline>() != null)
        {
          Destroy(mesh.GetComponent<Outliner.Outline>());
        }
      }
    }

    public override void Clear(XdeAsbStep step)
    {
      // If the Assembly Step is an Assembly Placing
      if (step.GetType() == typeof(XdeAsbPartSelectionStep))
      {
        XdeAsbPartSelectionStep selectionStep = step as XdeAsbPartSelectionStep;
        for (int i = 0; i < selectionStep.selectableParts.Count; i++)
          DestroySelectablePartOutline(selectionStep.selectableParts[i]);
      }
    }

    public override void Clear()
    {
      XdeAsbScenario scenario = GetComponent<XdeAsbScenario>();
      if (scenario != null)
        foreach (XdeAsbStep step in scenario.steps)
          Clear(step);
    }
  }
}
