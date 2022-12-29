using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;

namespace First_lesson.CRM.Services
{
    public partial class ClassMethods
    {
        public void LoginManager()
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
                        listRequestsForGetMoney[i].StatusDuty = "Accept";
                        ManagerServices.userRequests.Add(listRequestsForGetMoney[i]);
                        listRequestsForGetMoney.Remove(listRequestsForGetMoney[i]);
                    }
                    else if (inputTeg == "Refuse")
                    {
                        listRequestsForGetMoney[i].StatusDuty = "Refuse";
                        ManagerServices.userRequests.Add(listRequestsForGetMoney[i]);
                        listRequestsForGetMoney.Remove(listRequestsForGetMoney[i]);
                    }
                }
            }
            else Console.WriteLine("\n\t\t\tYou dont have requests\n\n");
        }
    }
}
