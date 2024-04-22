using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

namespace ProjectBase
{
    public enum JsonType
    {
        JsonUtility,
        LitJsion,
    };
    public class JsonMgr : BaseManager<JsonMgr>
    {
        /// <summary>
        /// ����Ƿ����ָ���� JSON �ļ���
        /// </summary>
        /// <param name="fileName">��Ҫ�����ļ�����</param>
        /// <returns>����ļ������򷵻� true�����򷵻� false��</returns>
        public bool JsonFileExists(string fileName)
        {
            // �趨Ĭ�������ļ�·��
            string path = Application.streamingAssetsPath + "/" + fileName + ".json";

            // ���Ĭ�������ļ������ڣ����趨��д�ļ�·��
            if (!File.Exists(path))
                path = Application.persistentDataPath + "/" + fileName + ".json";

            // ����ļ����ڣ��򷵻� true
            if (File.Exists(path))
                return true;

            // ����ļ������ڣ��򷵻� false
            return false;
        }

        public void SaveData(object data, string fileName, JsonType type = JsonType.LitJsion)
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
            TextAsset jsonData = Resources.Load<TextAsset>("Json/" + fileName);
            string jsonStr;
            if (jsonData == null)
            {
                string path = Application.persistentDataPath + "/" + fileName + ".json";
                if (!File.Exists(path))
                {
                    path = Application.streamingAssetsPath + "/" + fileName + ".json";
                }
                if (!File.Exists(path))
                {
                    return new T();
                }
                jsonStr = File.ReadAllText(path);
            }
            else
            {
                jsonStr = jsonData.text;
            }

            //�����л�


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
}


