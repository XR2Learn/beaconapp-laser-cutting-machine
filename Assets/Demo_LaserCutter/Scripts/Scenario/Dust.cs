using System.Collections;
using UnityEngine;

public class Dust : MonoBehaviour
{
    #region Attributes

    [SerializeField]
    private Transform _model;
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private float _fadingTime = 1;

    public int dustLayer = 1;

    private float _fadingValue;

    #endregion

    #region Updates

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            Clean();
    }

    #endregion

    #region Logic

    public void Clean()
    {
        _collider.enabled = false;

        StartCoroutine(CleanInternal());
    }

    private IEnumerator CleanInternal()
    {
        float lStep = 1.0f / _fadingTime;

        _fadingValue = 1.0f;
        while (_fadingValue > 0.0f)
        {
            _fadingValue = Mathf.Clamp01(_fadingValue - Time.deltaTime * lStep);
            _model.localScale = new Vector3(_fadingValue, 1.0f, _fadingValue);
            yield return null;
        }
    }

    #endregion
}