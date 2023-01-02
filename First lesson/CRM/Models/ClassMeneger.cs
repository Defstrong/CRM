using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Services;

namespace First_lesson.CRM.Models
{
    public class ClassManager
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }

        public ClassManager(InputUserDto classInputUser, Roles role)
        {
            string date;
            FirstName = classInputUser.FirstName;
            LastName = classInputUser.LastName;
            Patronymic = classInputUser.Patronymic;
            DateOfBirth = classInputUser.DateOfBirth;
            Age = ClassMethods.age(DateOfBirth);
            Login = classInputUser.Login;
            Password = classInputUser.Password;
            Role = role;
        }
        public override string ToString()
        {
            return $"First Name: {FirstName}\nLast Name: {LastName}\nPatronymic:{Patronymic}\n" +
                    $"Age: {Age} \nDate of Birth: {DateOfBirth.ToString("dd.MM.yyyy")}\n";
        }
    }
}
