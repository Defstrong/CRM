using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;
using System;

namespace CRM.Services
{
    public sealed class UserServices
    {
        private static List<Person> person;
        static List<GetMoney> requestsUser;
        static List<Massage> massages;
        public UserServices(List<Person> Person, List<GetMoney> RequestsUser, List<Massage> Massages)
        {
            person = Person;
            requestsUser = RequestsUser;
            massages = Massages;
        }
        public void DeleteUser(Guid Id)
        {
            int idx = person.FindIndex(x => x.Id.Equals(Id));
            person.Remove(person[idx]);
        }
        public void EditUser(InputUserDto EditUser, Guid Id)
        {
            int idx = person.FindIndex(x => x.Id.Equals(Id));
            if (!string.IsNullOrEmpty(EditUser.FirstName))
                person[idx].FirstName = EditUser.FirstName;
            if (!string.IsNullOrEmpty(EditUser.LastName))
                person[idx].LastName = EditUser.LastName;
            if (!string.IsNullOrEmpty(EditUser.Patronymic))
                person[idx].Patronymic = EditUser.Patronymic;
            if (!string.IsNullOrEmpty(EditUser.Age.ToString()))
                person[idx].Age = EditUser.Age;
            if (!string.IsNullOrEmpty(EditUser.Login))
                person[idx].Login = EditUser.Login;
            if (!string.IsNullOrEmpty(EditUser.Password))
                person[idx].Password = EditUser.Password;
        }

        public void GetMoney(InputUserDto DtoGetMoney, int index)
        {
            requestsUser.Add(new GetMoney
            {
                Id = Guid.NewGuid(),
                IdSender = DtoGetMoney.Id,
                Payday = DtoGetMoney.Payday,
                Name = person[index].FirstName,
                Age = person[index].Age,
                CountMoney = DtoGetMoney.AmountMoney,
                StatusDuty = StatusUser.Pending
            }) ;
        }

        public void DeleteDuty(Guid IdTransaction)
        {
            int idx = requestsUser.FindIndex(x => x.Id.Equals(IdTransaction));
            requestsUser.Remove(requestsUser[idx]);
        }

        public void SendMassage(InputUserDto MassageInformation)
        {
            massages.Add(new Massage {
                Id = Guid.NewGuid(),
                Name = MassageInformation.FirstName,
                Role = MassageInformation.Role,
                IdSender = MassageInformation.Id,
                Text = MassageInformation.Text,
                Theme = MassageInformation.Theme }) ;
        }
        public void PayTheDebtOff(Guid Id)
        {
            int idx = requestsUser.FindIndex(x => x.Id.Equals(Id));
            if (idx != -1)
                requestsUser.Remove(requestsUser[idx]);
            else
                throw new Exception("Request is not found");
        }
    }
}
