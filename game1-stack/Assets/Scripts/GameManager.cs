using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private CubeSpawner[] spawners;
    private int spawnerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    public static event Action OnCubeSpawned = delegate {};
       
    private void Awake()
    {
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    // Update is called once per frame
    private void Update() // private by default
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (MovingCubeScript.CurrentCube != null)
            {
                MovingCubeScript.CurrentCube.Stop();
            }
            spawnerIndex = (spawnerIndex + 1) % spawners.Length;
            CubeSpawner currentSpawner = spawners[spawnerIndex];
            currentSpawner.SpawnCube();
            OnCubeSpawned();
        }
    }
}
