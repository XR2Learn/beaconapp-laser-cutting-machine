//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by CEA
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XdeEngine.Assembly;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using XdeEngine.Core;
using XdeNetwork;

namespace VMachina
{
    public class XdeAsbPartSelectionStep : XdeAsbStep
    {
        [Tooltip("At least one if false")]
        public bool requireAllParts = true;
        public List<XdeAsbSelectablePart> selectableParts;

        private XdeRPCApi rpcApi;

        private string rpc_step_solve = "";

        private void Awake()
        {
            rpcApi = FindObjectOfType<XdeRPCApi>();
        }

        public void SetRpcTopics()
        {
            rpc_step_solve = "on_step_solved_" + this.gameObject.GetInstanceID();
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
                for (int i = 0; i < selectableParts.Count; i++)
                {
                    selectableParts[i].ResetSelected();
                }
                isActive = true;
                StepBind();
                if (activationEvent != null)
                    activationEvent.Invoke(this);
            }
        }

        protected async override void FixedUpdate()
        {
            if (stopUpdate)
                return;

            base.FixedUpdate();

            if (isCompleted)
                return;

            // Making sure that the Physic is active
            if (simulationState == SIMULATION_STATE.ACTIVE)
            {
                if (isActive)
                {
                    // Checking if this Assembly Step is complete, it is completed when all parts are placed or when all Keypoints are unactivated
                    if (!isCompleted)
                    {
                        int nbPlacedPart = 0;

                        for (int i = 0; i < selectableParts.Count; i++)
                        {
                            XdeAsbSelectablePart lPart = selectableParts[i];
                            if (lPart && lPart.isSelected)
                                nbPlacedPart++;
                        }

                        //One of the two lists is completed
                        if (requireAllParts && nbPlacedPart == selectableParts.Count ||
                            !requireAllParts && nbPlacedPart >= 1)
                        {
                            stopUpdate = true;
                            Complete();

                            Debug.LogWarning("RPC Solve step");
                            rpcApi.CallRPC(rpc_step_solve, false);
                            stopUpdate = false;
                        }
                    }
                }
            }
        }

        public void SolveUIEvent()
        {
            if (isCompleted)
                return;
            else if (this.isActive)
                this.Solve();
        }

        public override async Task SolveAsync()
        {
            StepUnbind();
            Complete();
            Deactivate();
        }

        // Clears all the parts
        public override void Reinitialise()
        {
            // Clearing all Keypoints
            for (int i = 0; i < selectableParts.Count; i++)
            {
                if (selectableParts[i] != null)
                {
                    selectableParts[i].ResetSelected();
                }
            }
            Deactivate();
            Uncomplete();
        }

        public async void StepBind()
        {
            SetRpcTopics();

            await rpcApi.BindRPCAsync(rpc_step_solve, async () =>
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
