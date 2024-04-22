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
        /// 检查是否存在指定的 JSON 文件。
        /// </summary>
        /// <param name="fileName">需要检查的文件名。</param>
        /// <returns>如果文件存在则返回 true，否则返回 false。</returns>
        public bool JsonFileExists(string fileName)
        {
            // 设定默认数据文件路径
            string path = Application.streamingAssetsPath + "/" + fileName + ".json";

            // 如果默认数据文件不存在，则设定读写文件路径
            if (!File.Exists(path))
                path = Application.persistentDataPath + "/" + fileName + ".json";

            // 如果文件存在，则返回 true
            if (File.Exists(path))
                return true;

            // 如果文件不存在，则返回 false
            return false;
        }

        public void SaveData(object data, string fileName, JsonType type = JsonType.LitJsion)
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

            //反序列化


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
}


