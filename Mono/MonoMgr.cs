using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace ProjectBase
{

    /// <summary>
    /// ����Monoģ�������
    /// </summary>
    public class MonoMgr : SingletonAutoMono<MonoMgr>
    {
        private event UnityAction updateEvent;
        private event UnityAction fixedUpdateEvent;
        private event UnityAction lateUpdateEvent;

        /// <summary>
        /// ���Update֡���¼�������
        /// </summary>
        /// <param name="updateFun"></param>
        public void AddUpdateListener(UnityAction updateFun)
        {
            updateEvent += updateFun;
        }

        /// <summary>
        /// �Ƴ�Update֡���¼�������
        /// </summary>
        /// <param name="updateFun"></param>
        public void RemoveUpdateListener(UnityAction updateFun)
        {
            updateEvent -= updateFun;
        }

        /// <summary>
        /// ���FixedUpdate֡���¼�������
        /// </summary>
        /// <param name="updateFun"></param>
        public void AddFixedUpdateListener(UnityAction updateFun)
        {
            fixedUpdateEvent += updateFun;
        }
        /// <summary>
        /// �Ƴ�FixedUpdate֡���¼�������
        /// </summary>
        /// <param name="updateFun"></param>
        public void RemoveFixedUpdateListener(UnityAction updateFun)
        {
            fixedUpdateEvent -= updateFun;
        }

        /// <summary>
        /// ���LateUpdate֡���¼�������
        /// </summary>
        /// <param name="updateFun"></param>
        public void AddLateUpdateListener(UnityAction updateFun)
        {
            lateUpdateEvent += updateFun;
        }

        /// <summary>
        /// �Ƴ�LateUpdate֡���¼�������
        /// </summary>
        /// <param name="updateFun"></param>
        public void RemoveLateUpdateListener(UnityAction updateFun)
        {
            lateUpdateEvent -= updateFun;
        }


        private void Update()
        {
            updateEvent?.Invoke();
        }

        private void FixedUpdate()
        {
            fixedUpdateEvent?.Invoke();
        }

        private void LateUpdate()
        {
            lateUpdateEvent?.Invoke();
        }
    }

}