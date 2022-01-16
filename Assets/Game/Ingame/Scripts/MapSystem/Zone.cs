using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private int size;
    public Zone(int size)
    {
        this.size = size;
    }

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
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3((float)size / 2, (float)size / 2), new Vector3((float)size, (float)size, 1));
        //Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3((float)size, (float)size, 0));
    }
}
