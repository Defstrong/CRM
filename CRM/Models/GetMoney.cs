using First_lesson.CRM.Enums;
using System.Data;
using System.Runtime.CompilerServices;

namespace First_lesson.CRM.Models
{
    public class GetMoney
    {
        public int CountMoney { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public StatusUser StatusDuty { get; set; } = StatusUser.Pending;
        public DateTime Payday { get; set; }
        public string Print()
        {
            return $"Name: {Name}\nAge: {Age}\nPayday: {Payday.ToString("dd.MM.yyyy")}\n{CountMoney}$";
        }
    }
}
