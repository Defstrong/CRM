using First_lesson.CRM.DTO;
using First_lesson.CRM.Models;
using First_lesson.CRM.Enums;
using CRM.DTO;

namespace CRM.Services
{
    public partial class Autorization
    {
        private static List<Person> person;
        static List<GetMoney> requestsUser;
        static List<Massage> massages;
        private static Write write = new Write();
        public Autorization(List<Person> Person, List<GetMoney> RequestsUser, List<Massage> Massages)
        {
            person = Person;
            requestsUser = RequestsUser;
            massages = Massages;
        }
        public void Registration(InputUserDto Data)
        {
            person.Add(new Person(Data, Roles.User) { Status = StatusUser.Pending, Id = Guid.NewGuid() }) ;
        }

    }
}
