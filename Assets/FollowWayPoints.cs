using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWayPoints : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private float moveSpeed = 2f;

    private int waypointIndex = 0;

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position; 

    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
         waypoints[waypointIndex].transform.position,
         moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            if (waypointIndex + 1 < waypoints.Length)
            {
                waypointIndex += 1;
            }
            else
            {
                waypointIndex = 0;
            }
            
        }
    }


}
