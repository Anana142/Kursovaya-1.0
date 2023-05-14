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

        public void AddVisit(int id)
        {
            Attendance attendance = new Attendance();
            attendance.Id = id;
            attendance.Date = DateTime.Now;

            DataBase.GetInstance().Attendances.Add(attendance);
        }

        public void DeleteSubscriotion(Subscription subscription)
        {
            if (subscription != null)
            {
                Subscription sub = subscription;

                List<Attendance> at = DataBase.GetInstance().Attendances.Where(s => s.Id == sub.Id).ToList();
                foreach (Attendance att in at)
                {
                    DataBase.GetInstance().Attendances.Remove(att);
                    DataBase.GetInstance().SaveChanges();
                }

                List<Subscriptionservice> ss = DataBase.GetInstance().Subscriptionservices.Where(s => s.IdSubscrirtion == sub.Id).ToList();
                foreach (Subscriptionservice s in ss)
                {
                    DataBase.GetInstance().Subscriptionservices.Remove(s);
                    DataBase.GetInstance().SaveChanges();
                }

                List<Sale> sale = DataBase.GetInstance().Sales.Where(s => s.IdSubscription == sub.Id).ToList();

                foreach (Sale s in sale)
                {
                    DataBase.GetInstance().Sales.Remove(s);
                    DataBase.GetInstance().SaveChanges();
                }

                DataBase.GetInstance().Subscriptions.Remove(sub);
                DataBase.GetInstance().SaveChanges();
            }
        }
      }
}
