using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
public class LogicManagerScript : MonoBehaviour
{

    public GameObject objectSpawner;
    public ObstacleSpawnerScript spawnerScript;
    public GameObject highScore;
    public GameObject currentScore;
    public GameObject playerObject;
    public GameObject scenarioObject;
    public TextMeshProUGUI currentScoreText;
    public new ParticleSystem particleSystem;
    public AudioSource pointSound;
    public float pointInterval = 10f;
    public float miliSecondInterval = 0.1f;
    public float totalMiliSeconds = 0f;
    public int countableTotalMiliSeconds = 0;
    public float timerMiliSeconds = 0f;
    public float spawnTimer = 0f;
    public float spawnDelaySeconds = 1;
    public float obstaclesMoveSpeed = 6;
    public float spawnLowestDelay = 0.5f;
    public float spawnHighestDelay = 2f;
    public float SHDelayIncreaseOverTime = 5;
    public float SLDelayIncreaseOverTime = 5;
    public float simulationSpeedIncreaseOverTime = 5;
    public float obstaclesMoveSpeedIncreaseOverTime = 10;
    public float groundMoveSpeedIncreaseOverTime = 5;
    public int obstacleMaxRange = 7;
    public float obstacleMinimumProportionSize = 1f;
    public float obstacleMaximumProportionSize = 1.5f;
    public float obstacleIncreaseOverTime = 15;


    private void Start()
    {
        spawnerScript = objectSpawner.GetComponent<ObstacleSpawnerScript>();
        currentScoreText = currentScore.GetComponent<TextMeshProUGUI>();

        int playerHighScore = PlayerPrefs.HasKey("highScore") ? PlayerPrefs.GetInt("highScore") : 0;
        highScore.GetComponent<TextMeshProUGUI>().SetText(playerHighScore.ToString("D5"));
    }


    private void Update()
    { 
        showTimer();

        spawnTimer += Time.deltaTime;

        if(spawnTimer > spawnDelaySeconds){
            spawnerScript.SpawnObstacle(sizeProportion: Random.Range(obstacleMinimumProportionSize, obstacleMaximumProportionSize), obstacleIndex: Random.Range(0, obstacleMaxRange), moveSpeed: obstaclesMoveSpeed);
            spawnTimer = 0;
            spawnDelaySeconds = Random.Range(spawnLowestDelay, spawnHighestDelay);
        }
    }

    public void increaseGameSpeed()
    {  
        ParalaxScenarioScript paralaxScript = scenarioObject.GetComponent<ParalaxScenarioScript>();
        paralaxScript.updatableSpeed += (paralaxScript.updatableSpeed * groundMoveSpeedIncreaseOverTime) / 100;
        obstaclesMoveSpeed += (obstaclesMoveSpeed * obstaclesMoveSpeedIncreaseOverTime) / 100;
        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.simulationSpeed += (mainModule.simulationSpeed * simulationSpeedIncreaseOverTime) / 100;
        spawnLowestDelay -= (spawnLowestDelay * SLDelayIncreaseOverTime) / 100;
        spawnHighestDelay -= (spawnHighestDelay * SHDelayIncreaseOverTime) / 100;

        if(obstacleMinimumProportionSize < 1.3f){
            obstacleMinimumProportionSize += (obstacleMinimumProportionSize * obstacleIncreaseOverTime) / 100;
        }

        if(obstacleMaximumProportionSize < 1.9f){
            obstacleMaximumProportionSize += (obstacleMaximumProportionSize * obstacleIncreaseOverTime) / 100;
        }

    }

    public void showTimer()
    {
        timerMiliSeconds += Time.deltaTime;

        if (timerMiliSeconds >= miliSecondInterval)
        {
            totalMiliSeconds += 0.1f;

            countableTotalMiliSeconds = Mathf.FloorToInt((totalMiliSeconds * 10));
            
            if(countableTotalMiliSeconds == 500){
                obstacleMaxRange++;
            }

            //every 10 seconds, plays the point sound
            if (countableTotalMiliSeconds % 100 == 0)
            {

                pointSound.Play();
                StartCoroutine(TimerFadingAnimation());
                increaseGameSpeed();
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
