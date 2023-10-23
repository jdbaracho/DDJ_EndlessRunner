using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovement : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraUpdateOffset;
    [SerializeField] private Vector3 itemLocation;
    [SerializeField] private float obstacleMinX;
    [SerializeField] private float obstacleMaxX;
    [SerializeField] private float obstacleMinY;
    [SerializeField] private float obstacleMaxY;
    private GameObject terrainGenerator;
    private GameObject update;
    private GameObject destructor;
    private GameObject item;
    private float speed;
    private float speedIncreaseRate;
    private bool canCreate;
    private System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        canCreate = true;
        random = new System.Random();
        CreateObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        speed += speedIncreaseRate * Time.deltaTime;

        if (transform.position.x <= destructor.transform.position.x)
        {
            Destroy(this.gameObject);
        }

        if (canCreate && transform.position.x <= update.transform.position.x)
        {
            canCreate = false;
            terrainGenerator.GetComponent<TerrainGenerator>().GenerateTerrain(gameObject.CompareTag("Transition"));
        }

        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);

        if (Vector2.Distance(update.transform.position + cameraUpdateOffset, transform.position) < 0.1f)
        {
            terrainGenerator.GetComponent<TerrainGenerator>().UpdateCamera(cameraOffset);
        }
    }

    public void Initialize(GameObject terrainGenerator, GameObject destructor, GameObject update, float speed, float speedIncreaseRate)
    {
        this.terrainGenerator = terrainGenerator;
        this.destructor = destructor;
        this.update = update;
        this.speed = speed;
        this.speedIncreaseRate = speedIncreaseRate;
    }

    void CreateObstacle()
    {
        if (transform.childCount <= 0 || !transform.GetChild(0).gameObject.CompareTag("Obstacle")) return;

        var x = random.NextDouble();
        x = (x * (obstacleMaxX - obstacleMinX)) + obstacleMinX;
        var y = random.NextDouble();
        y = (y * (obstacleMaxY - obstacleMinY)) + obstacleMinY;
        transform.GetChild(0).gameObject.transform.position = transform.position + new Vector3((float)x, (float)y, transform.position.z);
    }

    public Vector3 GetCameraOffset()
    {
        return cameraOffset;
    }

    public Vector3 GetItemLocation()
    {
        return itemLocation;
    }
}
