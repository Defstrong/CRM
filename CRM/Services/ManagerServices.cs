using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public void SendMassage(InputUserDto MassageInformation)
        {
            massages.Add(new Massage
            {
                Id = Guid.NewGuid(),
                Name = "Manager",
                IdRecipient = MassageInformation.Id,
                Role = Roles.User,
                Text = MassageInformation.Text,
                Theme = MassageInformation.Theme
            }) ;
        }

        public void OverdueRequest()
        {
            for(int i = 0; i < requestsUser.Count; i++)
            {
                if ( requestsUser[i].Payday >= DateTime.Now && requestsUser[i].StatusDuty == StatusUser.Accepted )
                {
                    int idx = person.FindIndex(x => x.Id == requestsUser[i].IdSender);
                    massages.Add(new Massage
                    {
                        Id = Guid.NewGuid(),
                        Name = "Manager",
                        IdRecipient = requestsUser[i].IdSender,
                        Role = Roles.User,
                        Text = $"Dear client {person[idx].FirstName} {person[idx].LastName} {person[idx].Patronymic} you have not repaid your debt, please within the next three days",
                        Theme = "Overdue"
                    });
                }
            }
            
        }
    }
}
