using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CRM.Services
{
    public partial class Autorization
    {
        public static List<Person> persons;
        public Guid Login(InputUserDto DtoLogin)
        {
            int idx = person.FindIndex(x => x.Login.Equals(DtoLogin.Login) && x.Password.Equals(DtoLogin.Password));
            if (person[idx].Status == StatusUser.Accepted)
            {
                DtoLogin.Role = person[idx].Role;
            }
            else if (person[idx].Status == StatusUser.Refuse)
                throw new Exception(person[idx].CauseRefuseRegistration);
            if (idx != -1)
                return person[idx].Id;
            else 
                throw new Exception("Eror 404\nUser is not found");
        }
    }
}
