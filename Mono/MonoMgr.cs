using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace ProjectBase
{

    /// <summary>
    /// 公共Mono模块管理器
    /// </summary>
    public class MonoMgr : SingletonAutoMono<MonoMgr>
    {
        private event UnityAction updateEvent;
        private event UnityAction fixedUpdateEvent;
        private event UnityAction lateUpdateEvent;

        /// <summary>
        /// 添加Update帧更新监听函数
        /// </summary>
        /// <param name="updateFun"></param>
        public void AddUpdateListener(UnityAction updateFun)
        {
            updateEvent += updateFun;
        }

        /// <summary>
        /// 移除Update帧更新监听函数
        /// </summary>
        /// <param name="updateFun"></param>
        public void RemoveUpdateListener(UnityAction updateFun)
        {
            updateEvent -= updateFun;
        }

        /// <summary>
        /// 添加FixedUpdate帧更新监听函数
        /// </summary>
        /// <param name="updateFun"></param>
        public void AddFixedUpdateListener(UnityAction updateFun)
        {
            fixedUpdateEvent += updateFun;
        }
        /// <summary>
        /// 移除FixedUpdate帧更新监听函数
        /// </summary>
        /// <param name="updateFun"></param>
        public void RemoveFixedUpdateListener(UnityAction updateFun)
        {
            fixedUpdateEvent -= updateFun;
        }

        /// <summary>
        /// 添加LateUpdate帧更新监听函数
        /// </summary>
        /// <param name="updateFun"></param>
        public void AddLateUpdateListener(UnityAction updateFun)
        {
            lateUpdateEvent += updateFun;
        }

        /// <summary>
        /// 移除LateUpdate帧更新监听函数
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