using UnityEngine.UI;
using UnityEngine;
using System;
namespace Gelo.Jenga.UI
{
    public class CameraTargetButton : MonoBehaviour
    {
        [SerializeField]
        private Text m_buttonName;
        private int index;

        public Action<int> OnButtonClicked;

        public void SetButton(string name, int index)
        {
            m_buttonName.text = name;
            this.index = index;
        }
        public void ClickedButton()
        {
            OnButtonClicked?.Invoke(index);
        }
    }
}
