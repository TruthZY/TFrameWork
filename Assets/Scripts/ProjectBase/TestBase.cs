using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class TestBase : MonoBehaviour
{
    private void Start()
    {
        UIMgr.GetInstance().ShowPanel<TestPanel>("TestPanel",UILayer.Mid, tttt);
    }
    private void Update()
    {

    }
    private void tttt(TestPanel t)
    {
        t.GetComponentInChildren<Text>().text = "sdfsdf";
        Invoke("Hide", 2);
    }
    void Hide()
    {
        UIMgr.GetInstance().HidePanel("TestPanel");
    }


}
