using UnityEngine;
using System;
namespace Gelo.Jenga
{
    public class JengaBlock : MonoBehaviour
    {
        [SerializeField]
        private JengaObject m_currentData;
        [SerializeField]
        private MeshRenderer m_jengaRenderer;
        [SerializeField]
        private Rigidbody m_jengaRigibody;
        [SerializeField]
        private Material m_matGlass;
        [SerializeField]
        private Material m_matWood;
        [SerializeField]
        private Material m_matStone;

        private bool m_isGlass = false;

        public void SetData(JengaObject data)
        {
            if (data.mastery == 0)
            {
                m_jengaRenderer.material = m_matGlass;
                m_isGlass = true;
            }
            else if (data.mastery == 1)
            {
                m_jengaRenderer.material = m_matWood;
            }
            else if (data.mastery == 2) 
            {
                m_jengaRenderer.material = m_matStone;
            }
        }

        public void RecievedReleasePhysics()
        {
            if (m_isGlass) 
            {
                // remove glass type jenga
                gameObject.SetActive(false);
            }
            m_jengaRigibody.isKinematic = false;
        }
    }
}
