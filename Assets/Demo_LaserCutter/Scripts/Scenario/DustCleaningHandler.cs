using UnityEngine;
using UnityEngine.Events;

public class DustCleaningHandler : MonoBehaviour
{
    #region Attributes

    [SerializeField]
    private Transform _dusts;
    [SerializeField]
    private int _dustCount = 0;
    [SerializeField]
    private UnityEvent _onAllDustWasCleaned;

    #endregion

    #region Logic

    public void OnDustGoVacuumed()
    {
        if (!_dusts)
            return;

        _dustCount++;

        if (_dustCount >= _dusts.childCount)
        {
            _onAllDustWasCleaned.Invoke();
            this.gameObject.SetActive(false);
        }
    }

    #endregion
}