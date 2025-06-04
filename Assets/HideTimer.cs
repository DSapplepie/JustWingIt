using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class HideTimer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject welcomePanel;
    [SerializeField] private GameObject minuteTimer;

    private void Update()
    {
        if (welcomePanel.activeInHierarchy || introPanel.activeInHierarchy)
            minuteTimer.SetActive(false);
        else
            minuteTimer.SetActive(true);
        
    }
}
