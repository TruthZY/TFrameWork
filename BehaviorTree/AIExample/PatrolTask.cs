using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class PatrolTask : Node
{
    private Transform transform;
    private Transform[] wayPoints;

    private int currentWayPointIndex = 0;
    private float waitTime = 1;
    private float waitCounter = 0;
    private bool isWaiting = false;

    public PatrolTask(Transform transform,Transform[] waypoints)
    {
        this.transform = transform;
        this.wayPoints = waypoints;
    }

    public override NodeState Evalute()
    {
        if (isWaiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
                isWaiting = false;
        }
        else
        {
            Transform wp = wayPoints[currentWayPointIndex];
            if (Vector3.Distance(transform.position, wp.position) < 0.01f)
            {
                transform.position = wp.position;
                waitCounter = 0;
                isWaiting = true;
                currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Length;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, wp.position, GuardBT.speed * Time.deltaTime);
            }
        }
        state = NodeState.RUNNING;
        return state;
    }
}
