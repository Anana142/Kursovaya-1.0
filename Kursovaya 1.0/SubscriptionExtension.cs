using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya_1._0
{
    public static class SubscriptionExtension
    {
        public static void DeleteSubscriotion(Subscription subscription)
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
