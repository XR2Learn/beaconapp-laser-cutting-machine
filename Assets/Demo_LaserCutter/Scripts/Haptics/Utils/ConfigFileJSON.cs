//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by CEA
//
//=============================================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using System;

public class ConfigFileJSON
{

  private string fileName;
  private StreamReader configStream;
  private JSONNode parameters;
  public JSONNode Parameters { get { return parameters; } }

  public ConfigFileJSON(string fileName, bool debug = false)
  {
    this.fileName = System.IO.Path.GetFullPath(fileName);
    if (!File.Exists(fileName))
    {
      Debug.LogException(new NullReferenceException("File " + this.fileName + " not found"));
      parameters = JSON.Parse("{}");
      return;
    }
    configStream = new StreamReader(this.fileName);


    if (debug)
      Debug.Log("Start read file " + this.fileName);
    try
    {
      string filedata = configStream.ReadToEnd();
      parameters = JSON.Parse(filedata);
    }
    catch (Exception e)
    {
      throw new Exception("Error during the reading of the file" + this.fileName, e);
    }
    finally
    {
      if (debug)
        Debug.Log("Ending read file " + this.fileName);
      configStream.Close();
    }
  }

  /*public static ForceFeedbackData convertToForceFeedbackData(JSONNode node)
  {
      ForceFeedbackData ff = null;
      JSONArray arr = node.AsArray;
      if (arr.Count >= 9)
      {
          ff = new ForceFeedbackData(arr[0].AsFloat, arr[1].AsFloat, arr[2].AsFloat, arr[3].AsFloat, arr[4].AsFloat, arr[5].AsFloat, arr[8].AsBool, arr[6].AsFloat, arr[7].AsFloat);
      }
      else
      {
          ff = new ForceFeedbackData();
      }
      return ff;
  }
  */

  public static AnimationCurve convertToCurve(JSONNode nodeCurve)
  {
    AnimationCurve cr = new AnimationCurve();
    if (nodeCurve.HasKey("x") && nodeCurve.HasKey("y"))
    {
      JSONArray arrK = nodeCurve["x"].AsArray;
      JSONArray arrV = nodeCurve["y"].AsArray;

      for (int i = 0; i < Mathf.Min(arrK.Count, arrV.Count); i++)
      {
        cr.AddKey(arrK[i].AsFloat, arrV[i].AsFloat);
      }
    }
    return cr;
  }

}
