using UnityEngine;

public class PartCleaner : MonoBehaviour
{
    #region Attributes

    [SerializeField]
    private bool _canInteract;
    [SerializeField]
    private SpongeHandler _cleaningEventHandler;

    #endregion

    #region Properties

    public bool CanInteract
    {
        get => _canInteract;
        set => _canInteract = value;
    }

    #endregion

    #region Logic

    public void SetEventHandler(SpongeHandler pHandler)
    {
        _cleaningEventHandler = pHandler;
    }

    #endregion

    #region Events

    private void OnTriggerEnter(Collider pOther)
    {
        if (!_canInteract || !_cleaningEventHandler)
            return;

        _cleaningEventHandler.OnItemGotCleanedBySponge(pOther.gameObject);
    }

    #endregion
}