using First_lesson.CRM.Models;
using First_lesson.CRM.DTO;
using First_lesson.CRM.Services;

namespace First_lesson.CRM.Services
{
    public class CreatorServices
    {
        static ClassMethods CreatorMethods = new ClassMethods();
        public static List<User> usersList = new List<User>();

        public void GeneralAccessSetCreatorServices(ref List<User> UsersList)
        {
            usersList = UsersList;
        }

        public void LoginCreator(
            string inputLogin,
            InputUserDto creatorAction)
        {
            Console.WriteLine("\nDelete User\tEdit User");
            ClassMethods.inputTeg = Console.ReadLine();

            if (ClassMethods.inputTeg == "Delete User")
                DeleteUser(inputLogin);

            if (ClassMethods.inputTeg == "Edit User")
                EditUser(creatorAction);
        }
        public void DeleteUser(
            string inputLogin)
        {
            Console.Write("User Login: "); inputLogin = Console.ReadLine();

            var index = usersList.FindIndex(x => x.Login == inputLogin);

            if (index >= 0) usersList.Remove(usersList[index]);
            else Console.WriteLine("\nUser is not found");
        }

        public void EditUser(
            InputUserDto dtoForEditUser)
        {
            string inputLogin;
            Console.Write("User Login: "); inputLogin = Console.ReadLine();

            var index = usersList.FindIndex(x => x.Login == inputLogin);

            if (index >= 0)
            {
                CreatorMethods.DataInput(ref dtoForEditUser);

                if (dtoForEditUser.FirstName != string.Empty) usersList[index].FirstName = dtoForEditUser.FirstName;

                if (dtoForEditUser.LastName != string.Empty) usersList[index].LastName = dtoForEditUser.LastName;

                if (dtoForEditUser.Patronymic != string.Empty) usersList[index].Patronymic = dtoForEditUser.Patronymic;

                if (dtoForEditUser.DateOfBirth.ToString() != string.Empty) usersList[index].DateOfBirth = dtoForEditUser.DateOfBirth;

                if (dtoForEditUser.Login != string.Empty) usersList[index].Login = dtoForEditUser.Login;

                if (dtoForEditUser.Password != string.Empty) usersList[index].Password = dtoForEditUser.Password;
            }
            else Console.WriteLine("\nUser is not found\n");
        }
    }
}
