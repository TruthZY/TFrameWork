using BehaviorTree;
using UnityEngine;

public class CheckEnemyInFovRange : Node
{
    private Transform transform;

    private static int _enemyLayerMask = 1 << 6;

    public CheckEnemyInFovRange(Transform transform)
    {
        this.transform = transform;
    }

    public override NodeState Evalute()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                transform.position, GuardBT.fovRange, _enemyLayerMask
                );
            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);

                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FALIURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }

}
