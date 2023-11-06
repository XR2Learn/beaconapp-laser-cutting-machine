using UnityEngine;
using System.IO;
using xde.unity;

namespace INTERACT.Runtime.PointCloud
{
    [ExecuteInEditMode]
    // ReSharper disable once InconsistentNaming
    public class PointCloudManager : MonoBehaviour
    {
        public KeyCode m_changeAppearanceKey = KeyCode.F1;

        private System.Diagnostics.Stopwatch vTimer = new System.Diagnostics.Stopwatch();
        //INCORE
        private OctopclTree m_pcltree;
        //OUTOFCORE
        private OctopclTreeServer m_pcltreeServer;
        private OctopclAppearance m_pclAppearance;

        // Use this for initialization
        void Awake()
        {
            m_pcltree = this.GetComponent<OctopclTree>();
            m_pcltreeServer = this.GetComponent<OctopclTreeServer>();
            m_pclAppearance = this.GetComponent<OctopclAppearance>();

            //_pclAppearance.appearance = OctopclAppearance.Appearance.ConeSplat;
            //_pclAppearance.UpdateMaterial();

            if (m_pcltreeServer != null)
            {
                if (!File.Exists(m_pcltreeServer.file_path) && !string.IsNullOrEmpty(m_pcltreeServer.file_path)) //If original file does not exist, check in "ApplicationName_Data/3DPointClouds" folder
                {
                    if (File.Exists(Path.Combine(Application.dataPath, "3DPointClouds/" + Path.GetFileName(m_pcltreeServer.file_path))))
                    {
                        m_pcltreeServer.file_path = Path.Combine(Application.dataPath, "3DPointClouds/" + Path.GetFileName(m_pcltreeServer.file_path));
                        m_pcltreeServer.LoadDB();
                    }
                    else
                        Debug.LogError("PointCloud database file not found at " + Path.Combine(Application.dataPath, "3DPointClouds/" + Path.GetFileName(m_pcltreeServer.file_path)));
                }
            }


        }

        public void IncreasePointBudget()
        {
            if (m_pcltree != null)
                m_pcltree.pointBudget += 1000000;
            else
                m_pcltreeServer.pointBudget += 1000000;

            vTimer.Reset();
            vTimer.Start();
        }

        public void DecreasePointBudget()
        {
            if (m_pcltree != null)
                m_pcltree.pointBudget -= 1000000;
            else
                m_pcltreeServer.pointBudget -= 1000000;
            vTimer.Reset();
            vTimer.Start();
        }

        void Update()
        {
            if (Application.isPlaying)
            {
                if (m_pcltree != null)
                    m_pcltree.viewCamera = Camera.main;
                else
                    m_pcltreeServer.viewCamera = Camera.main;

                if (Input.GetKeyDown(m_changeAppearanceKey))
                {
                    m_pclAppearance.appearance = (m_pclAppearance.appearance == Appearance.AdaptativeConeSplat) ? Appearance.Point : Appearance.AdaptativeConeSplat;
                    m_pclAppearance.UpdateMaterial();
                }
            }
            else
            {
#if UNITY_EDITOR
                if (UnityEditor.SceneView.lastActiveSceneView != null)
                {
                    if (m_pcltree != null)
                        m_pcltree.viewCamera = UnityEditor.SceneView.lastActiveSceneView.camera;
                    else
                        m_pcltreeServer.viewCamera = UnityEditor.SceneView.lastActiveSceneView.camera;
                }
#endif
            }
        }

        void OnGUI()
        {
            if (Application.isPlaying)
            {
                if (vTimer.IsRunning && (float)vTimer.Elapsed.TotalSeconds < 3.0f)
                {
                    int l_w = Screen.width, l_h = Screen.height;
                    GUIStyle l_style = new GUIStyle();
                    Rect l_rect = new Rect(0, 0, l_w, (float)l_h * 2 / 100);
                    l_style.alignment = TextAnchor.UpperLeft;
                    l_style.fontSize = l_h * 2 / 100;
                    l_style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);

                    string l_text;
                    if (m_pcltree != null)
                        l_text = "Point Budget :" + m_pcltree.pointBudget.ToString();
                    else
                        l_text = "Point Budget :" + m_pcltreeServer.pointBudget.ToString();

                    GUI.Label(l_rect, l_text, l_style);
                }
                else if (vTimer.IsRunning)
                {
                    vTimer.Stop();
                }
            }
        }
    }
}
