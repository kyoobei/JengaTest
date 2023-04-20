using System.Collections.Generic;
namespace Gelo.Jenga
{
    [System.Serializable]
    public class StudentData
    {
        public string identifier;
        public List<JengaObject> data = new List<JengaObject>();
    }
}
