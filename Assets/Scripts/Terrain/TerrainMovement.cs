using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovement : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraUpdateOffset;
    [SerializeField] private Vector3 itemLocation;
    private GameObject terrainGenerator;
    private GameObject update;
    private GameObject destructor;
    private GameObject item;
    private float speed;
    private float speedIncreaseRate;
    private bool canCreate;

    // Start is called before the first frame update
    void Start()
    {
        canCreate = true;
    }

    // Update is called once per frame
    void Update()
    {
        speed += speedIncreaseRate * Time.deltaTime;

        if (Vector2.Distance(destructor.transform.position, transform.position) < 0.1f)
        {
            Destroy(this.gameObject);
        }

        if (canCreate && Vector2.Distance(update.transform.position, transform.position) < 0.1f)
        {
            canCreate = false;
            terrainGenerator.GetComponent<TerrainGenerator>().GenerateTerrain(gameObject.CompareTag("Transition"));
        }

        transform.position = Vector2.MoveTowards(transform.position, destructor.transform.position, speed * Time.deltaTime);

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

    public Vector3 GetCameraOffset()
    {
        return cameraOffset;
    }

    public Vector3 GetItemLocation()
    {
        return itemLocation;
    }
}
