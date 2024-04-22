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

    //����ѧϰ�°���ʾ��
    void Start()
    {
        /*
        ///1. ����ģʽ�������� BaseManager �����߳�����֤��ȫ
        FastLearnFrameWork.GetInstance().SingletonLearn();
        ///2. Mono
        UnityAction a = () => { Debug.Log("2.MONO�̶�ִ֡��"); };
        MonoMgr.GetInstance().AddFixedUpdateListener(a);
        MonoMgr.GetInstance().StartCoroutine(MonoLean(a));
        ///3. ����� �¼���PoolData ������prefab�ϣ������趨��ʼ����ش�С ֧�ַ�GO
        var g = PoolMgr.GetInstance().GetObj("Prefabs/Bullet/TeleportBulletEff");
        PoolMgr.GetInstance().PushObj(g);
        var s = PoolMgr.GetInstance().GetObj<FastLearnFrameWork.PoolLearn>();
        Debug.Log("3." + s.name);
        s.name = "3.bbb";
        Debug.Log("3." + s.name);
        PoolMgr.GetInstance().PushObj<FastLearnFrameWork.PoolLearn>(s);
        s = PoolMgr.GetInstance().GetObj<FastLearnFrameWork.PoolLearn>();
        Debug.Log("3."+s.name);

        ///4. EventCenter ��Ҫ�Զ����¼�ö�٣���������string����֧�־ɵ�string����
        EventCenter.GetInstance().AddEventListener<float>(E_EventType.E_Test, (val) => {
            Debug.Log(val);
        });
        EventCenter.GetInstance().EventTrigger<float>(E_EventType.E_Test, 333);
        ///5. Resource ����ֵ�洢�Ѽ��ع������ݣ���ֹ�����ظ�����
        ///����������ã�����ж����Դ
        ///�����ڹ̶���Դ�����Խ��ǹ̶���Դ����Editor�У�ʹ��ABResMgr����(��ʵ������EditorResMgr)
        var t = ResMgr.GetInstance().Load<Material>("Materials/Blue");
        Debug.Log("5.����"+t.name);
        ResMgr.GetInstance().LoadAsync<Material>("Materials/Blue", (mat) => {
            Debug.Log("5.�첽����" + mat.name);
        });
        ResMgr.GetInstance().UnloadAsset<Material>("Materials/Blue");
        Debug.Log("5.���ü���"+ResMgr.GetInstance().GetRefCount<Material>("Materials/Blue"));
        ResMgr.GetInstance().UnloadUnusedAssets(() => {
            Debug.Log("5.���������Դ�����ü���" + ResMgr.GetInstance().GetRefCount<Material>("Materials/Blue"));
        });
        ///6.ABResMgr ��ʵ������EditorResMgr
        ABMgr.GetInstance().LoadResAsync<GameObject>("model", "Cube", (obj) =>
        {
            obj.transform.position = Vector3.up;
            Instantiate(obj);
        });
        ABMgr.GetInstance().LoadResAsync<Material>("material", "test", (obj) =>
        {
            //�пӣ����ʵ�shader��Ҫ���¸�ֵ������alwaysInclude
            obj.shader = Shader.Find(obj.shader.name);
        });
        ABResMgr.GetInstance().LoadResAsync<GameObject>("ui", "BeginPanel", (res) =>
        {
            
            GameObject panelObj = GameObject.Instantiate(res, transform, false);

            //��ȡ��ӦUI������س�ȥ
            //T panel = panelObj.GetComponent<T>();
            //��ʾ���ʱִ�е�Ĭ�Ϸ���
            //panel.ShowMe();

        }, false);
        ///7.www WebRequest
        //WWW w3 = new WWW("file://.....");
        //w3.error
        //w3.assetBundle

        UWQResMgr.GetInstance().LoadRes<string>("file://" + Application.streamingAssetsPath + "/test.txt",(val)=>{
            Debug.Log("7.���ص��ı�"+val);
        }, () => {
            Debug.Log("7.Failed");
        });
        ///8. UIMgr ���ָ���������Ⱦ
        UIMgr.GetInstance().ShowPanel<BeginPanel>(E_UILayer.Middle, (panel) => {
            panel.Test();
        });
        */
        ///9. MusicMgr
        //ABResMgr.isDebug = true;
        MusicMgr.GetInstance().PlaySound("hit_4");
        MusicMgr.GetInstance().PlayBKMusic("Begin");
        ///10.�����л�
        /*
        SceneMgr.GetInstance().LoadSceneAsyn("1", () =>
        {
            //����
        });*/
        ///11.����ģ�� ��ʱû��Ҫ
        ///InputMgr.GetInstance().StartOrCloseInputMgr(true);
        ///12.��ʱ��ģ��
        int key = TimerMgr.GetInstance().CreateTimer(true, 5000, () =>
        {
            Debug.Log("12.Over");
        }, 1000, () => {
            Debug.Log("12.Tick");
        });


    }
    public void SingletonLearn()
    {
        Debug.Log("1.����ģʽ");
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
