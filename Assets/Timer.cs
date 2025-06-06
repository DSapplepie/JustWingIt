using UnityEngine;
using TMPro;

//Source: https://www.youtube.com/watch?v=POq1i8FyRyQ

public class Timer : MonoBehaviour
{
    //[SerializeField]
    public TextMeshProUGUI timerText;
    float elapsedTime;


    void Update()
    {
        elapsedTime += Time.deltaTime;
        // Formats the raw time into minutes and seconds.
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        //Converts the calculated time into string format.+
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
