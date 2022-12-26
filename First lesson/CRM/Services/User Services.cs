using First_lesson.CRM.Models;
using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Services;

namespace First_lesson.CRM.Services
{
    public class ClassMethods
    {
        static public InputUserDto InputUserData = new InputUserDto();
        static public CreatorServices creatorServices = new CreatorServices();
        private const string creatorName = "Creator";
        private const string creatorPassword = "Creator";

        private const string loginManager = "Manager";
        private const string passwordManager = "Manager";

        public static string inputTeg;
        private const string moderName = "Moder";
        private const string moderPassword = "Moder";
        private static int numberRequest;
        string userRole = string.Empty;

        public static List<ManagerServices> listRequestsForGetMoney = new List<ManagerServices>();
        public static List<User> usersList = new List<User>();
        public static List<User> requests = new List<User>();
        public static List<User> refuseRequests = new List<User>();


        public void GeneralAccessSetUserServices(
            ref List<User> UsersList, 
            ref List<User> Requests, 
            ref List<User> refuseRequests, 
            ref List<ManagerServices> ListRequestsForGetMoney)
        {
            usersList = UsersList;
            requests = Requests;
            refuseRequests = refuseRequests;
            listRequestsForGetMoney = ListRequestsForGetMoney;
        }

        public void CreateAdmin()
        {
            DataInput(ref InputUserData);

            usersList.Add(new User(InputUserData, userRole = Rols.Admin.ToString()));
        }

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

        public void Login(
            string inputLogin,
            string inputPassword,
            ref InputUserDto loginDto)
        {
            ModerServices.GeneralAccessForModer(ref requests, ref usersList, ref refuseRequests);

            Console.WriteLine();
            Console.Write("Login: ");
            inputLogin = Console.ReadLine();
            Console.Write("Password: ");
            inputPassword = Console.ReadLine();

            var findAdminIdx = usersList.FindIndex(x => x.Login == inputLogin && x.Role == "Admin");
            if (inputLogin == creatorName && inputPassword == creatorPassword)
            {
                creatorServices.LoginCreator(inputLogin, loginDto);
                return;
            }
            if (findAdminIdx >= 0)
            {
                Console.WriteLine("\nShow users\tShow user");
                string show = Console.ReadLine();
                Console.WriteLine();
                if (show == "Show users")
                {
                    foreach (var ii in usersList)
                    {
                        if (ii.Role == "User")
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
                return;
            }
            else if (inputLogin == moderName && inputPassword == moderPassword)
            {
                ModerServices.ModerLogin();
            }
            else if(inputLogin == loginManager && inputPassword == passwordManager)
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
            else
            {
                var indexRefuse = refuseRequests.FindIndex(x => x.Login == inputLogin);
                var index = usersList.FindIndex(x => x.Login == inputLogin);
                if (indexRefuse >= 0)
                    Console.WriteLine($"\n\t\t{refuseRequests[indexRefuse].UserStatus}\n");

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

                        listRequestsForGetMoney.Add(new ManagerServices { 
                            Name = usersList[index].FirstName, 
                            Age = usersList[index].Age, 
                            CountMoney = countMoneyForGet });
                    }
                    else if (inputUserAction == "Personal area")
                    {
                        usersList[index].ToString();
                    }
                    else if(inputUserAction == "Status dutys")
                    {
                        int idx = 1;
                        foreach(var ii in ManagerServices.userRequests)
                        {
                            Console.WriteLine($"\t\t\t{idx++}){ii.StatusDuty}");
                            ii.Print();
                        }
                    }
                    else if(inputUserAction == "Delete dutys")
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

        public void Registration(
            ref InputUserDto dtoRegistration)
        {
            DataInput(ref dtoRegistration);

            requests.Add(new User(dtoRegistration, userRole = "User"));
        }

        public static int age(DateTime birthDate)
        {
            DateTime today = DateTime.Today;

            int Age = today.Year - birthDate.Year;
            if (birthDate.AddYears(Age) > today)
                Age--;
            return Age;
        }
    }
}
