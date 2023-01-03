using CRM.DTO;
using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class CreateManager
    {
        private static List<Person> person;
        static List<GetMoney> requestsUserForGetMoney;
        static List<Massage> massages;
        public CreateManager(List<Person> Person, List<GetMoney> RequestsUserForGetMoney, List<Massage> Massages)
        {
            person = Person;
            requestsUserForGetMoney = RequestsUserForGetMoney;
            massages = Massages;
        }
        public void Registration(InputUserDto Data)
        {
            person.Add(new Person(Data, Roles.Manager) { Status = StatusUser.Accepted, Id = Guid.NewGuid() });
        }
    }
}
