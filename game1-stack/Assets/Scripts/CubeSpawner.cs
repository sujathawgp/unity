using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCubeScript CubePrefab;
    [SerializeField]
    private MoveDirection moveDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, CubePrefab.transform.localScale);
    }

    public void SpawnCube()
    {
        var Cube = Instantiate(CubePrefab);

        if(MovingCubeScript.LastCube != null 
            && MovingCubeScript.LastCube.gameObject != GameObject.Find("StartCube"))
        {

            float x = moveDir == MoveDirection.X ? transform.position.x : MovingCubeScript.LastCube.transform.position.x;
            float z = moveDir == MoveDirection.Z ? transform.position.z : MovingCubeScript.LastCube.transform.position.z;

            Cube.transform.position = new Vector3(x
                , MovingCubeScript.LastCube.transform.position.y + CubePrefab.transform.localScale.y
                , z);
        }
        else
        {
            Cube.transform.position = transform.position;
        }
        Cube.transform.localScale = new Vector3(1f, 0.1f, 1f);
        Cube.GetComponent<MovingCubeScript>().MoveDir = moveDir;
    }


}
