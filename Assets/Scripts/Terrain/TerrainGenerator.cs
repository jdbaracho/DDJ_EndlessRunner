using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    enum Terrain
    {
        Forest,
        Ocean,
        Desert
    }
    enum Transition
    {
        None = -1,
        Forest_Ocean,
        Forest_Desert,
        Ocean_Forest,
        Ocean_Desert,
        Desert_Forest,
        Desert_Ocean
    }
    [SerializeField] private float changeRate;
    [SerializeField] private float speed;
    [SerializeField] private float speedIncreaseRate;
    [SerializeField] private float speedRatio;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject update;
    [SerializeField] private GameObject generator;
    [SerializeField] private GameObject destructor;
    [SerializeField] private Vector3 itemSpawn;
    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject[] terrains;
    [SerializeField] private GameObject[] transitions;
    [SerializeField] bool isTransitioning;
    GameObject item;
    GameObject terrain;
    float itemTimer;
    Vector3 cameraDefaultPosition;
    Vector3 cameraNextPosition;
    Terrain currentTerrain;
    Terrain nextTerrain;
    Transition transition;
    System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        itemTimer = 0.0f;
        isTransitioning = false;
        currentTerrain = Terrain.Forest;
        transition = Transition.None;
        random = new System.Random();

        terrain = Instantiate(terrains[(int)currentTerrain], transform.position, transform.rotation);
        terrain.GetComponent<TerrainMovement>().Initialize(this.gameObject, destructor, update, speed, speedIncreaseRate);
        terrain.transform.GetChild(0).gameObject.SetActive(false);

        cameraDefaultPosition = camera.transform.position;
        camera.transform.position += terrain.GetComponent<TerrainMovement>().GetCameraOffset();
        cameraNextPosition = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        speed += speedIncreaseRate * Time.deltaTime;

        if (!isTransitioning)
        {
            itemTimer += Time.deltaTime;
        }

        if (itemTimer >= 10.0f)
        {
            itemTimer = 0.0f;
            nextTerrain = RandomTerrain();

            var position = transform.position + terrains[(int)currentTerrain].GetComponent<TerrainMovement>().GetItemLocation() + itemSpawn;
            Destroy(item);
            item = Instantiate(items[(int)nextTerrain], position, transform.rotation);
            item.GetComponent<ItemMovement>().Initialize(speed, speedIncreaseRate);
        }

        camera.transform.position = Vector3.MoveTowards(camera.transform.position, cameraNextPosition, speed/speedRatio * Time.deltaTime);
    }

    Terrain RandomTerrain()
    {
        var v = Enum.GetValues(typeof(Terrain));
        var i = random.Next(terrains.Length);
        if (currentTerrain == (Terrain)v.GetValue(i))
        {
            i++;
            if (i >= terrains.Length)
            {
                i = 0;
            }
        }
        return (Terrain)v.GetValue(i);
    }

    Transition NextTransition()
    {
        if (currentTerrain == Terrain.Forest && nextTerrain == Terrain.Ocean)
            return Transition.Forest_Ocean;
        if (currentTerrain == Terrain.Forest && nextTerrain == Terrain.Desert)
            return Transition.Forest_Desert;
        if (currentTerrain == Terrain.Ocean && nextTerrain == Terrain.Forest)
            return Transition.Ocean_Forest;
        if (currentTerrain == Terrain.Ocean && nextTerrain == Terrain.Desert)
            return Transition.Ocean_Desert;
        if (currentTerrain == Terrain.Desert && nextTerrain == Terrain.Forest)
            return Transition.Desert_Forest;
        if (currentTerrain == Terrain.Desert && nextTerrain == Terrain.Ocean)
            return Transition.Desert_Ocean;
        
        return Transition.None;
    }

    public void GenerateTerrain(bool isTransition)
    {
        if(isTransition)
        {
            isTransitioning = false;
        }

        GameObject next;
        if (transition == Transition.None)
        {
            next = terrains[(int)currentTerrain];
        }
        else
        {
            next = transitions[(int)transition];
            transition = Transition.None;
        }
        terrain = Instantiate(next, generator.transform.position, transform.rotation);
        terrain.GetComponent<TerrainMovement>().Initialize(this.gameObject, destructor, update, speed, speedIncreaseRate);
    }

    public void UpdateCamera(Vector3 offset)
    {
        cameraNextPosition = cameraDefaultPosition + offset;
    }

    public void PickUpItem()
    {
        transition = NextTransition();
        currentTerrain = nextTerrain;
        isTransitioning = true;
    }
}
