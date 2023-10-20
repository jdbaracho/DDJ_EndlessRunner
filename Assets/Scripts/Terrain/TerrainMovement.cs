using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovement : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    private GameObject terrainGenerator;
    private GameObject cameraUpdate;
    private GameObject destructor;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(destructor.transform.position, transform.position) < 0.1f)
        {
            terrainGenerator.GetComponent<TerrainGenerator>().GenerateTerrain();
            Destroy(this.gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, destructor.transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(cameraUpdate.transform.position, transform.position) < 0.1f)
        {
            terrainGenerator.GetComponent<TerrainGenerator>().UpdateCamera(cameraOffset);
        }
    }

    public void Initialize(GameObject terrainGenerator, GameObject destructor, GameObject cameraUpdate, float speed)
    {
        this.terrainGenerator = terrainGenerator;
        this.destructor = destructor;
        this.cameraUpdate = cameraUpdate;
        this.speed = speed;
    }

    public Vector3 GetCameraOffset()
    {
        return cameraOffset;
    }
}
