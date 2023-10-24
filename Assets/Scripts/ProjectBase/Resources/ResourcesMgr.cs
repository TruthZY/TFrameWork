using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesMgr:BaseManager<ResourcesMgr>
{
    //同步
    public T Load<T>(string name)where T : Object
    {
        T res = null;
        res = Resources.Load<T>(name);
        //如果对象为gameobject 实例化后再返回 外部直接使用
        if(res is GameObject)
        {
            return GameObject.Instantiate(res);
        }
        else// TextAsset AudioClip等等
            return res;
    }
    /// <summary>
    /// 异步
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="name">资源名字</param>
    /// <param name="callback">函数</param>
    public void LoadAsync<T>(string name,UnityAction<T> callback) where T : Object
    {
        //开启异步加载协程
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadAsync<T>(name,callback));

    }
    private IEnumerator ReallyLoadAsync<T>(string name,UnityAction<T> callback) where T:Object
    {
        ResourceRequest r =  Resources.LoadAsync<T>(name);
        yield return r;
        if(r.asset is GameObject)
        {
            callback(GameObject.Instantiate(r.asset) as T);
        }
        else
        {
            callback(r.asset as T);
        }


    }
}
