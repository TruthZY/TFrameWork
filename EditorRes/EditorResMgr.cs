using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProjectBase
{
    /// <summary>
    /// �༭����Դ������
    /// ע�⣺ֻ���ڿ���ʱ��ʹ�øù�����������Դ ���ڿ�������
    /// ������ ���޷�ʹ�øù������� ��Ϊ����Ҫ�õ��༭����ع���
    /// </summary>
    public class EditorResMgr : BaseManager<EditorResMgr>
    {
        //���ڷ�����Ҫ�����AB���е���Դ·�� 
        private string rootPath = "Assets/Editor/ArtRes/";

        private EditorResMgr() { }

        //1.���ص�����Դ��
        public T LoadEditorRes<T>(string path) where T : Object
        {
#if UNITY_EDITOR
            string suffixName = "";
            //Ԥ���塢����ͼƬ������������Ч�ȵ�
            if (typeof(T) == typeof(GameObject))
                suffixName = ".prefab";
            else if (typeof(T) == typeof(Material))
                suffixName = ".mat";
            else if (typeof(T) == typeof(Texture))
                suffixName = ".png";
            else if (typeof(T) == typeof(AudioClip))
                suffixName = ".mp3";
            T res = AssetDatabase.LoadAssetAtPath<T>(rootPath + path + suffixName);
            return res;
#else
        return null;
#endif
        }

        //2.����ͼ�������Դ��
        public Sprite LoadSprite(string path, string spriteName)
        {
#if UNITY_EDITOR
            //����ͼ���е���������Դ 
            Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(rootPath + path);
            //������������Դ �õ�ͬ��ͼƬ����
            foreach (var item in sprites)
            {
                if (spriteName == item.name)
                    return item as Sprite;
            }
            return null;
#else
        return null;
#endif
        }

        //����ͼ���ļ��е�������ͼƬ�����ظ��ⲿ
        public Dictionary<string, Sprite> LoadSprites(string path)
        {
#if UNITY_EDITOR
            Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();
            Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(rootPath + path);
            foreach (var item in sprites)
            {
                spriteDic.Add(item.name, item as Sprite);
            }
            return spriteDic;
#else
        return null;
#endif
        }

    }

}

