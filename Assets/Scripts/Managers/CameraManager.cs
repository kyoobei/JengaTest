using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gelo.Jenga
{
    public class CameraManager : MonoBehaviour
    {
        private Vector3 m_initialPosition;
        private Quaternion m_initialRotation;

        [SerializeField]
        public float m_minDistance;
        [SerializeField]
        private float m_maxDistance;
        [SerializeField]
        private float m_camSpeed;

        [SerializeField]
        private float m_currentDistance;
        [SerializeField]
        private float m_angleHor;
        [SerializeField]
        private float m_angleVer;

        private List<Transform> m_targetTransforms = new List<Transform>();
        private int m_index = 0;
        private bool hasTarget = false;

        private void Start()
        {
            m_initialPosition = Camera.main.transform.position;
            m_initialRotation = Camera.main.transform.rotation;
        }
        private void Update()
        {
            // in case theres no target availble
            if (m_targetTransforms.Count <= 0 || hasTarget == false)
                return;

            if (Input.GetMouseButton(1)) 
            {
                m_angleHor += Input.GetAxis("Mouse X");
                m_angleVer -= Input.GetAxis("Mouse Y");
                m_currentDistance += Input.GetAxis("Mouse ScrollWheel");

                transform.RotateAround(m_targetTransforms[m_index].position, transform.right, -Input.GetAxis("Vertical") * m_camSpeed); 
                transform.RotateAround(m_targetTransforms[m_index].position, transform.up, -Input.GetAxis("Horizontal") * m_camSpeed);
            }
        }

        public void LateUpdate()
        {
            if (hasTarget) 
            {
                LookAtTarget();
            }
        }
        private void LookAtTarget()
        {
           Vector3 tmp;
           tmp.x = (Mathf.Cos(m_angleHor * (Mathf.PI / 180)) * Mathf.Sin(m_angleVer * (Mathf.PI / 180)) * m_currentDistance + m_targetTransforms[m_index].position.x);
           tmp.z = (Mathf.Sin(m_angleHor * (Mathf.PI / 180)) * Mathf.Sin(m_angleVer * (Mathf.PI / 180)) * m_currentDistance + m_targetTransforms[m_index].position.z);
           tmp.y = Mathf.Sin(m_angleVer * (Mathf.PI / 180)) * m_currentDistance + m_targetTransforms[m_index].position.y;
           Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, tmp, m_camSpeed * Time.deltaTime);
           Camera.main.transform.LookAt(m_targetTransforms[m_index]);
        }

        public void AddTargets(Transform target)
        {
            m_targetTransforms.Add(target);
        }
        public void RecievedChangeTarget(int index) 
        {
            Debug.Log(index);
            hasTarget = true;
            m_index = index;
        }
        public void RecievedResetCamera()
        {
            Camera.main.transform.position = m_initialPosition;
            Camera.main.transform.rotation = m_initialRotation;
            hasTarget = false;
            m_index = 0;
        }
    }
}
