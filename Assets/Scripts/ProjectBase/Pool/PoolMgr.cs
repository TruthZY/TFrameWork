using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PoolData
{
    public GameObject fatherObj;
    public List<GameObject> poolList;
    public PoolData(GameObject obj, GameObject poolObj)
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>();
        PushObj(obj);
    }

    public void PushObj(GameObject obj)
    {
        poolList.Add(obj);
        obj.transform.parent = fatherObj.transform;
        obj.SetActive(false);
    }

    public GameObject GetObj()
    {
        GameObject obj;
        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.SetActive(true);
        obj.transform.parent = null;
        return obj;
    }
}

public class PoolMgr : BaseManager<PoolMgr>
{
    public Dictionary<string, PoolData> dic = new Dictionary<string, PoolData>();//����һ���ֵ�
    private GameObject poolObj;//Scene�����еġ�pool�������ɺ�
    public GameObject GetObj(string name)//����һ��Ԥ���壬    
    {
        GameObject obj;
        if (dic.ContainsKey(name) && dic[name].poolList.Count > 0)//����ֵ������������������������������0��ȡ����
        {
            obj = dic[name].GetObj();
        }
        else
        {
            ResourcesMgr.GetInstance().LoadAsync<GameObject>(name, (o) =>
            {
                o.name = name;
            });
            obj = Object.Instantiate(Resources.Load(name)) as GameObject;//û�оͶ�ȡһ�������ô����ֵ�
            obj.name = name;
        }
        return obj;
    }

    /// <summary>
    /// �첽
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    public void GetObj(string name,UnityAction<GameObject> callback)//����һ��Ԥ���壬    
    {
        if (dic.ContainsKey(name) && dic[name].poolList.Count > 0)//����ֵ������������������������������0��ȡ����
        {
            callback(dic[name].GetObj());
        }
        else
        {
            ResourcesMgr.GetInstance().LoadAsync<GameObject>(name, (o) =>
            {
                o.name = name;
                callback(o);
            });

        }
    }


    public GameObject PushObj(string name ,GameObject obj)//���ص�
    {
        if (obj.activeSelf)
        {
            if (poolObj == null)
            {
                poolObj = new GameObject("pool");
            }
            name = obj.name;
            if (dic.ContainsKey(name))//����ҵ����������
            {
                dic[name].PushObj(obj);
            }
            else
            {
                dic.Add(name, new PoolData(obj, poolObj) { });
            }
        }
        return obj;
    }
    public void Clear()//�л�����ʱ�����л������
    {
        dic.Clear();
        poolObj = null;
    }
}

