using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovement : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private GameObject terrainGenerator;
    private GameObject destructor;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(destructor.transform.position + offset, transform.position) < 0.1f)
        {
            terrainGenerator.GetComponent<TerrainGenerator>().GenerateTerrain();
            Destroy(this.gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, destructor.transform.position + offset, speed * Time.deltaTime);
    }

    public void Initialize(GameObject terrainGenerator, GameObject destructor, float speed)
    {
        this.terrainGenerator = terrainGenerator;
        this.destructor = destructor;
        this.speed = speed;
    }
}
