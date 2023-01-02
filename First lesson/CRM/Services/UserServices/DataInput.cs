using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;

namespace First_lesson.CRM.Services
{
    public partial class ClassMethods
    {
        public void DataInput(ref InputUserDto inputUserData)
        {
            Console.Write("\nFirst Name: ");
            inputUserData.FirstName = Console.ReadLine();
            Console.Write("Last Name: ");
            inputUserData.LastName = Console.ReadLine();
            Console.Write("Patronymic: ");
            inputUserData.Patronymic = Console.ReadLine();
            Console.Write("Data of Birth: ");
            inputUserData.DateOfBirth = DateTime.Parse(Console.ReadLine());
            Console.Write("Login: ");
            inputUserData.Login = Console.ReadLine();
            Console.Write("Password: ");
            inputUserData.Password = Console.ReadLine();
        }
    }
}
