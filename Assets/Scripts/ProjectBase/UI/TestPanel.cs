using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : BasePanel
{
    private void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        //GetControl<Button>("btnStart").onClick.AddListener(ClickStart);
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //显示面板时执行的逻辑
    }
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "btnStart":
                Debug.Log("start");
                break;
        }
    }

}
