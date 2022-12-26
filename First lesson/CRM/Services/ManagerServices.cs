

using System.Runtime.CompilerServices;

namespace First_lesson.CRM.Services
{
    public class ManagerServices
    {
        public static List<ManagerServices> userRequests= new List<ManagerServices>();
        public int CountMoney { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string StatusDuty { get; set; }
        public void Print()
        {
            Console.WriteLine($"Name: {Name}\nAge: {Age}\n{CountMoney}$");
        }
    }
}
