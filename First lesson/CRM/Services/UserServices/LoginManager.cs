using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;
using System;

namespace First_lesson.CRM.Services
{
    public partial class ClassMethods
    {
        public void LoginManager()
        {
            Console.WriteLine("\n\t\t\tMassages\tRequests\tSend massages");
            string massage;
            inputTeg = Console.ReadLine();
            if (inputTeg == "Send massages")
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
                    Name = "Manager",
                    RecipientName = userName,
                    Theme = theme,
                    Recipient = Roles.User,
                    SendMassage = massage
                }) ;
            }
            else if (inputTeg == "Massages")
            {
                int numberMassage = 0;
                for (int i = 0; i < ClassMethods.massages.Count; i++)
                {
                    if (ClassMethods.massages[i].Recipient == Roles.Manager)
                    {
                        numberMassage++;
                        Console.WriteLine($"{numberMassage}){ClassMethods.massages[i].Name}\tTheme: {ClassMethods.massages[i].Theme}");
                    }
                }
                Console.Write("\nEnter number: ");
                int index = int.Parse(Console.ReadLine());
                Console.WriteLine(ClassMethods.massages[index - 1].ToString());
            }
            else if (inputTeg == "Requests")
            {
                if (listRequestsForGetMoney.Count != 0)
                {
                    for (int i = 0; i < listRequestsForGetMoney.Count; i++)
                    {
                        Console.WriteLine($"\t\t{i + 1})");
                        listRequestsForGetMoney[i].Print();

                        Console.WriteLine("\nAccept\t\tRefuse");
                        inputTeg = Console.ReadLine();

                        if (inputTeg == "Accept")
                        {
                            listRequestsForGetMoney[i].StatusDuty = StatusUser.Accepted;
                            GetMoney.userRequests.Add(listRequestsForGetMoney[i]);
                            listRequestsForGetMoney.Remove(listRequestsForGetMoney[i]);
                        }
                        else if (inputTeg == "Refuse")
                        {
                            listRequestsForGetMoney[i].StatusDuty = StatusUser.Refuse;
                            GetMoney.userRequests.Add(listRequestsForGetMoney[i]);
                            listRequestsForGetMoney.Remove(listRequestsForGetMoney[i]);
                        }
                    }
                }
                else Console.WriteLine("\n\t\t\tYou dont have requests\n\n");
            }
        }
    }
}
