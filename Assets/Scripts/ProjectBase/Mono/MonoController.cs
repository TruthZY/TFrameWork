using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour
{
    public event UnityAction updateEvent;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent+=fun;
    }
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent-=fun;
    }

    private void Update()
    {
        if (updateEvent != null)
        {
            updateEvent.Invoke();
        }
    }

}
