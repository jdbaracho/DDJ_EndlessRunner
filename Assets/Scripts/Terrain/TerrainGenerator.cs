using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private float changeRate;
    [SerializeField] private float speed;
    [SerializeField] private float speedRatio;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject cameraUpdate;
    [SerializeField] private GameObject generator;
    [SerializeField] private GameObject destructor;
    [SerializeField] private GameObject[] terrains;
    private List<GameObject> activeTerrain;
    private int terrainIndex;
    private float nextTerrainChange;
    private Vector3 cameraDefaultPosition;
    private Vector3 cameraNextPosition;

    // Start is called before the first frame update
    void Start()
    {
        nextTerrainChange = 0f;

        var terrain = Instantiate(terrains[0], transform.position, transform.rotation);
        terrain.GetComponent<TerrainMovement>().Initialize(this.gameObject, destructor, cameraUpdate, speed);

        cameraDefaultPosition = camera.transform.position;
        camera.transform.position += terrain.GetComponent<TerrainMovement>().GetCameraOffset();
        cameraNextPosition = camera.transform.position;
        
        Instantiate(terrains[0], generator.transform.position, transform.rotation)
        .GetComponent<TerrainMovement>().Initialize(this.gameObject, destructor, cameraUpdate, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTerrainChange)
        {
            terrainIndex++;
            if (terrainIndex >= terrains.Length)
            {
                terrainIndex = 0;
            }

            nextTerrainChange = Time.time + 1f / changeRate;
        }

        camera.transform.position = Vector3.MoveTowards(camera.transform.position, cameraNextPosition, speed/speedRatio * Time.deltaTime);

    }

    public void GenerateTerrain()
    {
        Instantiate(terrains[terrainIndex], generator.transform.position, transform.rotation)
        .GetComponent<TerrainMovement>().Initialize(this.gameObject, destructor, cameraUpdate, speed);
    }

    public void UpdateCamera(Vector3 offset)
    {
        cameraNextPosition = cameraDefaultPosition + offset;
        Debug.Log(cameraNextPosition);
    }
}
