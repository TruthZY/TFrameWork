using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;

public class BinaryMgr
{

    public static void SerializeObject<T>(T serializableObject, string fileName)
    {
        if (serializableObject == null) { return; }

        try
        {
            FileStream fileStream = File.Create(Application.streamingAssetsPath + "/" + fileName + "." + typeof(T).Name);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, serializableObject);
            fileStream.Close();
            Debug.Log(Application.streamingAssetsPath + "/" + fileName + "." + typeof(T).Name);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public static T DeSerializeObject<T>(string fileName)
    {
        T objectOut = default(T);

        if (File.Exists(Application.streamingAssetsPath + "/" + fileName + "." + typeof(T).Name))
        {
            try
            {
                FileStream fileStream = File.Open(Application.streamingAssetsPath + "/" + fileName + "." + typeof(T).Name, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                objectOut = (T)formatter.Deserialize(fileStream);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        return objectOut;
    }
}