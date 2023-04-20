using System.Collections.Generic;
using UnityEngine;
using Gelo.Jenga.UI;
namespace Gelo.Jenga
{
    public class JengaGameManager : MonoBehaviour
    {
        [SerializeField]
        private DataManager m_dataManager;
        [SerializeField]
        private UIManager m_uiManager;
        [SerializeField]
        private CameraManager m_cameraManager;

        [SerializeField]
        private GameObject m_jengaPrefab;
        [SerializeField]
        private GameObject m_spawnPoint;
        [SerializeField]
        private float m_identifierDistance;         // distance per each identifier
        [SerializeField]
        private float m_jengaBlocksDistance;        // distance per each jenga blocks
        [SerializeField]
        private float m_jengaRotatedZPos;           // starting z pos of jenga when rotated

        private void OnEnable()
        {
            m_dataManager.OnFinishedPopulating += GenerateJengaBlocks;
            m_uiManager.OnFocusCamera += m_cameraManager.RecievedChangeTarget;
            m_uiManager.OnResetCamera += m_cameraManager.RecievedResetCamera;
        }
        private void OnDisable()
        {
            m_dataManager.OnFinishedPopulating -= GenerateJengaBlocks;
            m_uiManager.OnFocusCamera -= m_cameraManager.RecievedChangeTarget;
            m_uiManager.OnResetCamera -= m_cameraManager.RecievedResetCamera;
        }

        private void GenerateJengaBlocks(List<StudentData> m_students)
        {
            m_uiManager.GenerateCameraButtons(m_students);
            Vector3 currentLocation = Vector3.zero;
            for (int i = 0; i < m_students.Count; i++) 
            {
                GameObject spawnHolder = Instantiate(m_spawnPoint);
                Spawnpoint spawnPoint = spawnHolder.GetComponent<Spawnpoint>();
                spawnHolder.name = m_students[i].identifier;
                spawnPoint.SetName(m_students[i].identifier);

                // add camera targets
                m_cameraManager.AddTargets(spawnHolder.transform);

                spawnHolder.transform.position = currentLocation;
                // temp values
                int tempCountToRotate = 0;
                float localTempX = 0f;
                float localTempY = 0f;
                float localTempZ = 0f;
                float localRotY = 0f;

                bool isRotated = false;
                for (int j = 0; j < m_students[i].data.Count; j++) 
                {
                    GameObject jengaBlock = Instantiate(m_jengaPrefab);
                    JengaBlock jbComponent = jengaBlock.GetComponent<JengaBlock>();
                    jbComponent.SetData(m_students[i].data[j]);
                    // add a listener for the action
                    m_uiManager.OnReleaseStack += jbComponent.RecievedReleasePhysics;
                    jbComponent.OnClicked += m_uiManager.UpdateInfo;
                    // set positioning
                    jengaBlock.transform.SetParent(spawnHolder.transform);
                    if (j != 0)
                    {
                        if (tempCountToRotate % 3 == 0)
                        {
                            localTempX = 0f;
                            localTempY += 1f;
                            if (!isRotated)
                            {
                                localRotY = 90f;
                                localTempZ = m_jengaRotatedZPos;
                                isRotated = true;
                            }
                            else
                            {
                                localRotY = 0f;
                                localTempZ = 0f;
                                isRotated = false;
                            }
                        }
                        jengaBlock.transform.localPosition = new Vector3(localTempX, localTempY, localTempZ);
                        jengaBlock.transform.localRotation = Quaternion.Euler(0f, localRotY, 0f);
                        if (isRotated)
                        {
                            localTempZ -= m_jengaBlocksDistance;
                        }
                        else 
                        {
                            localTempX += m_jengaBlocksDistance;
                        }
                    }
                    else 
                    {
                        // starting position of initial jenga block
                        jengaBlock.transform.localPosition = Vector3.zero;
                        jengaBlock.transform.localRotation = Quaternion.identity;
                        localTempX += m_jengaBlocksDistance;
                    }
                    // reposition
                    tempCountToRotate++;
                }
                currentLocation += new Vector3(m_identifierDistance, 0, 0);
            }
        }
    }
}
