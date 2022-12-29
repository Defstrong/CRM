using First_lesson.CRM.Models;
using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Services;

namespace First_lesson.CRM.Services
{
    public partial class ClassMethods
    {
        public ClassMethods(
            ref List<User> UsersList, 
            ref List<User> Requests, 
            ref List<ManagerServices> ListRequestsForGetMoney)
        {
            usersList = UsersList;
            requests = Requests;
            listRequestsForGetMoney = ListRequestsForGetMoney;
        }
        public ClassMethods() { }
        public void CreateAdmin()
        {
            DataInput(ref InputUserData);
            usersList.Add(new User(InputUserData, userRole = Roles.Admin));
        }
        string inputLogin, inputPassword;

        public void Login(
            ref InputUserDto loginDto)
        {
            var method = new ModerServices(ref requests, ref usersList);

            Console.WriteLine();
            Console.Write("Login: ");
            inputLogin = Console.ReadLine();
            Console.Write("Password: ");
            inputPassword = Console.ReadLine();

            if (inputLogin == creatorName && inputPassword == creatorPassword)
            {
                creatorServices.LoginCreator(inputLogin, loginDto);
                return;
            }

            var findAdminIdx = usersList.FindIndex(x => x.Login == inputLogin && x.Role == Roles.Admin);
            if (findAdminIdx >= 0)
            {
                LoginAdmin();
                return;
            }
            else if (inputLogin == moderName && inputPassword == moderPassword)
                ModerServices.ModerLogin();

            else if(inputLogin == loginManager && inputPassword == passwordManager)
                LoginManager();

            else
                LoginUser();
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
