using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum UILayer
{
    Bot,
    Mid,
    Top,
    System
}

/// <summary>
/// UI管理器
/// 1.管理所有显示的面板
/// 2.提供给外部显示和隐藏等接口
/// </summary>
public class UIMgr : BaseManager<UIMgr>
{
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    private Transform bot, mid, top, system;
    public RectTransform canvas;
    public UIMgr()
    {
        GameObject obj =  ResourcesMgr.GetInstance().Load<GameObject>("UI/Canvas");
        canvas = obj.transform as RectTransform;

        bot = canvas.Find("Bot");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        system = canvas.Find("System");

        GameObject.DontDestroyOnLoad(obj);
        obj = ResourcesMgr.GetInstance().Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
    }
    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T">面板脚本类型</typeparam>
    /// <param name="panelName">面板名</param>
    /// <param name="layer">显示在哪一层</param>
    /// <param name="callback">当面板预设体创建成功后做的事</param>
    public void ShowPanel<T>(string panelName, UILayer layer = UILayer.Mid, UnityAction<T> callback=null ) where T:BasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].ShowMe();
            if(callback!=null)
                callback(panelDic[panelName] as T);
            //如果存在该面板 避免面板重复加载
            return;
        }

        ResourcesMgr.GetInstance().LoadAsync<GameObject>("UI/" + panelName, (obj) =>
        {
            //设置Canvas子对象和相对位置
            Transform father = bot;
            switch (layer)
            {
                case UILayer.Mid:
                    father = mid;
                    break;
                case UILayer.Top:
                    father = top;
                    break;
                case UILayer.System:
                    father = system;
                    break;
            }
            obj.transform.SetParent(father);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;
            //得到预制体面板
            T panel = obj.GetComponent<T>();
            //存储面板
            if(callback != null)    callback(panel);

            panelDic.Add(panelName,panel);
        });
    }
    public void HidePanel(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
        {
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        } 
    }
    public T GetPanel<T>(string panelName)where T:BasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        else return null;
    }
    public Transform GetLayerFather(UILayer layer)
    {
        switch (layer)
        {
            case UILayer.Bot:
                return this.bot;
            case UILayer.Mid:
                return this.mid;
            case UILayer.Top:
                return this.top;
            case UILayer.System:
                return this.system;
            default:
                return null;
        }
    }
}
