using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{

    public GameObject obstacleCactus1;
    public GameObject obstacleCactus2;
    public GameObject obstacleCactus3;
    public GameObject obstacleCactus4;
    public GameObject obstacleCactus5;

    public void SpawnObstacle(int moveSpeed, float sizeProportion)
    {

        obstacleCactus1.GetComponent<ObstacleMovementScript>().moveSpeed = moveSpeed;

        Vector3 currentScale = obstacleCactus1.transform.localScale;
        Vector3 currentSpawnerPosition = transform.position;

        transform.position = new Vector3(currentSpawnerPosition.x, currentSpawnerPosition.y + ((sizeProportion - 1) * 0.30f), currentSpawnerPosition.z);

        obstacleCactus1.transform.localScale = new Vector3(currentScale.x * sizeProportion, currentScale.y * sizeProportion, currentScale.z);
        
        Instantiate(obstacleCactus1, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

        obstacleCactus1.transform.localScale = currentScale;
        transform.position = currentSpawnerPosition;
    }
}
