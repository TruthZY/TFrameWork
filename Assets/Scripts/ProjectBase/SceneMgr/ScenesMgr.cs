using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenesMgr : BaseManager<ScenesMgr>
{
    public void LoadScene(string name, UnityAction fun)
    {
        SceneManager.LoadScene(name);
        fun.Invoke();
    }

    public void LoadSceneAsyn(string name, UnityAction fun)
    {
        MonoMgr.GetInstance().StopCoroutine(ReallyLoadSceneAsyn(name, fun));
    }
    private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction fun)
    {
        AsyncOperation ao =  SceneManager.LoadSceneAsync(name);
        //�������ؽ��� 
        while (!ao.isDone)
        {
            //���½�����
            EventCenter.GetInstance().EventTrigger("����������",ao.progress);
            yield return ao.progress;
        }
        fun.Invoke();
    }
}
