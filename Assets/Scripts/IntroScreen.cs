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

    [Header("Welcome Screen UI Elements")]
    [SerializeField] private GameObject welcomePanel;
    [SerializeField] private Button welcomeContinueButton;

    private void Start()
    {
        // Show the welcome screen first
        ShowWelcomeScreen();

        // Add listener to welcome continue button
        if (welcomeContinueButton != null)
            welcomeContinueButton.onClick.AddListener(OnWelcomeContinue);

        // Add listener to intro continue button
        if (continueButton != null)
            continueButton.onClick.AddListener(HideIntroScreen);
    }

    private void ShowWelcomeScreen()
    {
        Time.timeScale = 0; // Pause the game
        if (welcomePanel != null)
            welcomePanel.SetActive(true);
        if (introPanel != null)
            introPanel.SetActive(false);
    }

    private void OnWelcomeContinue()
    {
        if (welcomePanel != null)
            welcomePanel.SetActive(false);
        ShowIntroScreen();
    }

    private void ShowIntroScreen()
    {
        // Populate key binding information
        moveBindingText.text = "Move - WASD / Arrow Keys";
        jumpBindingText.text = "Jump - Space";
        glideBindingText.text = "Glide - Hold Space (while in air)";
        sprintBindingText.text = "Sprint - Left Shift";
        attackBindingText.text = "Attack - Mouse Left Button / Enter";
        interactBindingText.text = "Interact - E";
        crouchBindingText.text = "Crouch - C";
        previousBindingText.text = "Prev. - 1 / D-pad Left";
        nextBindingText.text = "Next - 2 / D-pad Right";

        Time.timeScale = 0; // Pause the game
        if (introPanel != null)
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
