using UnityEngine.UI;
using UnityEngine;
using TMPro;
namespace Gelo.Jenga
{
    public class Spawnpoint : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text m_spawnPointName;

        public void SetName(string name) 
        {
            m_spawnPointName.text = name;
        }
    }
}
