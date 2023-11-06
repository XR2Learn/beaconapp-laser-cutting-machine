using UnityEngine;

public class DustCleaner : MonoBehaviour
{
    #region Attributes

    [SerializeField]
    private bool _canInteract;
    [SerializeField]
    private DustCleaningHandler _dustCleaningHandler;

    public int dustLayer = 1;

    #endregion

    #region Properties

    public bool CanInteract
    {
        get => _canInteract;
        set => _canInteract = value;
    }

    #endregion

    #region Events

    private void OnTriggerEnter(Collider p_other)
    {
        if (!_canInteract)
            return;

        Dust lDust = p_other.GetComponent<Dust>();
        if (lDust)
        {
            if (lDust.dustLayer == this.dustLayer)
            {
                lDust.Clean();
                _dustCleaningHandler.OnDustGoVacuumed();
            }
        }
    }

    #endregion

    #region Logic

    public void ActivateCleaningHandler(DustCleaningHandler pHandler)
    {
        _dustCleaningHandler = pHandler;
        _dustCleaningHandler.gameObject.SetActive(true);
        CanInteract = true;
    }

    public void DeactivateCleaningHandler()
    {
        CanInteract = false;
    }

    #endregion
}