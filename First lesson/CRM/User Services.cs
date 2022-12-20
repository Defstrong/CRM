using Class_User;
using UserRol;
using Crm.InputUser.Dto;


namespace UserServices
{
    public class ClassMethods
    {
        static public InputUserDto InputUserData = new InputUserDto();

        private const string creatorName = "Creator";
        private const string creatorPassword = "Creator";

        public static string inputTeg;

        private const string moderName = "Moder";
        private const string moderPassword = "Moder";
        string userRole;

        public void CreateAdmin(List<User> createAdminInUserList)
        {
            DataInput(ref InputUserData);

            createAdminInUserList.Add(new User(InputUserData, userRole = Rols.Admin.ToString()));
        }

        public void DataInput( ref InputUserDto inputUserData )
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
            ref InputUserDto loginDto,
            ref List<User> usersList, 
            ref List<User> requests)
        {
            Console.WriteLine();
            Console.Write("Login: ");
            inputLogin = Console.ReadLine();
            Console.Write("Password: ");
            inputPassword = Console.ReadLine();

            var findAdminIdx = usersList.FindIndex(x => x.Login == inputLogin && x.Role == "Admin");
            if (inputLogin == creatorName && inputPassword == creatorPassword)
            {
                LoginCreator(inputLogin, loginDto, usersList);
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
                if (requests.Count == 0)
                    Console.WriteLine("\t\t\tNo requests\n\n");
                else
                {
                    Console.WriteLine("\t\t\tList of registration requests");
                    for (int i = 0; i < requests.Count; i++)
                    {
                        Console.WriteLine($"\t\t{i + 1})");
                        Console.WriteLine(requests[i].ToString());

                        Console.WriteLine("\nAccept\t\tRefuse");
                        inputTeg = Console.ReadLine();

                        if (inputTeg == "Accept")
                        {
                            usersList.Add(requests[i]);
                            requests.Remove(requests[i]);
                        }
                    }
                }

            }
            else
            {
                Console.WriteLine("User");
                var index = usersList.FindIndex(x => x.Login == inputLogin);

                if (index >= 0 && inputPassword == usersList[index].Password) Console.WriteLine(usersList[index].ToString());
                else Console.WriteLine("User is not found");
            }
        }


        public void LoginCreator(
            string inputLogin, 
            InputUserDto creatorAction,
            List<User> creatorList)
        {
            Console.WriteLine("\nDelete User\tEdit User");
            ClassMethods.inputTeg = Console.ReadLine();

            if (ClassMethods.inputTeg == "Delete User")
                DeleteUser(inputLogin, creatorList);

            if (ClassMethods.inputTeg == "Edit User")
                EditUser(creatorAction, creatorList);
        }

        public void DeleteUser(
            string inputLogin, 
            List<User> deleteUser)
        {
            Console.Write("User Login: "); inputLogin = Console.ReadLine();

            var index = deleteUser.FindIndex(x => x.Login == inputLogin);

            if (index >= 0) deleteUser.Remove(deleteUser[index]);
            else Console.WriteLine("\nUser is not found");
        }

        public void EditUser(
            InputUserDto dtoForEditUser, 
            List<User> editList)
        {
            string inputLogin;
            Console.Write("User Login: "); inputLogin = Console.ReadLine();

            var index = editList.FindIndex(x => x.Login == inputLogin);

            if (index >= 0)
            {
                DataInput(ref dtoForEditUser);

                if (dtoForEditUser.FirstName != string.Empty) editList[index].FirstName = dtoForEditUser.FirstName;

                if (dtoForEditUser.LastName != string.Empty) editList[index].LastName = dtoForEditUser.LastName;

                if (dtoForEditUser.Patronymic != string.Empty) editList[index].Patronymic = dtoForEditUser.Patronymic;

                if (dtoForEditUser.ToString() != string.Empty) editList[index].DateOfBirth = dtoForEditUser.DateOfBirth;

                if (dtoForEditUser.Login != string.Empty) editList[index].Login = dtoForEditUser.Login;

                if (dtoForEditUser.Password != string.Empty) editList[index].Password = dtoForEditUser.Password;
            }
            else Console.WriteLine("\nUser is not found\n");
        }

        public void Registration( 
            ref InputUserDto dtoRegistration,
            ref List<User> registrationRequests)
        {
            DataInput(ref dtoRegistration);

            registrationRequests.Add(new User(dtoRegistration, userRole = "User"));
        }
        public static int age(DateTime birthDate)
        {
            DateTime today = DateTime.Today;

            int Age = today.Year - birthDate.Year;
            if (birthDate.AddYears(Age) > today)
            {
                Age--;
            }
            return Age;
        }
    }
}
