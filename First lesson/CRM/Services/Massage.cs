using First_lesson.CRM.Enums;

namespace First_lesson.CRM.Services
{
    public class Massage
    {
        public string Name { get; set; }
        public string RecipientName { get; set; }
        public Roles Recipient { get; set; }
        public string Theme { get; set; }
        public string SendMassage { get; set; }

        public override string ToString()
        {
            return $"\t\t\t{Name}\n{SendMassage}";
        }
    }
}
