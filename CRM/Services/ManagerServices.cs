using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public sealed class ManagerServices
    {
        private static List<Person> person;
        static List<GetMoney> requestsUser;
        static List<Massage> massages;
        public ManagerServices(List<Person> Person, List<GetMoney> RequestsUser, List<Massage> Massages)
        {
            person = Person;
            requestsUser = RequestsUser;
            massages = Massages;
        }
        public void ChoiceRequest(int idx, string choice)
        {
            if (choice == "Accepted")
                requestsUser[idx].StatusDuty = StatusUser.Accepted;
            else if(choice == "Refuse")
                requestsUser[idx].StatusDuty = StatusUser.Refuse;
        }
    }
}
