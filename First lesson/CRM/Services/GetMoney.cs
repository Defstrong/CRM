using First_lesson.CRM.Enums;
using System.Data;
using System.Runtime.CompilerServices;

namespace First_lesson.CRM.Services
{
    public class GetMoney
    {
        public static List<GetMoney> userRequests= new List<GetMoney>();
        public int CountMoney { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public StatusUser StatusDuty { get; set; }
        public DateTime Payday { get; set; }
        public void Print()
        {
            Console.WriteLine($"Name: {Name}\nAge: {Age}\nPayday: {Payday.ToString("dd.MM.yyyy")}\n{CountMoney}$");
        }
    }
}
