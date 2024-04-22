using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class MyTree : MonoBehaviour
    {
        private Node _root = null;
        void Start()
        {
            _root = SetupTree();
        }

        // Update is called once per frame
        void Update()
        {
            if (_root != null) _root.Evalute();
        }
        protected abstract Node SetupTree();
    }


}
