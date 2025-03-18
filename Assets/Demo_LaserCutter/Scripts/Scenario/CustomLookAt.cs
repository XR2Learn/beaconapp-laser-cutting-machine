using UnityEngine;

[ExecuteAlways]
public class CustomLookAt : MonoBehaviour
{
    #region Attributes

    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Transform _flatForwardRef;

    public float angle = -1;
    public bool invertAngles = false;

    #endregion

    #region Updates

    private void Update()
    {
        if (!_target)
            return;

        if (_flatForwardRef)
        {
            Vector3 targetDir = _target.position - transform.position;
            angle = Vector3.Angle(targetDir, _flatForwardRef.forward);

            transform.localEulerAngles = new Vector3(angle * (invertAngles ? -1 : 1), 0, 0);
        }
    }

    #endregion
}