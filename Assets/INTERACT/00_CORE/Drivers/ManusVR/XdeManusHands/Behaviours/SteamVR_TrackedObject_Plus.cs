//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: For controlling in-game objects with tracked devices.
// Update jeff at seethrouhlab
// - Added ability to specify a serial number to assign to the TrackedObject
// - Added a configurable lag value. 
// - use with GameManager https://gist.github.com/jeffcrouse/6419e84d7060c08c17cf97b9c41ddd14
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System.Text;
using System.Diagnostics;


namespace Valve.VR
{
    [System.Serializable]
    public class TimedPose : System.Object
    {
        public HmdMatrix34_t mat;
        public long time;

        public TimedPose(HmdMatrix34_t _mat, long _time)
        {
            this.mat = _mat;
            time = _time; 
        }
    }

    public class SteamVR_TrackedObject_Plus : MonoBehaviour
    {
        public enum EIndex
        {
            None = -1,
            Hmd = (int)OpenVR.k_unTrackedDeviceIndex_Hmd,
            Device1,
            Device2,
            Device3,
            Device4,
            Device5,
            Device6,
            Device7,
            Device8,
            Device9,
            Device10,
            Device11,
            Device12,
            Device13,
            Device14,
            Device15,
            Device16
        }

        public EIndex index;

        [Tooltip("If not set, relative to parent")]
        public Transform origin;

        public bool isValid { get; private set; }

        //private GameManager gm;
        public long lagMillis = 10;

        List<TimedPose> TimedPoses = new List<TimedPose>();

        public string DesiredSerialNumber = "";

        Stopwatch stopWatch = new Stopwatch();

        private void OnNewPoses(TrackedDevicePose_t[] poses)
        {
            while (TimedPoses.Count > 0 && stopWatch.ElapsedMilliseconds > TimedPoses[0].time + lagMillis)
            {
                var pose = new SteamVR_Utils.RigidTransform(TimedPoses[0].mat);

                if (origin != null)
                {
                    transform.position = origin.transform.TransformPoint(pose.pos);
                    transform.rotation = origin.rotation * pose.rot;
                }
                else
                {
                    transform.localPosition = pose.pos;
                    transform.localRotation = pose.rot;
                }

                TimedPoses.RemoveAt(0);
            }

           
            if (index == EIndex.None)
                return;

            var i = (int)index;

            isValid = false;
            if (poses.Length <= i)
                return;

            if (!poses[i].bDeviceIsConnected)
                return;

            if (!poses[i].bPoseIsValid)
                return;

            isValid = true;
            
            TimedPose p = new TimedPose(poses[i].mDeviceToAbsoluteTracking, stopWatch.ElapsedMilliseconds);
            TimedPoses.Add(p);
        }

 

        SteamVR_Events.Action newPosesAction;

        SteamVR_TrackedObject_Plus()
        {
            newPosesAction = SteamVR_Events.NewPosesAction(OnNewPoses);
            stopWatch.Start();
        }

        void Start()
        {
            //gm = GameObject.Find("GameManager").GetComponent<GameManager>();

            ETrackedPropertyError error = new ETrackedPropertyError();
            StringBuilder sb = new StringBuilder();
            bool Assigned = false;
            for (int i = 0; i < SteamVR.connected.Length; ++i)
            {

                OpenVR.System.GetStringTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_SerialNumber_String, sb, OpenVR.k_unMaxPropertyStringSize, ref error);
                var SerialNumber = sb.ToString();
                if (SerialNumber == DesiredSerialNumber)
                {
                    UnityEngine.Debug.Log("Assigning device " + i + " to " + gameObject.name + " (" + DesiredSerialNumber +")");
                    SetDeviceIndex(i);
                    Assigned = true;
                    //obj.TrackedObject.gameObject.transform.Find("Model").GetComponent<SteamVR_RenderModel>().index = (SteamVR_TrackedObject.EIndex)i;
                }
            }

            if(!Assigned)
            {
                UnityEngine.Debug.Log("Couldn't find a device with Serial Number \"" + DesiredSerialNumber + "\"");
            }
        }

        private void Awake()
        {
            OnEnable();
        }

        void OnEnable()
        {
            var render = SteamVR_Render.instance;
            if (render == null)
            {
                enabled = false;
                return;
            }

            newPosesAction.enabled = true;
        }

        void OnDisable()
        {
            newPosesAction.enabled = false;
            isValid = false;
        }

        public void SetDeviceIndex(int index)
        {
            if (System.Enum.IsDefined(typeof(EIndex), index))
                this.index = (EIndex)index;
        }
    }
}