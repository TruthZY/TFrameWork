using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProjectBase
{
    /// <summary>
    /// �����л������� ��Ҫ�����л�����
    /// </summary>
    public class SceneMgr : BaseManager<SceneMgr>
    {
        private SceneMgr() { }

        //ͬ���л������ķ���
        public void LoadScene(string name, UnityAction callBack = null)
        {
            //�л�����
            SceneManager.LoadScene(name);
            //���ûص�
            callBack?.Invoke();
            callBack = null;
        }

        //�첽�л������ķ���
        public void LoadSceneAsyn(string name, UnityAction callBack = null)
        {
            MonoMgr.Instance.StartCoroutine(ReallyLoadSceneAsyn(name, callBack));
        }

        private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction callBack)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name);
            //��ͣ����Эͬ������ÿ֡����Ƿ���ؽ��� ������ؽ����Ͳ�������ѭ��ÿִ֡����
            while (!ao.isDone)
            {
                //���������������¼����� ÿһ֡�����ȷ��͸���Ҫ�õ��ĵط�
                EventCenter.Instance.EventTrigger<float>(E_EventType.E_SceneLoadChange, ao.progress);
                yield return 0;
            }
            //�������һֱ֡�ӽ����� û��ͬ��1��ȥ
            EventCenter.Instance.EventTrigger<float>(E_EventType.E_SceneLoadChange, 1);

            callBack?.Invoke();
            callBack = null;
        }
    }

}

