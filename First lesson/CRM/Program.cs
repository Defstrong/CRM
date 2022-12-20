using Class_User;
using Crm.InputUser.Dto;
using UserRol;
using UserServices;


var _usersList = new List<User>();
var _requests = new List<User>();

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
    Login = "AdminBob", 
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

var Method = new ClassMethods();

while (++i < 100)
{
    Console.WriteLine("\t\t\tTEGS");
    Console.WriteLine("Registration\tLogin\tGuest\tCreate Admin\tExit");
    ClassMethods.inputTeg = Console.ReadLine();

    if (ClassMethods.inputTeg == "Exit") break;

    if (ClassMethods.inputTeg == "Registration")
        Method.Registration(ref _inputDto, ref _requests);

    if (ClassMethods.inputTeg == "Login")
        Method.Login(
            _inputLogin,
            _inputPassword,
            ref _inputDto, 
            ref _usersList, 
            ref _requests);

    if (ClassMethods.inputTeg == "Guest")
    {
        Console.WriteLine("\t\t\t\tAll funktion\n");
        Console.WriteLine("1) Registration \t 2) Login\t 3) View profile\t 4)Create Administrator" +
        "\n\t\t4) Show user \t 5) Show user \t 6)Delete user\tEdit User\n\n");
    }

    if (ClassMethods.inputTeg == "Create Admin")
        Method.CreateAdmin(_usersList);


}
