using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll2d : MonoBehaviour
{
    Material material;
    
    private Vector2 offset;
    [SerializeField]
    private float xVelocity, yVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    void Start()
    {
        offset = new Vector2(xVelocity, yVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
