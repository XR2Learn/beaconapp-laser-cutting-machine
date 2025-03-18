using UnityEngine;
using UnityEngine.Events;

public class SpongeHandler : MonoBehaviour
{
    #region Attributes

    [SerializeField]
    private GameObject _correctItemToClean;
    [SerializeField]
    private UnityEvent _onCorrectItemWasCleanedWithSponge;

    #endregion

    #region Logic

    public void OnItemGotCleanedBySponge(GameObject pGo)
    {
        if (!_correctItemToClean)
            return;

        if (pGo == _correctItemToClean)
            _onCorrectItemWasCleanedWithSponge.Invoke();
    }

    #endregion
}