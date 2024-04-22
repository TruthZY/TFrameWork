using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
namespace ProjectBase
{
    public class UWQResMgr : SingletonAutoMono<UWQResMgr>
    {
        /// <summary>
        /// ����UnityWebRequestȥ������Դ
        /// </summary>
        /// <typeparam name="T">����ֻ����string��byte[]��Texture��AssetBundle �������������� Ŀǰ��֧��</typeparam>
        /// <param name="path">��Դ·����Ҫ�Լ�����Э�� http��ftp��file</param>
        /// <param name="callBack">���سɹ��Ļص�����</param>
        /// <param name="failCallBack">����ʧ�ܵĻص�����</param>
        public void LoadRes<T>(string path, UnityAction<T> callBack, UnityAction failCallBack) where T : class
        {
            StartCoroutine(ReallyLoadRes<T>(path, callBack, failCallBack));
        }

        private IEnumerator ReallyLoadRes<T>(string path, UnityAction<T> callBack, UnityAction failCallBack) where T : class
        {
            //string
            //byte[]
            //Texture
            //AssetBundle
            Type type = typeof(T);
            //���ڼ��صĶ���
            UnityWebRequest req = null;
            if (type == typeof(string) ||
                type == typeof(byte[]))
                req = UnityWebRequest.Get(path);
            else if (type == typeof(Texture))
                req = UnityWebRequestTexture.GetTexture(path);
            else if (type == typeof(AssetBundle))
                req = UnityWebRequestAssetBundle.GetAssetBundle(path);
            else
            {
                failCallBack?.Invoke();
                yield break;
            }

            yield return req.SendWebRequest();
            //������سɹ� 
            if (req.result == UnityWebRequest.Result.Success)
            {
                if (type == typeof(string))
                    callBack?.Invoke(req.downloadHandler.text as T);
                else if (type == typeof(byte[]))
                    callBack?.Invoke(req.downloadHandler.data as T);
                else if (type == typeof(Texture))
                    callBack?.Invoke(DownloadHandlerTexture.GetContent(req) as T);
                else if (type == typeof(AssetBundle))
                    callBack?.Invoke(DownloadHandlerAssetBundle.GetContent(req) as T);
            }
            else
                failCallBack?.Invoke();
            //�ͷ�UWQ����
            req.Dispose();
        }
    }

}
