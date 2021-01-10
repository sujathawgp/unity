using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCubeScript : MonoBehaviour
{
    [SerializeField]
    //private float moveSpeed = 1f;
    private float moveSpeed = 1f;
    [SerializeField]
    public MoveDirection MoveDir;

    public static MovingCubeScript CurrentCube { get; private set; }
    public static MovingCubeScript LastCube { get; private set; }

    void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("StartCube").GetComponent<MovingCubeScript>(); // String based. rename error. duplicate possibility
            LastCube.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f);
        }
        if (transform.name != "StartCube")
        {
            moveSpeed = 1f;
            CurrentCube = this;
            GetComponent<Renderer>().material.color = GetRandomColor();
        }
    }

    Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    internal void Stop()
    {
        if(moveSpeed > 0)
        {
            moveSpeed = 0;
            if (MoveDir == MoveDirection.Z)
            {
                float Hangover = transform.position.z - LastCube.transform.position.z;

                if (Hangover <= 1f && Hangover >= -1f)
                {
                    SplitCubeOnZ(Hangover, Hangover > 0 ? 1f : -1f);
                }
                else if (Mathf.Abs(Hangover) >= LastCube.transform.localScale.z)
                {
                    LastCube = null;
                    CurrentCube = null;
                    SceneManager.LoadScene(0);

                }

            }
            else
            {
                float Hangover = transform.position.x - LastCube.transform.position.x;

                if (Hangover <= 1f && Hangover >= -1f)
                {
                    SplitCubeOnX(Hangover, Hangover > 0 ? 1f : -1f);
                }
                else if (Mathf.Abs(Hangover) >= LastCube.transform.localScale.x)
                {
                    LastCube = null;
                    CurrentCube = null;
                    SceneManager.LoadScene(0);

                }
            }

            LastCube = this;


        }
        
    }

    void SplitCubeOnX(float Hangover, float direction)
    {
        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(Hangover);
        float fallingBlockXSize = transform.localScale.x - newXSize;

        float newXPos = LastCube.transform.position.x + (Hangover / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);

        float cubeEdgeX = transform.position.x + (newXSize / 2 * direction);
        float fallingBlockXPos = cubeEdgeX + (fallingBlockXSize / 2f * direction);
        SpawnDropCubeX(fallingBlockXPos, fallingBlockXSize);
    }

    void SplitCubeOnZ(float Hangover, float direction)
    {
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(Hangover);
        float fallingBlockZSize = transform.localScale.z - newZSize;

        float newZPos = LastCube.transform.position.z + (Hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPos);

        float cubeEdgeZ = transform.position.z + (newZSize / 2 * direction);
        float fallingBlockZPos = cubeEdgeZ + (fallingBlockZSize / 2f * direction);
        SpawnDropCube(fallingBlockZPos, fallingBlockZSize);
    }

    void SpawnDropCubeX(float xPos, float xSize)
    {
        var Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Cube.transform.localScale = new Vector3(xSize, transform.localScale.y, transform.localScale.z);
        Cube.transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        Cube.transform.name = "FallingCubeX";
        Cube.AddComponent<Rigidbody>();
        Cube.GetComponent<Renderer>().material.color = new Color(0.6f, 0f, 0f);
        Destroy(Cube.gameObject, 1f);
    }

    void SpawnDropCube(float zPos, float zSize)
    {
        var Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, zSize);
        Cube.transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
        Cube.transform.name = "FallingCube";
        Cube.AddComponent<Rigidbody>();
        Cube.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f);
        Destroy(Cube.gameObject, 1f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (MoveDir == MoveDirection.Z)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }
        else
        {
            transform.position += transform.right * Time.deltaTime * moveSpeed;
        }

    }
}

