using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ProjectBase
{
    /// <summary>
    /// ����ģʽ���� ��ҪĿ���Ǳ����������� ��������ʵ�ֵ���ģʽ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseManager<T> where T : class//,new()
    {
        private static T instance;

        //�жϵ���ģʽ���� �Ƿ�Ϊnull
        protected bool InstanceisNull => instance == null;

        //���ڼ����Ķ���
        protected static readonly object lockObj = new object();

        //���Եķ�ʽ
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            //instance = new T();
                            //���÷���õ��޲�˽�еĹ��캯�� �����ڶ����ʵ����
                            Type type = typeof(T);
                            ConstructorInfo info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                                        null,
                                                                        Type.EmptyTypes,
                                                                        null);
                            if (info != null)
                                instance = info.Invoke(null) as T;
                            else
                                Debug.LogError("û�еõ���Ӧ���޲ι��캯��");

                            //instance = Activator.CreateInstance(typeof(T), true) as T;
                        }
                    }
                }
                return instance;
            }
        }
        public static T GetInstance()
        {
            return Instance;
        }


        //�����ķ�ʽ
        //public static T GetInstance()
        //{
        //    if (instance == null)
        //        instance = new T();
        //    return instance;
        //}
    }

}

