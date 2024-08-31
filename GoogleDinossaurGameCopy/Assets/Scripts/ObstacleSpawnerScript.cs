using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    public GameObject[] obstacles;
    public float desiredYPosition = -1.9f;

    public void SpawnObstacle(float sizeProportion, int obstacleIndex, float moveSpeed = 5)
    {  
        float objectHeight = obstacles[obstacleIndex].GetComponent<Renderer>().bounds.size.y;
        float objectWidth = obstacles[obstacleIndex].GetComponent<Renderer>().bounds.size.x;


        ObstacleMovementScript obstacleMovementScript = obstacles[obstacleIndex].GetComponent<ObstacleMovementScript>();

        float newYPosition = desiredYPosition + (objectHeight / 2);

        if(obstacleMovementScript.fly){

            float[] birdPossiblePositions = new float[4]{-1.59f, -0.17f, 0.89f, -0.62f};

            sizeProportion = 1;

            newYPosition = birdPossiblePositions[Random.Range(0, 4)];

        }

        obstacleMovementScript.moveSpeed = moveSpeed;

        Vector3 currentScale = obstacles[obstacleIndex].transform.localScale;
        Vector3 currentSpawnerPosition = transform.position;


        //if the object width is higher than 0.9, lets decrease the size proportion
        if(objectWidth > 0.9f){
            sizeProportion -= ((sizeProportion * 20) / 100);
        }

        transform.position = new Vector3(currentSpawnerPosition.x, newYPosition, currentSpawnerPosition.z);

        obstacles[obstacleIndex].transform.localScale = new Vector3(currentScale.x * sizeProportion, currentScale.y * sizeProportion, currentScale.z);
        
        Instantiate(obstacles[obstacleIndex], new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

        obstacles[obstacleIndex].transform.localScale = currentScale;
        transform.position = currentSpawnerPosition;
    }
}
