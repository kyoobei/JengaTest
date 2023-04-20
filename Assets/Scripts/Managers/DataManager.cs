using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
namespace Gelo.Jenga
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField]
        private string m_url;

        private List<StudentData> m_studentDatas = new List<StudentData>();

        public Action<List<StudentData>> OnFinishedPopulating;

        void Start()
        {
            StartCoroutine(GetRequest(m_url));
        }

        IEnumerator GetRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError("problem in processing data: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("recieved an error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        string requestRecieved = webRequest.downloadHandler.text;
                        var parsedData = JsonUtility.FromJson<JengaData>("{\"data\":" + requestRecieved + "}");
                        PopulateStudentDatas(parsedData);
                        break;
                }
            }
        }

        private void PopulateStudentDatas(JengaData jengaData)
        {
            string _identifier = string.Empty;
            // create identifiers first
            for(int i = 0; i < jengaData.data.Count; i++)
            {
                if (jengaData.data[i].grade != _identifier) 
                {
                    _identifier = jengaData.data[i].grade;
                    m_studentDatas.Add(new StudentData() { identifier = _identifier });
                }
            }
            // insert students inside each identifier
            for (int i = 0; i < m_studentDatas.Count; i++) 
            {
                for (int j = 0; j < jengaData.data.Count; j++) 
                {
                    if (jengaData.data[j].grade == m_studentDatas[i].identifier) 
                    {
                        m_studentDatas[i].data.Add(
                            new JengaObject()
                            {
                                id = jengaData.data[j].id,
                                subject = jengaData.data[j].subject,
                                grade = jengaData.data[j].grade,
                                mastery = jengaData.data[j].mastery,
                                domainid = jengaData.data[j].domainid,
                                domain = jengaData.data[j].domain,
                                cluster = jengaData.data[j].cluster,
                                standardid = jengaData.data[j].standardid,
                                standarddescription = jengaData.data[j].standarddescription
                            }
                        );
                    }
                }
            }
            OnFinishedPopulating?.Invoke(m_studentDatas);
        }
    }
}
