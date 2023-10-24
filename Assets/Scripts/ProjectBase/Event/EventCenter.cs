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
    //key 事件名字 value 监听事件对应函数的委托
    private Dictionary<string,IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    //添加事件监听
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
    //重载
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
    //移除事件监听
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
    //事件触发
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
    //清空事件
    public void Clear()
    {
        eventDic.Clear();
    }
    /* 监听
     * EventCenter.GetInstance().AddEventListener("do", Dosomething);//无形参
     * EventCenter.GetInstance().AddEventListener<GameObject>("do", DosomethingObj);
     * 触发
     * EventCenter.GetInstance().EventTrigger("do");//无形参
     * EventCenter.GetInstance().EventTrigger<GameObject>("do",Null);
     * 接受
     * void Dosomething()
     * void DosomethingObje(GameObject obj)
     */
}
