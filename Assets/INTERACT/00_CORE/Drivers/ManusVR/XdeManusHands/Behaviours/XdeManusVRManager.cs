using UnityEngine;
using ManusVR.Core.ProjectManagement;

    [ExecuteInEditMode]
    public class XdeManusVRManager : MonoBehaviour
    {
        [Tooltip("Automatically add the required defines to the project.")]
        public bool AutoManageDefines = true;

        public Transform LeftTargetController { get; private set; }
        public Transform RightTargetController { get; private set; }

        // Add by GC 2019-10-02
        public Transform LeftTargetTracker;
        public Transform RightTargetTracker;

        protected virtual void Awake()
        {
            if (AutoManageDefines)
            {
                ManusVRCoreDefines.TrySettingManusVRCoreDefine();
                ManusVRCoreDefines.TrySettingSteamVRDefine();
            }

            if (!Application.isPlaying) return;

            InitializeSteamVRTracking();
        }

        protected virtual bool InitializeSteamVRTracking()
        {
#if MANUSVR_DEFINE_STEAMVR_PLUGIN_1_2_2_OR_NEWER
            SteamVR_ControllerManager steamVRControllerManager = Component.FindObjectOfType<SteamVR_ControllerManager>();

            if (steamVRControllerManager == null)
            {
                return false;
            }

            // Commented by Antoine 2019-10-01 for test
            //LeftTargetController = steamVRControllerManager.left.transform;
            //RightTargetController = steamVRControllerManager.right.transform;

            // Add by GC 2019-10-02
            LeftTargetController = LeftTargetTracker;
            RightTargetController = RightTargetTracker;
            return true;
#endif
            return false;
        }
    }


