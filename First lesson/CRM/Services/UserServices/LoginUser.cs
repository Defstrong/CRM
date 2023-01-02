using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;
using System.Data;

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

                Console.WriteLine("\n\t\tGet money\tPersonal area\tStatus dutys\tDelete dutys\tSend massage\tMy massages\nPay the debt off\r\n\n");
                inputUserAction = Console.ReadLine();
                int numberMassage = 0;
                if(inputUserAction == "My massages")
                {
                    for (int i = 0; i < ClassMethods.massages.Count; i++)
                    {
                        if (ClassMethods.massages[i].Recipient == Roles.User && ClassMethods.massages[i].RecipientName == usersList[index].FirstName)
                        {
                            numberMassage++;
                            Console.WriteLine($"{numberMassage}){ClassMethods.massages[i].Name}\tTheme: {ClassMethods.massages[i].Theme}");
                        }
                    }
                    Console.Write("\nEnter number: ");
                    index = int.Parse(Console.ReadLine());
                    Console.WriteLine(ClassMethods.massages[index - 1].ToString());
                }
                else if (inputUserAction == "Get money")
                {
                    Console.WriteLine("\t\tHow much money do you want?\n");
                    Console.Write("Enter the amount of money:");
                    checked
                    {
                        countMoneyForGet = int.Parse(Console.ReadLine());
                    }
                    Console.Write("Enter payday: ");
                    DateTime Payday = DateTime.Parse(Console.ReadLine());

                    listRequestsForGetMoney.Add(new GetMoney
                    {
                        Payday = Payday,
                        Name = usersList[index].FirstName,
                        Age = usersList[index].Age,
                        CountMoney = countMoneyForGet
                    }) ;
                }
                else if(inputUserAction == "Send massage")
                {
                    string theme;
                    Console.WriteLine("\n\t\tManager\t\tAdmin");
                    inputUserAction = Console.ReadLine();

                    massage = Console.ReadLine();
                    Console.Write("Enter theme: ");
                    theme = Console.ReadLine();
                    if (inputUserAction == "Manager")
                        massages.Add(new Massage { 
                            Name = usersList[index].FirstName,
                            Theme = theme,
                            Recipient = Roles.Manager, 
                            SendMassage = massage });

                    else if(inputUserAction == "Admin")
                        massages.Add(new Massage { 
                            Name = usersList[index].FirstName,
                            Theme = theme,
                            Recipient = Roles.Admin, 
                            SendMassage = massage });
                }
                else if (inputUserAction == "Personal area")
                    Console.WriteLine(usersList[index].ToString());

                else if (inputUserAction == "Status dutys")
                {
                    int idx = 1;
                    foreach (var ii in GetMoney.userRequests)
                    {
                        Console.WriteLine($"\t\t\t{idx++}){ii.StatusDuty.ToString()}");
                        if(ii.StatusDuty == StatusUser.Accepted)
                        {
                            if(ii.Payday >= DateTime.Now)
                            {
                                int idxPayUser = usersList.FindIndex(x => x.FirstName == ii.Name);
                                massages.Add(new Massage
                                {
                                    Name = Roles.Manager.ToString(),
                                    SendMassage = $"Уважаемый клиент " +
                                    $"{usersList[idxPayUser].FirstName} " +
                                    $"{usersList[idxPayUser].LastName} " +
                                    $"{usersList[idxPayUser].Patronymic}, " +
                                    $"Вы не погасили свой долг, " +
                                    $"пожалуйста в течении следующих трех дней. ",
                                    RecipientName = ii.Name,
                                    Theme = "Payday",
                                    Recipient = Roles.User
                                });
                            }
                        }
                        ii.Print();
                    }
                }
                else if(inputUserAction == "Pay the debt off")
                {
                    int idx = 1;
                    foreach (var ii in GetMoney.userRequests)
                    {
                        Console.WriteLine($"\t\t\t{idx++}){ii.StatusDuty}");
                        ii.Print();
                    }

                    Console.Write("Enter dutu number: ");
                    numberRequest = int.Parse(Console.ReadLine());
                    GetMoney.userRequests.Remove(GetMoney.userRequests[numberRequest - 1]);
                }
                else if (inputUserAction == "Delete dutys")
                {
                    int idx = 1;
                    foreach (var ii in GetMoney.userRequests)
                    {
                        Console.WriteLine($"\t\t\t{idx++}){ii.StatusDuty}");
                        ii.Print();
                    }

                    Console.Write("Enter dutu number: ");
                    numberRequest = int.Parse(Console.ReadLine());
                    GetMoney.userRequests.Remove(GetMoney.userRequests[numberRequest - 1]);
                }

            }
            else Console.WriteLine("User is not found");
        }
    }
}
