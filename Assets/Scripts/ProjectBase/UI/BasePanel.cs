using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    protected virtual void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<Slider>();
        FindChildrenControl<InputField>();
    }
    /// <summary>
    /// 显示自己
    /// </summary>
    public virtual void ShowMe() { }
    /// <summary>
    /// 隐藏自己
    /// </summary>
    public virtual void HideMe() { }

    protected virtual void OnClick(string name) {
        switch (name)
        {
            case "Test":
                break;
        }
    }
    protected virtual void OnValueChange(string name, bool value)
    {
        switch (name)
        {
            case "Test":
                break;
        }
    }
    protected T GetControl<T>(string controlName) where T : UIBehaviour
    {
        if (controlDic.ContainsKey(controlName))
        {
            for(int i = 0; i < controlDic[controlName].Count; i++)
            {
                if(controlDic[controlName][i] is T)
                    return controlDic[controlName][i] as T;
            }
        }
        return null;
    }

    /// <summary>
    /// 找到子对象对应控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name=""></param>
    private void FindChildrenControl<T>() where T:UIBehaviour
    {
        T[] controls = GetComponentsInChildren<T>();
        foreach (T t in controls)
        {
            string objName = t.gameObject.name;
            if (controlDic.ContainsKey(objName))
            {
                controlDic[objName].Add(t);
            }
            else
            {
                controlDic.Add(t.gameObject.name, new List<UIBehaviour> { t});
            }

            if( t is Button) { (t as Button).onClick.AddListener(() =>
            {
                OnClick(objName);
            }); }
            else if (t is Toggle)
            {
                (t as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnValueChange(objName, value);
                });
            }
        }
    }


}
