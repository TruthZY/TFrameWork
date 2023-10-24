using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{

}

public class EventInfo<T>:IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventInfo : IEventInfo {
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }

}


public class EventCenter : BaseManager<EventCenter>
{
    //key �¼����� value �����¼���Ӧ������ί��
    private Dictionary<string,IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    //����¼�����
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions += action;    
        }
        else
        {
            eventDic.Add(name,new EventInfo<T>(action));
        }
    }
    //����
    public void AddEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo(action));
        }
    }
    //�Ƴ��¼�����
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
    }

    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }
    }
    //�¼�����
    public void EventTrigger<T>(string name, T info)
    {
        if (eventDic.ContainsKey(name))
        {
            if((eventDic[name] as EventInfo<T>).actions!=null)
            (eventDic[name] as EventInfo<T>).actions.Invoke(info);
        }
    }
    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo).actions != null)
                (eventDic[name] as EventInfo).actions.Invoke();
        }
    }
    //����¼�
    public void Clear()
    {
        eventDic.Clear();
    }
    /* ����
     * EventCenter.GetInstance().AddEventListener("do", Dosomething);//���β�
     * EventCenter.GetInstance().AddEventListener<GameObject>("do", DosomethingObj);
     * ����
     * EventCenter.GetInstance().EventTrigger("do");//���β�
     * EventCenter.GetInstance().EventTrigger<GameObject>("do",Null);
     * ����
     * void Dosomething()
     * void DosomethingObje(GameObject obj)
     */
}
