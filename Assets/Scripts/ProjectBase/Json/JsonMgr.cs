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
        //ȷ���洢·��
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        //���л�
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
        //ȷ����ȡ·��
        //���ж�Ĭ�������ļ������Ƿ�����Ҫ������ ����оʹ��л�ȡ
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        //��������Ӷ�д�ļ�����Ѱ��
        if (!File.Exists(path))
            path = Application.persistentDataPath + "/" + fileName + ".json";
        //������ �򷵻�һ��Ĭ�϶���
        if(!File.Exists(path))  return new T();
        //�����л�
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
        //���ض���
        return data;
    }
}
