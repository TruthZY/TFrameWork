using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace ProjectBase
{
    /// <summary>
    /// ��ʱ������ ����洢�˼�ʱ�����������
    /// </summary>
    public class TimerItem : IPoolObject
    {
        /// <summary>
        /// ΨһID
        /// </summary>
        public int keyID;
        /// <summary>
        /// ��ʱ�������ί�лص�
        /// </summary>
        public UnityAction overCallBack;
        /// <summary>
        /// ���һ��ʱ��ȥִ�е�ί�лص�
        /// </summary>
        public UnityAction callBack;

        /// <summary>
        /// ��ʾ��ʱ���ܵļ�ʱʱ�� ���룺1s = 1000ms
        /// </summary>
        public int allTime;
        /// <summary>
        /// ��¼һ��ʼ��ʱʱ����ʱ�� ����ʱ������
        /// </summary>
        public int maxAllTime;

        /// <summary>
        /// ���ִ�лص���ʱ�� ���� ���룺1s = 1000ms
        /// </summary>
        public int intervalTime;
        /// <summary>
        /// ��¼һ��ʼ�ļ��ʱ��
        /// </summary>
        public int maxIntervalTime;

        /// <summary>
        /// �Ƿ��ڽ��м�ʱ
        /// </summary>
        public bool isRuning;

        /// <summary>
        /// ��ʼ����ʱ������
        /// </summary>
        /// <param name="keyID">ΨһID</param>
        /// <param name="allTime">�ܵ�ʱ��</param>
        /// <param name="overCallBack">��ʱ���ʱ������Ļص�</param>
        /// <param name="intervalTime">���ִ�е�ʱ��</param>
        /// <param name="callBack">���ִ��ʱ�������Ļص�</param>
        public void InitInfo(int keyID, int allTime, UnityAction overCallBack, int intervalTime = 0, UnityAction callBack = null)
        {
            this.keyID = keyID;
            this.maxAllTime = this.allTime = allTime;
            this.overCallBack = overCallBack;
            this.maxIntervalTime = this.intervalTime = intervalTime;
            this.callBack = callBack;
            this.isRuning = true;
        }

        /// <summary>
        /// ���ü�ʱ��
        /// </summary>
        public void ResetTimer()
        {
            this.allTime = this.maxAllTime;
            this.intervalTime = this.maxIntervalTime;
            this.isRuning = true;
        }

        /// <summary>
        /// ����ػ���ʱ  ��������������
        /// </summary>
        public void ResetInfo()
        {
            overCallBack = null;
            callBack = null;
        }
    }
}

