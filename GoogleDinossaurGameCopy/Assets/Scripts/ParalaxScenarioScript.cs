using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxScenarioScript : MonoBehaviour
{
    public Material material;
    public float distance;

    /* [Range(0f, 0.5f)] */
    public float updatableSpeed = 0.4f;

    public float initialSpeed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        updatableSpeed = initialSpeed;
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        distance += Time.deltaTime*updatableSpeed;
        material.mainTextureOffset = Vector2.right * distance;
    }
}
