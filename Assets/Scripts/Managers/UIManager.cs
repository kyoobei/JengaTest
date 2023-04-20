using System;
using System.Collections.Generic;
using UnityEngine;
using Gelo.Jenga.UI;
namespace Gelo.Jenga.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private MainUIModel m_model;

        public Action OnReleaseStack;
        public Action OnResetCamera;
        public Action<int> OnFocusCamera;

        private void OnEnable()
        {
            m_model.OnClickedReleaseButton += RecievedReleaseStack;
            m_model.OnClickedResetCamera += RecievedResetCam;
            m_model.OnClickedCamButton += RecievedFocusCam;
        }
        private void OnDisable()
        {
            m_model.OnClickedReleaseButton -= RecievedReleaseStack;
            m_model.OnClickedResetCamera -= RecievedResetCam;
            m_model.OnClickedCamButton -= RecievedFocusCam;
        }

        public void GenerateCameraButtons(List<StudentData> m_students)
        {
            for (int i = 0; i < m_students.Count; i++)
            {
                m_model.CreateButton(m_students[i].identifier, i);
            }
        }
        private void RecievedReleaseStack()
        {
            OnReleaseStack?.Invoke();
        }
        private void RecievedResetCam()
        {
            OnResetCamera?.Invoke();
        }
        private void RecievedFocusCam(int index)
        {
            OnFocusCamera?.Invoke(index);
        }
    }
}
