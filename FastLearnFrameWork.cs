using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;
using UnityEngine.Events;
using UnityEngine.Networking;

public class FastLearnFrameWork : SingletonMono<FastLearnFrameWork>
{
    public class PoolLearn:IPoolObject
    {
        public string name;

        public void ResetInfo()
        {
            name = "test";
        }
    }

    //快速学习新版框架示例
    void Start()
    {
        /*
        ///1. 单例模式更新内容 BaseManager 加入线程锁保证安全
        FastLearnFrameWork.GetInstance().SingletonLearn();
        ///2. Mono
        UnityAction a = () => { Debug.Log("2.MONO固定帧执行"); };
        MonoMgr.GetInstance().AddFixedUpdateListener(a);
        MonoMgr.GetInstance().StartCoroutine(MonoLean(a));
        ///3. 对象池 新加入PoolData 挂载在prefab上，用于设定初始对象池大小 支持非GO
        var g = PoolMgr.GetInstance().GetObj("Prefabs/Bullet/TeleportBulletEff");
        PoolMgr.GetInstance().PushObj(g);
        var s = PoolMgr.GetInstance().GetObj<FastLearnFrameWork.PoolLearn>();
        Debug.Log("3." + s.name);
        s.name = "3.bbb";
        Debug.Log("3." + s.name);
        PoolMgr.GetInstance().PushObj<FastLearnFrameWork.PoolLearn>(s);
        s = PoolMgr.GetInstance().GetObj<FastLearnFrameWork.PoolLearn>();
        Debug.Log("3."+s.name);

        ///4. EventCenter 需要自定义事件枚举，性能优于string，待支持旧的string类型
        EventCenter.GetInstance().AddEventListener<float>(E_EventType.E_Test, (val) => {
            Debug.Log(val);
        });
        EventCenter.GetInstance().EventTrigger<float>(E_EventType.E_Test, 333);
        ///5. Resource 添加字典存储已加载过的内容，防止大量重复加载
        ///加入计数引用，方便卸载资源
        ///仅用于固定资源，可以将非固定资源放入Editor中，使用ABResMgr加载(非实机调用EditorResMgr)
        var t = ResMgr.GetInstance().Load<Material>("Materials/Blue");
        Debug.Log("5.导入"+t.name);
        ResMgr.GetInstance().LoadAsync<Material>("Materials/Blue", (mat) => {
            Debug.Log("5.异步导入" + mat.name);
        });
        ResMgr.GetInstance().UnloadAsset<Material>("Materials/Blue");
        Debug.Log("5.引用计数"+ResMgr.GetInstance().GetRefCount<Material>("Materials/Blue"));
        ResMgr.GetInstance().UnloadUnusedAssets(() => {
            Debug.Log("5.清空无用资源后引用计数" + ResMgr.GetInstance().GetRefCount<Material>("Materials/Blue"));
        });
        ///6.ABResMgr 非实机调用EditorResMgr
        ABMgr.GetInstance().LoadResAsync<GameObject>("model", "Cube", (obj) =>
        {
            obj.transform.position = Vector3.up;
            Instantiate(obj);
        });
        ABMgr.GetInstance().LoadResAsync<Material>("material", "test", (obj) =>
        {
            //有坑，材质的shader需要重新赋值，或者alwaysInclude
            obj.shader = Shader.Find(obj.shader.name);
        });
        ABResMgr.GetInstance().LoadResAsync<GameObject>("ui", "BeginPanel", (res) =>
        {
            
            GameObject panelObj = GameObject.Instantiate(res, transform, false);

            //获取对应UI组件返回出去
            //T panel = panelObj.GetComponent<T>();
            //显示面板时执行的默认方法
            //panel.ShowMe();

        }, false);
        ///7.www WebRequest
        //WWW w3 = new WWW("file://.....");
        //w3.error
        //w3.assetBundle

        UWQResMgr.GetInstance().LoadRes<string>("file://" + Application.streamingAssetsPath + "/test.txt",(val)=>{
            Debug.Log("7.加载的文本"+val);
        }, () => {
            Debug.Log("7.Failed");
        });
        ///8. UIMgr 添加指定摄像机渲染
        UIMgr.GetInstance().ShowPanel<BeginPanel>(E_UILayer.Middle, (panel) => {
            panel.Test();
        });
        */
        ///9. MusicMgr
        //ABResMgr.isDebug = true;
        MusicMgr.GetInstance().PlaySound("hit_4");
        MusicMgr.GetInstance().PlayBKMusic("Begin");
        ///10.场景切换
        /*
        SceneMgr.GetInstance().LoadSceneAsyn("1", () =>
        {
            //黑屏
        });*/
        ///11.输入模块 暂时没必要
        ///InputMgr.GetInstance().StartOrCloseInputMgr(true);
        ///12.计时器模块
        int key = TimerMgr.GetInstance().CreateTimer(true, 5000, () =>
        {
            Debug.Log("12.Over");
        }, 1000, () => {
            Debug.Log("12.Tick");
        });


    }
    public void SingletonLearn()
    {
        Debug.Log("1.单例模式");
    }
    IEnumerator MonoLean(UnityAction a)
    {
        yield return new WaitForSeconds(2f);
        MonoMgr.GetInstance().RemoveFixedUpdateListener(a);
        Debug.Log("2.Mono");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
