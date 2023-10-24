using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMgr : BaseManager<InputMgr>
{
    private bool isUpdate = false;
    private List<KeyCode> keys = new List<KeyCode>();
    public InputMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(OnUpdate);
        //AddKey(KeyCode.Escape);
    }
    public void SetUpdate(bool state)
    {
        isUpdate = state;
    }
    public void AddKey(KeyCode key)
    {
        if (!keys.Contains(key))
        {
            keys.Add(key);
        }
    }
    private void OnUpdate()
    {
        if (!isUpdate) return;
        foreach(KeyCode key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                EventCenter.GetInstance().EventTrigger(key.ToString()+"Down");
            }
        }
        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyUp(key))
            {
                EventCenter.GetInstance().EventTrigger(key.ToString() + "Up");
            }
        }
    }
    /*
        InputMgr.GetInstance().SetUpdate(true);设置开启
        InputMgr.GetInstance().AddKey(KeyCode.Mouse0);增加按键
        EventCenter.GetInstance().AddEventListener(KeyCode.Mouse0 + "Down", fun);增加监听
     
     
     */
}
