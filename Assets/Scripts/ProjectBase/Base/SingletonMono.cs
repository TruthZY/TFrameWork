using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
        return instance;
    }
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
    }
}
/*用法 直接继承
public class GameManager : SingletonMono<GameManager>
{
    protected override void Awake(){
        base.Awake();//重写
    }
}


GameManger.GetInstance();
*/
