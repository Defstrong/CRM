using First_lesson.CRM.Models;
using First_lesson.CRM.Enums;
using System.Runtime.CompilerServices;

namespace CRM.Services
{
    public sealed class ModeratorServices
    {
        private static List<Person> person;
        static List<GetMoney> requestsUser;
        static List<Massage> massages;
        public ModeratorServices(List<Person> Person, List<GetMoney> RequestsUser, List<Massage> Massages)
        {
            person = Person;
            requestsUser = RequestsUser;
            massages = Massages;
        }
        public void ChoiceRequest(int idx, string choice)
        {
            if (choice == "Accepted")
                person[idx].Status = StatusUser.Accepted;
            else
                person[idx].Status = StatusUser.Refuse;
        }
    }
}
