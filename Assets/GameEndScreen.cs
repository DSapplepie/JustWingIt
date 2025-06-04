using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
public class GameEndScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameEndScreen;
    //[SerializeField] private Button restartButton;
    [SerializeField] TextMeshProUGUI finalTime;
    [SerializeField] TextMeshProUGUI totalTime;


    private void Start()
    {
        if (gameEndScreen != null)
            gameEndScreen.SetActive(false);

    }
   
    /*private void OnReset()
    {
        if (gameEndScreen != null)
            gameEndScreen.SetActive(false);
    }*/

    void Update()
    {
        finalTime.text = totalTime.text;
        /*if (restartButton != null)
            restartButton.onClick.AddListener(OnReset);*/
    }
}
