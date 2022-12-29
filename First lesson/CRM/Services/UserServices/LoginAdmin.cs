using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;

namespace First_lesson.CRM.Services
{
    public partial class ClassMethods
    {
        public void LoginAdmin()
        {
            Console.WriteLine("\nShow users\tShow user");
            string show = Console.ReadLine();
            Console.WriteLine();
            if (show == "Show users")
            {
                foreach (var ii in usersList)
                {
                    if (ii.Role == Roles.User)
                        Console.WriteLine(ii.ToString());
                }
            }
            else if (show == "Show User")
            {
                Console.Write("Name User: ");
                inputLogin = Console.ReadLine();

                var index = usersList.FindIndex(x => x.Login == inputLogin);

                if (index >= 0) Console.WriteLine(usersList[index].ToString());
                else Console.WriteLine("\nUser is not found");
            }
            else
                Console.WriteLine("\nEror\n");
        }
    }
}
