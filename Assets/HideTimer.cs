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
        // If the welcome panel  or intro panel is active, then hide the timer. Otherwise, show the timer.
        // Side note, the time is paused when either panel is open so the time will only count towards actual time in game.
        if (welcomePanel.activeInHierarchy || introPanel.activeInHierarchy)
            minuteTimer.SetActive(false);
        else
            minuteTimer.SetActive(true);
        
    }
}
