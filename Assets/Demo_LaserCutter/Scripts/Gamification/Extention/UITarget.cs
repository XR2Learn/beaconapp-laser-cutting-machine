//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by CEA
//
//=============================================================================

using UnityEngine;
using Outliner;
using XdeEngine.Assembly;
using UnityEngine.UI;

namespace VMachina
{
  public class UITarget : XdeAsbTarget
  {

    public Material invisibleMaterial;

    private Button previousButton;

    public void Start()
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
      // If the Assembly Step is an Assembly Placing
      if (step.GetType() == typeof(UIStep))
      {
        UIStep uiStep = step as UIStep;
        // If the part isn't already placed and doesn't already have an Outline as component
        if (uiStep.buttonToUse.GetComponent<Outliner.Outline>() == null)
        {
          AddButtonOutline(uiStep.buttonToUse, 0);
        }
        else
          DestroyButtonOutline(uiStep.buttonToUse);

      }
    }

    // Adding an Outline to each of the GameObject's child with active Mesh Renderer
    public bool AddButtonOutline(Button button, int idColor)
    {
      if (button == null)
        return false;

      bool outlineAdded = false;
      BoxCollider buttonCollider = button.GetComponent<BoxCollider>();

      GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
      cube.name = "Outline";
      cube.transform.parent = button.transform;
      cube.transform.localPosition = Vector3.zero;
      cube.transform.localRotation = Quaternion.identity;
      cube.transform.localScale = buttonCollider.size;
      cube.GetComponent<MeshRenderer>().material = invisibleMaterial;
      Destroy(cube.GetComponent<BoxCollider>());

      if (cube.GetComponent<MeshRenderer>() != null)
      {
        if (cube.GetComponent<Outliner.Outline>() == null)
        {
          cube.gameObject.AddComponent<Outliner.Outline>().color = idColor;
          outlineAdded = true;
        }
        else
          cube.gameObject.GetComponent<Outliner.Outline>().color = idColor;
      }
      return outlineAdded;
    }

    public void DestroyButtonOutline(Button button)
    {
      if (button == null)
        return;

      Outliner.Outline[] Outlines = button.GetComponentsInChildren<Outliner.Outline>();
      foreach (Outliner.Outline child in Outlines)
      {
        Destroy(child.gameObject);
      }
    }

    public override void Clear(XdeAsbStep step)
    {
      // If the Assembly Step is an Assembly Placing
      if (step as UIStep != null)
      {
        UIStep uiStep = step as UIStep;
        DestroyButtonOutline(uiStep.buttonToUse);
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
