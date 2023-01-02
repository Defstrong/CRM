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
    public class CreateModerator
    {
        private static List<Person> person;
        static List<GetMoney> requestsUser;
        static List<Massage> massages;
        public CreateModerator(List<Person> Person, List<GetMoney> RequestsUser, List<Massage> Massages)
        {
            person = Person;
            requestsUser = RequestsUser;
            massages = Massages;
        }
        public void Registration(InputUserDto Data)
        {
            person.Add(new Person(Data, Roles.Moderator) { Status = StatusUser.Accepted });
        }
    }
}
