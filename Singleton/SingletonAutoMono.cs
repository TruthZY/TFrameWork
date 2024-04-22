using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBase
{
    /// <summary>
    /// �Զ�����ʽ�� �̳�Mono�ĵ���ģʽ����
    /// �Ƽ�ʹ�� 
    /// �����ֶ����� ���趯̬��� ��������г�������������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    //��̬���� ��̬����
                    //�ڳ����ϴ���������
                    GameObject obj = new GameObject();
                    //�õ�T�ű������� Ϊ������� �����ٱ༭���п�����ȷ�Ŀ�����
                    //����ģʽ�ű�����������GameObject
                    obj.name = typeof(T).ToString();
                    //��̬���ض�Ӧ�� ����ģʽ�ű�
                    instance = obj.AddComponent<T>();
                    //������ʱ���Ƴ����� ��֤����������Ϸ���������ж�����
                    DontDestroyOnLoad(obj);
                }
                return instance;
            }
        }
        public static T GetInstance()
        {
            return Instance;
        }

    }

}
