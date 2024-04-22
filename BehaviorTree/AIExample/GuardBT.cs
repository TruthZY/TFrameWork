using UnityEngine;
using BehaviorTree;
using System.Collections.Generic;

public class GuardBT : MyTree
{
    public Transform[] wayPoints;
    public static float speed = 2f;
    public static float fovRange = 6f;

    protected override Node SetupTree()
    {
        //selector will choose the first can do
        Node root = new Selector(new List<Node>
        {
            /* 
             new Sequence(new List<Node>{

                new CheckEnemyInAttackRange(transofrm),
                new Attack(transform),
            }),
             */
            //sequence will do one by one until can not do
            new Sequence(new List<Node>
            {
                new CheckEnemyInFovRange(transform),
                new TaskGoToTarget(transform),
            }),
            new PatrolTask(transform,wayPoints)

        });
        return root;
    }
}
