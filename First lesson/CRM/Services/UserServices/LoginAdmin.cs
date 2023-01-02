using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;

namespace First_lesson.CRM.Services
{
    public partial class ClassMethods
    {
        public void LoginAdmin()
        {
            Console.WriteLine("\nShow users\tShow user\tSend massage\tMassages");
            string show = Console.ReadLine();
            Console.WriteLine();
            if (inputTeg == "Massages")
            {
                int numberMassage = 0;
                for (int i = 0; i < ClassMethods.massages.Count; i++)
                {
                    if (ClassMethods.massages[i].Recipient == Roles.Admin)
                    {
                        numberMassage++;
                        Console.WriteLine($"{numberMassage}){ClassMethods.massages[i].Name}\tTheme: {ClassMethods.massages[i].Theme}");
                    }
                }
                Console.Write("\nEnter number: ");
                int index = int.Parse(Console.ReadLine());
                Console.WriteLine(ClassMethods.massages[index - 1].ToString());
            }
            else if (show == "Send massage")
            {
                string theme, userName;
                Console.Write("Enter your massage: ");
                massage = Console.ReadLine();
                Console.Write("Enter theme: ");
                theme = Console.ReadLine();
                Console.Write("Enter user name: ");
                userName = Console.ReadLine();
                massages.Add(new Massage
                {
                    Name = "Admin",
                    RecipientName = userName,
                    Theme = theme,
                    Recipient = Roles.User,
                    SendMassage = massage
                });
            }
            else if (show == "Show users")
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
