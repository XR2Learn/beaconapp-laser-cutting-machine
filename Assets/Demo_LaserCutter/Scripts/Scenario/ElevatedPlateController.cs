using UnityEngine;

public class ElevatedPlateController : MonoBehaviour
{
    #region Attributes

    [SerializeField]
    private Transform _elevatedPlate;
    [SerializeField]
    private float _plateSpeed = 1.0f;
    [SerializeField]
    private float _plateDirection = 1.0f;
    [SerializeField]
    private bool _move = false;
    [SerializeField]
    private float _yValueMin = -0.2f;
    [SerializeField]
    private float _yValueMax = 0;
    [SerializeField]
    private bool _interactable = false;

    [Header("DEBUG")]
    [SerializeField]
    private KeyCode _keyRaise = KeyCode.UpArrow;
    [SerializeField]
    private KeyCode _keyLower = KeyCode.DownArrow;

    #endregion

    #region Properties

    public bool Move
    {
        get => _move;
        set => _move = value;
    }

    public float YValueMax
    {
        get => _yValueMax;
        set => _yValueMax = value;
    }

    public bool Interactable
    {
        get => _interactable;
        set => _interactable = value;
    }

    #endregion

    #region Updates

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(_keyRaise))
        {
            _plateDirection = 1;
            _move = true;
        }
        else if (Input.GetKeyDown(_keyLower))
        {
            _plateDirection = -1;
            _move = true;
        }
        else if (Input.GetKeyUp(_keyRaise) || Input.GetKeyUp(_keyLower))
            _move = false;
        else if (Input.GetKeyDown(KeyCode.U))
            SetPlateLocalHeight(-0.04f);
#endif

        if (_move)
            MovePlate(_plateDirection);
    }

    #endregion

    #region Logic

    public void SetMoveDirection(float pDirection)
    {
        if (!_interactable)
            return;

        _plateDirection = pDirection;
        _move = true;
    }

    public void MovePlate(float pDirection)
    {
        if (!_interactable)
            return;

        _elevatedPlate.transform.position += Vector3.up * Time.deltaTime * _plateSpeed * pDirection;

        float yValue = Mathf.Clamp(_elevatedPlate.transform.localPosition.y, _yValueMin, _yValueMax);
        _elevatedPlate.transform.localPosition = new Vector3(0, yValue, 0);
    }

    public void SetPlateLocalHeight(float pHeight)
    {
        float yValue = Mathf.Clamp(pHeight, _yValueMin, _yValueMax);
        _elevatedPlate.localPosition = new Vector3(0, yValue, 0);
    }

    #endregion
}