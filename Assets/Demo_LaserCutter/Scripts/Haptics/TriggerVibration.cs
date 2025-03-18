//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by CEA
//
//=============================================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XdeEngine.Core;

public class TriggerVibration : MonoBehaviour
{
    [Range(0,15)]
    public int idPattern;

    // Start is called before the first frame update
    private TextMesh text;

    private void Start()
    {
        text = GetComponentInChildren<TextMesh>();
        if (text)
        {
            text.text = idPattern.ToString();
        }
    }

    //private void StartVibration(Collider other)
    //{
    //    if (other.gameObject.tag == "Hand")
    //    {
    //        HapticManager.Instance.playPattern(idPattern);
    //    }
    //}
 }
