using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace ProjectBase
{

    /// <summary>
    /// ���ڽ��м���AB�����Դ������ �ڿ����п���ͨ��EditorResMgrȥ���ض�Ӧ��Դ���в���
    /// </summary>
    public class ABResMgr : BaseManager<ABResMgr>
    {
        //�����true��ͨ��EditorResMgrȥ���� �����false��ͨ��ABMgr AB������ʽȥ����
        public static bool isDebug = false;

        private ABResMgr() { }

        public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack, bool isSync = false) where T : Object
        {
#if UNITY_EDITOR
            if (isDebug)
            {
                Debug.LogWarning("Debugģʽ����" + abName +":"+ resName);
                //�����Զ�����һ��AB������Դ�Ĺ���ʽ ��Ӧ�ļ����� ���ǰ��� 
                T res = EditorResMgr.Instance.LoadEditorRes<T>($"{abName}/{resName}");
                callBack?.Invoke(res as T);
            }
            else
            {
                ABMgr.Instance.LoadResAsync<T>(abName, resName, callBack, isSync);
            }
#else
        ABMgr.Instance.LoadResAsync<T>(abName, resName, callBack, isSync);
#endif
        }
    }

}
