using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskGoToTarget : Node
{
    private Transform transform;
    public TaskGoToTarget(Transform transform) {
        this.transform = transform;
    }
    public override NodeState Evalute()
    {
        Transform target = GetData("target") as Transform;
        if (Vector3.Distance(transform.position, target.position) > 0.01f) {

            transform.position = Vector3.MoveTowards(
                transform.position, target.position, GuardBT.speed * Time.deltaTime
                ) ;
            transform.LookAt(target.position);
        }
        state = NodeState.RUNNING;
        return state;


    }


}
