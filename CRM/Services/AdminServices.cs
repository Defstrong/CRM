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
    public sealed class AdminServices
    {
        private static List<Person> person;
        static List<GetMoney> requestsUser;
        static List<Massage> massages;
        public AdminServices(List<Person> Person, List<GetMoney> RequestsUser, List<Massage> Massages)
        {
            person = Person;
            requestsUser = RequestsUser;
            massages = Massages;
        }
        public void SendMassage(InputUserDto MassageInformation)
        {
            massages.Add(new Massage
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                IdRecipient = MassageInformation.Id,
                Role = Roles.User,
                Text = MassageInformation.Text,
                Theme = MassageInformation.Theme
            }) ;
        }

    }
}
