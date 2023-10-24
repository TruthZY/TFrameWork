using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesMgr:BaseManager<ResourcesMgr>
{
    //ͬ��
    public T Load<T>(string name)where T : Object
    {
        T res = null;
        res = Resources.Load<T>(name);
        //�������Ϊgameobject ʵ�������ٷ��� �ⲿֱ��ʹ��
        if(res is GameObject)
        {
            return GameObject.Instantiate(res);
        }
        else// TextAsset AudioClip�ȵ�
            return res;
    }
    /// <summary>
    /// �첽
    /// </summary>
    /// <typeparam name="T">����</typeparam>
    /// <param name="name">��Դ����</param>
    /// <param name="callback">����</param>
    public void LoadAsync<T>(string name,UnityAction<T> callback) where T : Object
    {
        //�����첽����Э��
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
