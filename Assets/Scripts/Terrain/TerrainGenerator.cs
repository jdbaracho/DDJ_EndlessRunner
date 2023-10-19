using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private float changeRate;
    [SerializeField] private float speed;
    [SerializeField] private GameObject generator;
    [SerializeField] private GameObject destructor;
    [SerializeField] private GameObject[] terrains;
    private List<GameObject> activeTerrain;
    [SerializeField] private int terrainIndex;
    private float nextTerrainChange;

    // Start is called before the first frame update
    void Start()
    {
        nextTerrainChange = 0f;
        Instantiate(terrains[0], generator.transform.position / -2.0f, transform.rotation).GetComponent<TerrainMovement>().Initialize(this.gameObject, destructor, speed);
        Instantiate(terrains[0], transform.position, transform.rotation).GetComponent<TerrainMovement>().Initialize(this.gameObject, destructor, speed);
        Instantiate(terrains[0], generator.transform.position / 2.0f, transform.rotation).GetComponent<TerrainMovement>().Initialize(this.gameObject, destructor, speed);
        Instantiate(terrains[0], generator.transform.position, transform.rotation).GetComponent<TerrainMovement>().Initialize(this.gameObject, destructor, speed);
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
    }

    public void GenerateTerrain()
    {

        Instantiate(terrains[terrainIndex], generator.transform.position, transform.rotation).GetComponent<TerrainMovement>().Initialize(this.gameObject, destructor, speed);;
    }
}
