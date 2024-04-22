using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectBase
{
    /// <summary>
    /// �¼����� ö��
    /// </summary>
    public enum E_EventType
    {
        /// <summary>
        /// ���������¼� ���� ������Monster
        /// </summary>
        E_Monster_Dead,
        /// <summary>
        /// ��һ�ȡ���� ���� ������int
        /// </summary>
        E_Player_GetReward,
        /// <summary>
        /// �������¼� ���� ��������
        /// </summary>
        E_Test,
        /// <summary>
        /// �����л�ʱ���ȱ仯��ȡ
        /// </summary>
        E_SceneLoadChange,

        /// <summary>
        /// ����ϵͳ��������1 ��Ϊ
        /// </summary>
        E_Input_Skill1,
        /// <summary>
        /// ����ϵͳ��������2 ��Ϊ
        /// </summary>
        E_Input_Skill2,
        /// <summary>
        /// ����ϵͳ��������3 ��Ϊ
        /// </summary>
        E_Input_Skill3,

        /// <summary>
        /// ˮƽ�ȼ� -1~1���¼�����
        /// </summary>
        E_Input_Horizontal,

        /// <summary>
        /// ��ֱ�ȼ� -1~1���¼�����
        /// </summary>
        E_Input_Vertical,
    }


}
