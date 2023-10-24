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
    public Dictionary<string, PoolData> dic = new Dictionary<string, PoolData>();//生成一个字典
    private GameObject poolObj;//Scene窗口中的“pool”总收纳盒
    public GameObject GetObj(string name)//返回一个预制体，    
    {
        GameObject obj;
        if (dic.ContainsKey(name) && dic[name].poolList.Count > 0)//如果字典里有这个东西，并且它的数量大于0就取出来
        {
            obj = dic[name].GetObj();
        }
        else
        {
            ResourcesMgr.GetInstance().LoadAsync<GameObject>(name, (o) =>
            {
                o.name = name;
            });
            obj = Object.Instantiate(Resources.Load(name)) as GameObject;//没有就读取一个，不用存入字典
            obj.name = name;
        }
        return obj;
    }

    /// <summary>
    /// 异步
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    public void GetObj(string name,UnityAction<GameObject> callback)//返回一个预制体，    
    {
        if (dic.ContainsKey(name) && dic[name].poolList.Count > 0)//如果字典里有这个东西，并且它的数量大于0就取出来
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


    public GameObject PushObj(string name ,GameObject obj)//隐藏掉
    {
        if (obj.activeSelf)
        {
            if (poolObj == null)
            {
                poolObj = new GameObject("pool");
            }
            name = obj.name;
            if (dic.ContainsKey(name))//如果找到了这个名字
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
    public void Clear()//切换场景时将所有缓存清除
    {
        dic.Clear();
        poolObj = null;
    }
}

