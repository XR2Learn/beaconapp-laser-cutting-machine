//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XR2Learn.Common;
using XR2Learn.DataCollection.Managers;

public class DataCollectionMessageManager : MonoBehaviour
{
    [SerializeField]
    private MainDataCollectionManager m_dataCollectionManager;

    [SerializeField]
    private GameObject m_popup;

    [SerializeField]
    private TMP_Text m_text;

    [SerializeField]
    private int m_durationOfDisplay;

    [SerializeField]
    private string m_startMessage;

    [SerializeField]
    private string m_endMessage;

    private Coroutine m_displayCoroutine;

    // Start is called before the first frame update
    private void Awake()
    {
        m_popup.SetActive(false);
    }

    private void OnEnable()
    {
        m_dataCollectionManager.DataCollectionChanged += OnDataCollectionChanged;
    }

    private void OnDisable()
    {
        m_dataCollectionManager.DataCollectionChanged -= OnDataCollectionChanged;
    }

    private void OnDataCollectionChanged(bool p_isRunning)
    {
        m_text.text = p_isRunning ? m_startMessage : m_endMessage;


        m_popup.SetActive(true);
        SoundManager.PlaySound("DataCollectionToggle", p_isRunning);
        HapticsManager.Vibrate(HapticsManager.Controller.BOTH, 0, 0.1f, 120f, 0.8f);
        if (m_displayCoroutine != null)
            StopCoroutine(m_displayCoroutine);

        m_displayCoroutine = StartCoroutine(HideTextAfterTime());
    }

    private IEnumerator HideTextAfterTime()
    {
        yield return new WaitForSeconds(m_durationOfDisplay);
        m_popup.SetActive(false);
    }
}

