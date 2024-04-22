using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ProjectBase;

    public abstract class MyBasePanel : MonoBehaviour
    {
        /// <summary>
        /// ���ڴ洢����Ҫ�õ���UI�ؼ�������ʷ�滻ԭ�� ����װ����
        /// </summary>
        protected Dictionary<string, UIBehaviour> controlDic = new Dictionary<string, UIBehaviour>();

        /// <summary>
        /// �ؼ�Ĭ������ ����õ��Ŀؼ����ִ������������ ��ζ�����ǲ���ͨ������ȥʹ���� ��ֻ��������ʾ���õĿؼ�
        /// </summary>
        private static List<string> defaultNameList = new List<string>() { "Image",
                                                                   "Text (TMP)",
                                                                   "RawImage",
                                                                   "Background",
                                                                   "Checkmark",
                                                                   "Label",
                                                                   "Text (Legacy)",
                                                                   "Arrow",
                                                                   "Placeholder",
                                                                   "Fill",
                                                                   "Handle",
                                                                   "Viewport",
                                                                   "Scrollbar Horizontal",
                                                                   "Scrollbar Vertical"};


        protected virtual void Awake()
        {
            //Ϊ�˱��� ĳһ�������ϴ������ֿؼ������
            //����Ӧ�����Ȳ�����Ҫ�����
            FindChildrenControl<Button>();
            FindChildrenControl<Toggle>();
            FindChildrenControl<Slider>();
            FindChildrenControl<InputField>();
            FindChildrenControl<ScrollRect>();
            FindChildrenControl<Dropdown>();
            //��ʹ�����Ϲ����˶����� ֻҪ�����ҵ�����Ҫ���
            //֮��Ҳ����ͨ����Ҫ����õ������������ص�����
            FindChildrenControl<Text>();
            FindChildrenControl<TextMeshPro>();
            FindChildrenControl<Image>();
        }

        /// <summary>
        /// �����ʾʱ����õ��߼�
        /// </summary>
        public abstract void ShowMe();

        /// <summary>
        /// �������ʱ����õ��߼�
        /// </summary>
        public abstract void HideMe();

        /// <summary>
        /// ��ȡָ�������Լ�ָ�����͵����
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        /// <param name="name">�������</param>
        /// <returns></returns>
        public T GetControl<T>(string name) where T : UIBehaviour
        {
            if (controlDic.ContainsKey(name))
            {
                T control = controlDic[name] as T;
                if (control == null)
                    Debug.LogError($"�����ڶ�Ӧ����{name}����Ϊ{typeof(T)}�����");
                return control;
            }
            else
            {
                Debug.LogError($"�����ڶ�Ӧ����{name}�����");
                return null;
            }
        }

        protected virtual void ClickBtn(string btnName)
        {

        }

        protected virtual void SliderValueChange(string sliderName, float value)
        {

        }

        protected virtual void ToggleValueChange(string sliderName, bool value)
        {

        }

        private void FindChildrenControl<T>() where T : UIBehaviour
        {
            T[] controls = this.GetComponentsInChildren<T>(true);
            for (int i = 0; i < controls.Length; i++)
            {
                //��ȡ��ǰ�ؼ�������
                string controlName = controls[i].gameObject.name;
                //ͨ�����ַ�ʽ ����Ӧ�����¼���ֵ���
                if (!controlDic.ContainsKey(controlName))
                {
                    if (!defaultNameList.Contains(controlName))
                    {
                        controlDic.Add(controlName, controls[i]);
                        //�жϿؼ������� �����Ƿ���¼�����
                        if (controls[i] is Button)
                        {
                            (controls[i] as Button).onClick.AddListener(() =>
                            {
                                ClickBtn(controlName);
                            });
                        }
                        else if (controls[i] is Slider)
                        {
                            (controls[i] as Slider).onValueChanged.AddListener((value) =>
                            {
                                SliderValueChange(controlName, value);
                            });
                        }
                        else if (controls[i] is Toggle)
                        {
                            (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                            {
                                ToggleValueChange(controlName, value);
                            });
                        }
                    }

                }
            }
        }
    }

