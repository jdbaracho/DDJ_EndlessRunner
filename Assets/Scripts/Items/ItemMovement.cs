using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    private float speed;
    private float speedIncreaseRate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        speed += speedIncreaseRate * Time.deltaTime;
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    public void Initialize(float speed, float speedIncreaseRate)
    {
        this.speed = speed;
        this.speedIncreaseRate = speedIncreaseRate;
    }
}
