using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectBase
{
    /// <summary>
    /// ����ʽ �̳�Mono�ĵ���ģʽ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                return instance;
            }
        }

        protected virtual void Awake()
        {
            //�Ѿ�����һ����Ӧ�ĵ���ģʽ������ ����Ҫ����һ����
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this as T;
            //���ǹ��ؼ̳иõ���ģʽ����Ľű��� �����Ķ��������ʱ�Ͳ��ᱻ�Ƴ���
            //�Ϳ��Ա�֤����Ϸ���������������ж����� 
            DontDestroyOnLoad(this.gameObject);
        }
        public static T GetInstance()
        {
            return Instance;
        }

    }

}
