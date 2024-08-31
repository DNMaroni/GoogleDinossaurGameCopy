using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovementScript : MonoBehaviour
{
    public float moveSpeed = 5;
    public bool fly = false;
    public float deadZone = -10;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        
        if(transform.position.x < deadZone){
            Destroy(gameObject);
        }
    }
}
