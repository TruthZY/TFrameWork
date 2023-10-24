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
        //场景加载进度 
        while (!ao.isDone)
        {
            //更新进度条
            EventCenter.GetInstance().EventTrigger("进度条更新",ao.progress);
            yield return ao.progress;
        }
        fun.Invoke();
    }
}
