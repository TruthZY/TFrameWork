using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class Sequence : Node
    {
        public Sequence() : base(){ }
        public Sequence(List<Node> children): base(children) { }

        public override NodeState Evalute()
        {
            bool myChildIsRunning = false;
            foreach(Node node in children) {
                switch (node.Evalute())
                {
                    case NodeState.FALIURE:
                        state = NodeState.FALIURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        myChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }

            }
            state = myChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }

    }
}



