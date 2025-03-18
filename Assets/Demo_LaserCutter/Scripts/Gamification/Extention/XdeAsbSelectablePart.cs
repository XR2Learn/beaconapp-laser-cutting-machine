//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by CEA
//
//=============================================================================

using UnityEngine;
using XdeEngine.Assembly;
using Outline = Outliner.Outline;

public class XdeAsbSelectablePart : XdeAsbPart
{
    public bool isSelected = false;
    public bool shouldBeDismountable = false;
    public bool shouldBeGrabbable = false;

    [SerializeField]
    private bool _canBeSelected = false;

    public bool CanBeSelected
    {
        get => _canBeSelected;
        set => _canBeSelected = value;
    }

    public void Select()
    {
        isSelected = _canBeSelected;

        /*
        foreach (MeshFilter mesh in this.GetComponentsInChildren<MeshFilter>())
        {
            Outline lOutline = mesh.GetComponent<Outline>();

            if (lOutline)
                Destroy(lOutline);
        }
        */
    }

    public void ResetSelected()
    {
        isSelected = false;
        _canBeSelected = true;
    }
}