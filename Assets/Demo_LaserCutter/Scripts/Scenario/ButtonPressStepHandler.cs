using UnityEngine;
using XdeEngine.Assembly;

public class ButtonPressStepHandler : MonoBehaviour
{
    #region Attributes

    [SerializeField]
    private XdeAsbStep _targetStep;
    [SerializeField]
    private bool _canBePressed;

    #endregion

    #region Properties

    public bool CanBePressed
    {
        get => _canBePressed;
        set => _canBePressed = value;
    }

    #endregion

    #region Logic

    public void OnButtonPressed()
    {
        if (!_canBePressed)
            return;

        if (_targetStep.IsActive && !_targetStep.IsCompleted)
        {
            _targetStep.Complete();
        }
    }

    #endregion
}