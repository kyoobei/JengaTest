using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;
namespace Gelo.Jenga.UI
{
    public class MainUIModel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text m_infoText;
        [SerializeField]
        private Transform m_buttonArea;
        [SerializeField]
        private GameObject m_buttonPrefab;

        public Action OnClickedReleaseButton;
        public Action OnClickedResetCamera;
        public Action<int> OnClickedCamButton;

        public void SetInfo(string domain, string cluster, string standardDesc)
        {
            m_infoText.text = $"Grade Level: {domain}\n\nCluster: {cluster}\n\nStandard Description: {standardDesc}";
        }
        public void CreateButton(string name, int index)
        {
            GameObject go = Instantiate(m_buttonPrefab);
            CameraTargetButton camButton = go.GetComponent<CameraTargetButton>();
            camButton.SetButton(name, index);
            camButton.OnButtonClicked += ClickedCamButton;

            go.transform.SetParent(m_buttonArea);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
        }
        public void ClickedReleaseButton()
        {
            OnClickedReleaseButton?.Invoke();
        }
        public void ClickedResetCamera()
        {
            OnClickedResetCamera?.Invoke();
        }
        public void ClickedCamButton(int index)
        {
            OnClickedCamButton?.Invoke(index);
        }
    }
}
