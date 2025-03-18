//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by CEA
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XdeEngine.Assembly;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using XdeNetwork;
using XdeEngine.Core;

namespace VMachina
{
  public class UIStep : XdeAsbStep
  {
    public Button buttonToUse;


    private XdeRPCApi rpcApi;

    private string rpc_ui_step_solve = "";

    // Start is called before the first frame update
    void Awake()
    {
      buttonToUse.onClick.AddListener(SolveUIEvent);
      rpcApi = FindObjectOfType<XdeRPCApi>();
    }

    public void SetRpcTopics()
    {
      rpc_ui_step_solve = "on_UI_step_solved_" + this.gameObject.GetInstanceID();
    }

    public override void Activate(bool delayed = false)
    {
      if (delayed)
      {
        isActive = false;
        delayedActivation = 2;
      }
      else
      {
        isActive = true;
        StepBind();
        if (activationEvent != null)
          activationEvent.Invoke(this);
      }
    }

    public void SolveUIEvent()
    {
      Debug.LogWarning("Solve Event");
      if (isCompleted)
        return;
      else if (this.isActive)
      {
        this.Solve();
        rpcApi.CallRPC(rpc_ui_step_solve, false);
      }
    }

    public override async Task SolveAsync()
    {
      StepUnbind();
      Complete();
      buttonToUse.OnPointerClick(new UnityEngine.EventSystems.PointerEventData(EventSystem.current));
      Deactivate();    
    }

    // Clears all the parts
    public override void Reinitialise()
    {
      Deactivate();
      Uncomplete();
    }

    public async void StepBind()
    {
      SetRpcTopics();

      await rpcApi.BindRPCAsync(rpc_ui_step_solve, async () =>
      {
        Debug.LogWarning("RPC RECEIVED");
        Debug.LogWarning(" / isActive : " + isActive + " / isCompleted : " + isCompleted);
        await this.SolveAsync();
      });
    }

    async void StepUnbind()
    {
      await rpcApi.UnbindRPCAsync("on_step_solved");
    }

  }
}
