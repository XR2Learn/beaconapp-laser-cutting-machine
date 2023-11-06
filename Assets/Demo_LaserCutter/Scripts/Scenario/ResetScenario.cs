using UnityEngine;
using UnityEngine.SceneManagement;
using XdeEngine.Core;

namespace Scenario
{
    public class ResetScenario : MonoBehaviour
    {
        [SerializeField]
        private XdeScene m_xdeScene;

        private bool m_resetting = false;

        public async void ResetScenarioElements()
        {
            if (m_resetting)
                return;
            m_resetting = true;
            await m_xdeScene.StopPhysicAsync();

            SceneManager.LoadScene(0);
        }
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R))
                this.ResetScenarioElements();
        }
    }
}