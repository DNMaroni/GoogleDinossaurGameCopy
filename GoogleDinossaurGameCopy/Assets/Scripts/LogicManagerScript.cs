using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Unity.VisualScripting;
public class LogicManagerScript : MonoBehaviour
{

    public GameObject objectSpawner;
    public ObstacleSpawnerScript spawnerScript;
    public GameObject highScore;
    public GameObject currentScore;
    public GameObject playerObject;
    public TextMeshProUGUI currentScoreText;
    public AudioSource pointSound;
    public float pointInterval = 10f;
    public float miliSecondInterval = 0.1f;
    public float totalMiliSeconds = 0f;

    public int countableTotalMiliSeconds = 0;
    public float timerMiliSeconds = 0f;

    private void Start()
    {
        spawnerScript = objectSpawner.GetComponent<ObstacleSpawnerScript>();
        currentScoreText = currentScore.GetComponent<TextMeshProUGUI>();
        spawnerScript.SpawnObstacle(10, 1f);

        int playerHighScore = PlayerPrefs.HasKey("highScore") ? PlayerPrefs.GetInt("highScore") : 0;
        highScore.GetComponent<TextMeshProUGUI>().SetText(playerHighScore.ToString("D5"));
    }


    private void Update()
    {
        showTimer();
    }

    public void showTimer()
    {
        timerMiliSeconds += Time.deltaTime;

        if (timerMiliSeconds >= miliSecondInterval)
        {
            totalMiliSeconds += 0.1f;

            countableTotalMiliSeconds = Mathf.FloorToInt((totalMiliSeconds * 10));;
            //every 10 seconds, plays the point sound

            if (countableTotalMiliSeconds % 100 == 0)
            {

                pointSound.Play();
                StartCoroutine(TimerFadingAnimation());
            }

            currentScoreText.SetText(countableTotalMiliSeconds.ToString("D5"));


            timerMiliSeconds = 0f;
        }
    }

    IEnumerator TimerFadingAnimation()
    {
        WaitForSeconds wait = new WaitForSeconds(0.3f);

        bool activeObject = true;
        for (int i = 0; i < 6; i++)
        {
            activeObject = !activeObject;
            currentScore.SetActive(activeObject);
            yield return wait;
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
