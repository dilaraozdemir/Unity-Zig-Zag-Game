using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAutoMove : MonoBehaviour
{

    public Transform[] waypoints;
    private Vector3[] waypointsPositions;

    // Start is called before the first frame update
    void Start()
    {
        waypointsPositions = new Vector3[waypoints.Length];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypointsPositions[i] = waypoints[i].position;
        }
        StartCoroutine(MoveToWayPoints(waypointsPositions, 3));

    }
    IEnumerator move(Vector3 targetPosition, float moveSpeed)
    {
        while (targetPosition != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime); //moveSpeed*Time.deltaTime = daha smooth bir hareket saðlayacak. 
            yield return null;
        }

    }

    IEnumerator MoveToWayPoints(Vector3[] waypoints, float moveSpeed)
    {
        foreach (var waypoint in waypoints)
        {
            yield return move(waypoint, moveSpeed);
        }

        yield return MoveToWayPoints(waypoints, moveSpeed);
    }

    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
