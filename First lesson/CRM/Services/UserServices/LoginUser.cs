using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;

namespace First_lesson.CRM.Services
{
    public partial class ClassMethods
    {
        public void LoginUser()
        {
            var indexRefuse = usersList.FindIndex(x => x.Login == inputLogin && x.Status == StatusUser.Refuse);
            var index = usersList.FindIndex(x => x.Login == inputLogin);
            if (indexRefuse >= 0)
                Console.WriteLine($"\n\t\t{usersList[indexRefuse].UserStatus}\n");

            else if (index >= 0 && inputPassword == usersList[index].Password)
            {
                string inputUserAction = string.Empty;
                int countMoneyForGet;

                Console.WriteLine("\n\t\tGet money\tPersonal area\tStatus dutys\tDelete dutys\n");
                inputUserAction = Console.ReadLine();
                if (inputUserAction == "Get money")
                {
                    Console.WriteLine("\t\tHow much money do you want?\n");
                    Console.Write("Enter the amount of money:");
                    checked
                    {
                        countMoneyForGet = int.Parse(Console.ReadLine());
                    }

                    listRequestsForGetMoney.Add(new ManagerServices
                    {
                        Name = usersList[index].FirstName,
                        Age = usersList[index].Age,
                        CountMoney = countMoneyForGet
                    });
                }
                else if (inputUserAction == "Personal area")
                {
                    Console.WriteLine(usersList[index].ToString());
                }
                else if (inputUserAction == "Status dutys")
                {
                    int idx = 1;
                    foreach (var ii in ManagerServices.userRequests)
                    {
                        Console.WriteLine($"\t\t\t{idx++}){ii.StatusDuty}");
                        ii.Print();
                    }
                }
                else if (inputUserAction == "Delete dutys")
                {
                    int idx = 1;
                    foreach (var ii in ManagerServices.userRequests)
                    {
                        Console.WriteLine($"\t\t\t{idx++}){ii.StatusDuty}");
                        ii.Print();
                    }

                    Console.Write("Enter dutu number: ");
                    numberRequest = int.Parse(Console.ReadLine());
                    ManagerServices.userRequests.Remove(ManagerServices.userRequests[numberRequest - 1]);

                }

            }
            else Console.WriteLine("User is not found");
        }
    }
}
