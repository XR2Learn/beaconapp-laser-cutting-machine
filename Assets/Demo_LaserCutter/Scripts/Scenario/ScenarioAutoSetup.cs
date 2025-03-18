using UnityEngine;
using VMachina;
using XdeEngine.Assembly;

public class ScenarioAutoSetup : MonoBehaviour
{
    #region Attributes

    [SerializeField]
    private XdeAsbScenario _scenario;

    #endregion

    #region Initialization

    void Awake()
    {
        if (!_scenario)
            return;

        AutoSetupSteps();
        AutoSetupHaptics();
    }

    private void OnEnable()
    {
        foreach (Transform t in transform)
        {
            t.name = t.name.Replace("_OK", "");
        }
    }

    #endregion

    #region Logic

    private void AutoSetupHaptics()
    {

        for (int i = 0; i < _scenario.steps.Count; i++)
        {
            XdeAsbStep lStep = _scenario.steps[i];

            if (i == _scenario.steps.Count - 1)
            {
                // Scenario complete
                lStep.completedEvent.AddListener((x) => VRHapticManager.Instance.playPattern(7));
            }
            else
            {
                // Step complete
                lStep.completedEvent.AddListener((x) => VRHapticManager.Instance.playPattern(6));
            }
        }
    }

    private void AutoSetupSteps()
    {
        int lChildrenCount = _scenario.transform.childCount;

        if (lChildrenCount < 2)
        {
            Debug.LogWarning("[ScenarioAutoSetup] Couldn't start : not enough steps");
            return;
        }

        for (int i = 0; i < lChildrenCount - 1; i++)
        {
            // Current step
            XdeAsbStep lCurrentStep = _scenario.transform.GetChild(i).GetComponent<XdeAsbStep>();

            lCurrentStep.gameObject.name.Replace("_OK", "");

            if (i == 0)
            {
                if (lCurrentStep is XdeAsbPartSelectionStep currentStep)
                {
                    //currentStep.selectableParts[0].canBeSelected = true;
                    foreach (var part in currentStep.selectableParts)
                    {
                        part.CanBeSelected = true;
                    }
                }
            }

            // Next step
            XdeAsbStep lNextStep = _scenario.transform.GetChild(i + 1).GetComponent<XdeAsbStep>();

            if (lNextStep is XdeAsbPartSelectionStep nextStep)
            {
                //lCurrentStep.completedEvent.AddListener(x => nextStep.selectableParts[0].CanBeSelected = true);
                foreach (var part in nextStep.selectableParts)
                {
                    lCurrentStep.completedEvent.AddListener(x => part.CanBeSelected = true);
                }
            }

            lNextStep?.dependencies.Clear();
            lNextStep?.dependencies.Add(lCurrentStep);
        }
    }

    #endregion
}