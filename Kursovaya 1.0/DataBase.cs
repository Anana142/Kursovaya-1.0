using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya_1._0
{
    public class DataBase
    {
        public DataBase() { }
        static SportclubContext instance;
        public static SportclubContext GetInstance()
        {
            if (instance == null)
                instance = new SportclubContext();
            return instance;    
        }

        public Worker Authorization(string login, string password)
        {
            List<Worker> workers = DataBase.GetInstance().Workers.ToList();
            Worker worker = workers.FirstOrDefault(s => s.Login == login && s.Password == password, null);

            return worker;
            
        }

        
      }
}
