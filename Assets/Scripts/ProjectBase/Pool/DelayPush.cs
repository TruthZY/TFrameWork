using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DelayPush : MonoBehaviour
{
    public float RelayTime = 2;
    void OnEnable()
    {
        Invoke("Return", RelayTime);
    }

    void Return()
    {
        PoolMgr.GetInstance().PushObj(this.gameObject.name, this.gameObject);
    }
}
