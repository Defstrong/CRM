using First_lesson.CRM.DTO;
using First_lesson.CRM.Services;
using First_lesson.CRM.Models;
using First_lesson.CRM.Services;
using First_lesson.CRM.Enums;

var _usersRequestsForGetMoney = new List<ManagerServices>();
var _usersList = new List<User>();
var _requests = new List<User>();
var _refuseRequests = new List<User>();



var _creatorServices = new CreatorServices();
var Method = new ClassMethods();
var _inputDto = new InputUserDto();


_usersList.Add(new User(
    new InputUserDto { 
    FirstName = "Admin", 
    LastName = "Tolman", 
    Patronymic = "Jeyson", 
    Age = 22, 
    DateOfBirth = new DateTime(2000, 12, 4), 
    Login = "Admin", 
    Password = "admin" }, 
    Rols.Admin.ToString()));

_usersList.Add(new User(
    new InputUserDto { 
    FirstName = "Bob", 
    LastName = "Grengford", 
    Patronymic = "Grand", 
    Age = 18, 
    DateOfBirth = new DateTime(2004, 08, 12), 
    Login = "Bob", 
    Password = "1111" }, 
    Rols.User.ToString()));

_usersList.Add(new User(
    new InputUserDto
{
    FirstName = "Alis",
    LastName = "Grand",
    Patronymic = "Franklin",
    Age = 19,
    DateOfBirth = new DateTime(2003, 08, 12),
    Login = "Alis",
    Password = "1111"
},
    Rols.User.ToString()));

var _inputFirstName = string.Empty;
var _inputLastName = string.Empty;
var _inputPatronymic = string.Empty;
int _inputAge = 0;
DateTime _inputDateOfBirth;
var _inputLogin = string.Empty;
var _inputPassword = string.Empty;
var _role = string.Empty;
int i = 0;


while (ClassMethods.inputTeg != "Exit")
{
    _creatorServices.GeneralAccessSetCreatorServices(ref _usersList);

    Method.GeneralAccessSetUserServices(
        ref _usersList,
        ref _requests,
        ref _refuseRequests,
        ref _usersRequestsForGetMoney
        ) ;

    Console.WriteLine("\t\t\tTEGS");
    Console.WriteLine("Registration\tLogin\tGuest\tCreate Admin\t Count accepted users\t Count refuse users\tExit");
    ClassMethods.inputTeg = Console.ReadLine();

    if(ClassMethods.inputTeg == "Count accepted users")
        Console.WriteLine($"\n\t\t\tCount accepter users: {ModerServices.countAcceptRequest}\n");

    if (ClassMethods.inputTeg == "Count refuse users")
        Console.WriteLine($"\n\t\t\tCount refuse users: {ModerServices.countRefuseRequest}\n");

    if (ClassMethods.inputTeg == "Registration")
        Method.Registration(ref _inputDto);

    if (ClassMethods.inputTeg == "Login")
    {
        Method.Login(
            _inputLogin,
            _inputPassword,
            ref _inputDto);
    }

    if (ClassMethods.inputTeg == "Guest")
    {
        Console.WriteLine("\t\t\t\tAll funktion\n");
        Console.WriteLine("1) Registration \t 2) Login\t 3) View profile\t 4)Create Administrator" +
        "\n\t\t4) Show user \t 5) Show user \t 6)Delete user\tEdit User\n\n");
    }

    if (ClassMethods.inputTeg == "Create Admin")
        Method.CreateAdmin();

}
