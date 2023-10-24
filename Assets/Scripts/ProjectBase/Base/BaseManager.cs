using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> where T:new()
{
    private static T instance;
    public static T GetInstance()
    {
        if (instance == null) instance = new T();
        return instance;
    }



}
/*用法 直接继承
public class GameManager : BaseManager<GameManager>
{

}


GameManger.GetInstance();
*/
