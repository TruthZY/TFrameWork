using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectBase{
    /// <summary>
    /// ������Ϣ
    /// </summary>
    public class InputInfo
    {
        public enum E_KeyOrMouse
        {
            /// <summary>
            /// ��������
            /// </summary>
            Key,
            /// <summary>
            /// �������
            /// </summary>
            Mouse,
            /// <summary>
            /// UI����
            /// </summary>
            UI

        }

        public enum E_InputType
        {
            /// <summary>
            /// ����
            /// </summary>
            Down,
            /// <summary>
            /// ̧��
            /// </summary>
            Up,
            /// <summary>
            /// ����
            /// </summary>
            Always,
        }

        //������������͡������̻������
        public E_KeyOrMouse keyOrMouse;
        //��������͡���̧�𡢰��¡�����
        public E_InputType inputType;
        //KeyCode
        public KeyCode key;
        //mouseID
        public int mouseID;

        /// <summary>
        /// ��Ҫ�����������ʼ��
        /// </summary>
        /// <param name="inputType"></param>
        /// <param name="key"></param>
        public InputInfo(E_InputType inputType, KeyCode key)
        {
            this.keyOrMouse = E_KeyOrMouse.Key;
            this.inputType = inputType;
            this.key = key;
        }

        /// <summary>
        /// ��Ҫ����������ʼ��
        /// </summary>
        /// <param name="inputType"></param>
        /// <param name="mouseID"></param>
        public InputInfo(E_InputType inputType, int mouseID)
        {
            this.keyOrMouse = E_KeyOrMouse.Mouse;
            this.inputType = inputType;
            this.mouseID = mouseID;
        }
    }

}
