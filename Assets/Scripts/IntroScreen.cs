using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class IntroScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject introPanel;
    [SerializeField] private Button continueButton;

    [Header("Key Binding UI Elements")]
    [SerializeField] private TextMeshProUGUI moveBindingText;
    [SerializeField] private TextMeshProUGUI jumpBindingText;
    [SerializeField] private TextMeshProUGUI glideBindingText;
    [SerializeField] private TextMeshProUGUI sprintBindingText;
    [SerializeField] private TextMeshProUGUI attackBindingText;
    [SerializeField] private TextMeshProUGUI interactBindingText;
    [SerializeField] private TextMeshProUGUI crouchBindingText;
    [SerializeField] private TextMeshProUGUI previousBindingText;
    [SerializeField] private TextMeshProUGUI nextBindingText;

    private void Start()
    {
        // Initialize the intro screen
        ShowIntroScreen();

        // Add listener to continue button
        if (continueButton != null)
            continueButton.onClick.AddListener(HideIntroScreen);
    }

    private void ShowIntroScreen()
    {
        // Populate key binding information
        moveBindingText.text = "WASD / Arrow Keys";
        jumpBindingText.text = "Space";
        glideBindingText.text = "Hold Space (while in air)";
        sprintBindingText.text = "Left Shift";
        attackBindingText.text = "Mouse Left Button / Enter";
        interactBindingText.text = "E";
        crouchBindingText.text = "C";
        previousBindingText.text = "1 / D-pad Left";
        nextBindingText.text = "2 / D-pad Right";

        Time.timeScale = 0; // Pause the game
        introPanel.SetActive(true);
    }

    private void HideIntroScreen()
    {
        Time.timeScale = 1; // Resume the game
        introPanel.SetActive(false);
    }

    private void Update()
    {
        // Toggle intro screen with F1 key
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (introPanel.activeInHierarchy)
            {
                HideIntroScreen();
            }
            else
            {
                ShowIntroScreen();
            }
        }
    }
}
