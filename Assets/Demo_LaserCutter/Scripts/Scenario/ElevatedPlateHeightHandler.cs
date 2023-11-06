using UnityEngine;
using UnityEngine.Events;

public class ElevatedPlateHeightHandler : MonoBehaviour
{
    #region Attributes

    [SerializeField]
    private ElevatedPlateController _elevatedPlateController;
    [SerializeField]
    private Transform _elevatedPlate;
    [SerializeField]
    private UnityEvent _onPlaqueReachedTargetHeight;
    [SerializeField]
    private float _targetHeight;

    #endregion

    #region Unity Callbacks

    private void OnEnable()
    {
        _elevatedPlateController.YValueMax = _targetHeight;
    }

    #endregion

    #region Updates

    void Update()
    {
        if (_elevatedPlate)
        {
            if (_elevatedPlate.localPosition.y == _targetHeight)
            {
                _onPlaqueReachedTargetHeight.Invoke();
                this.gameObject.SetActive(false);
            }
        }
    }

    #endregion
}