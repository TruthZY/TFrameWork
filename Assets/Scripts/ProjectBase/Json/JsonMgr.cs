using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public enum JsonType
{
    JsonUtility,
    LitJsion,
};
public class JsonMgr:BaseManager<JsonMgr>
{


    public void SaveData(object data, string fileName,JsonType type=JsonType.LitJsion)
    {
        //确定存储路径
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        //序列化
        string jsonStr = "";
        switch (type)
        {
            case JsonType.JsonUtility:
                jsonStr = JsonUtility.ToJson(data);
                break;
            case JsonType.LitJsion:
                jsonStr = JsonMapper.ToJson(data);
                break;
        }
        Debug.Log(path);
        File.WriteAllText(path, jsonStr);
    }
    public T LoadData<T>(string fileName, JsonType type = JsonType.LitJsion) where T : new()
    {
        //确定读取路径
        //先判断默认数据文件夹中是否有想要的数据 如果有就从中获取
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        //不存在则从读写文件夹中寻找
        if (!File.Exists(path))
            path = Application.persistentDataPath + "/" + fileName + ".json";
        //不存在 则返回一个默认对象
        if(!File.Exists(path))  return new T();
        //反序列化
        string jsonStr = File.ReadAllText(path);

        T data = default(T);
        switch (type)
        {
            case JsonType.JsonUtility:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
            case JsonType.LitJsion:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
        }
        //返回对象
        return data;
    }
}
