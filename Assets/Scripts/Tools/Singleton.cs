using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    //代表MonoBehaviour是Singleton的一个类型 用where来约束
{
    private static T instance;
    //所有的manager都可以用作T类型
    public static T Instance {  get { return instance; } }
    //只读
    protected virtual void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        //GameObject是类名 小写g是实例 具体的游戏对象
        else
            instance=(T)this;
    }
    public static bool IsInitialized {  get { return instance!=null; } }
    protected virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }


}
