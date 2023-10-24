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
/// UI������
/// 1.����������ʾ�����
/// 2.�ṩ���ⲿ��ʾ�����صȽӿ�
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
    /// ��ʾ���
    /// </summary>
    /// <typeparam name="T">���ű�����</typeparam>
    /// <param name="panelName">�����</param>
    /// <param name="layer">��ʾ����һ��</param>
    /// <param name="callback">�����Ԥ���崴���ɹ���������</param>
    public void ShowPanel<T>(string panelName, UILayer layer = UILayer.Mid, UnityAction<T> callback=null ) where T:BasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].ShowMe();
            if(callback!=null)
                callback(panelDic[panelName] as T);
            //������ڸ���� ��������ظ�����
            return;
        }

        ResourcesMgr.GetInstance().LoadAsync<GameObject>("UI/" + panelName, (obj) =>
        {
            //����Canvas�Ӷ�������λ��
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
            //�õ�Ԥ�������
            T panel = obj.GetComponent<T>();
            //�洢���
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
