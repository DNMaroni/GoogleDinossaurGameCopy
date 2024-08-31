using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{

    public Animator playerAnimator;
    public AudioSource jumpSound;
    public AudioSource diyingSound;

    public Rigidbody2D playerRigidBody;
    public int jumpPower = 5;
    public bool isCrouching = false;
    public bool isJumping = false;
    public bool isGrounded = false;
    public bool isDead = false;
    public int playerHighScore;
    public new ParticleSystem particleSystem;
    public LayerMask groundLayer;
    public LayerMask obstacleLayer;
    public GameObject gameOverObject;
    public GameObject scenarioObject;
    public GameObject logicObject;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.NameToLayer("Ground");
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        playerHighScore = PlayerPrefs.HasKey("highScore") ? PlayerPrefs.GetInt("highScore") : 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetBool("IsCrouching", isCrouching);
        playerAnimator.SetBool("IsJumping", isJumping);
        playerAnimator.SetBool("IsDead", isDead);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpPower);
            isJumping = true;
            jumpSound.Play();
        }

        if (Input.GetKey(KeyCode.DownArrow) && isGrounded)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }
    }

    public void freezeAllObstacles()
    {
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        List<GameObject> objectsInLayer = new List<GameObject>();

        foreach (GameObject obj in allGameObjects)
        {
            if (obj.layer == obstacleLayer)
            {
                objectsInLayer.Add(obj);
            }
        }

        foreach (GameObject obj in objectsInLayer)
        {
            obj.GetComponent<ObstacleMovementScript>().enabled = false;

            if (obj.GetComponent<Animator>() != null)
            {
                obj.GetComponent<Animator>().enabled = false;
            }
        }
    }

    public void FreezeGame()
    {
        freezeAllObstacles();
        scenarioObject.GetComponent<ParalaxScenarioScript>().enabled = false;
        logicObject.GetComponent<LogicManagerScript>().enabled = false;
        particleSystem.Clear();
        particleSystem.Stop();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == obstacleLayer)
        {
            isDead = true;
            FreezeGame();
            gameOverObject.SetActive(true);
            diyingSound.Play();


            int totalPoints = logicObject.GetComponent<LogicManagerScript>().countableTotalMiliSeconds;

            if (totalPoints > playerHighScore)
            {
                PlayerPrefs.SetInt("highScore", totalPoints);
                playerHighScore = totalPoints;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //in the ground
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = true;
            isJumping = false;
        }

        //diying
        if (collision.gameObject.layer == obstacleLayer)
        {
            isDead = true;
            FreezeGame();
            gameOverObject.SetActive(true);
            diyingSound.Play();


            int totalPoints = logicObject.GetComponent<LogicManagerScript>().countableTotalMiliSeconds;

            if (totalPoints > playerHighScore)
            {
                PlayerPrefs.SetInt("highScore", totalPoints);
                playerHighScore = totalPoints;
            }
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = false;
        }
    }
}
