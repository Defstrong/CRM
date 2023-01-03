using First_lesson.CRM.Enums;

namespace First_lesson.CRM.Models
{
    public class Massage
    {
        public Guid Id { get; set; }
        public Guid IdSender { get; set; }
        public Guid IdRecipient { get; set; }
        public string Name { get; set; }
        public string Theme { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"\t\t\t{Name}\n{Text}";
        }
    }
}
