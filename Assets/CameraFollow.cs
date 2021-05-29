using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 diffVector = Vector3.zero;
    Transform player { get { return FindObjectOfType<PlayerController>().transform; } }
    
    // Start is called before the first frame update
    void Start()
    {
        diffVector = player.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position - diffVector;
    }
}
