using CRM.DTO;
using CRM.Services;
using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;
using System.Diagnostics.Tracing;

var _personsList = new List<Person>();
var _personsRequestsList = new List<GetMoney>();
var _massage = new List<Massage>();
var write = new Write();
var _autorization = new Autorization(_personsList, _personsRequestsList, _massage);
var _createAdmin = new CreateAdmin(_personsList, _personsRequestsList, _massage);
_personsList.Add(new Person
{
    FirstName = "Bob",
    LastName = "Bob",
    Age = 20,
    Patronymic = "Bob",
    Login = "Bob",
    Password = "1",
    Role = Roles.User,
    Status = StatusUser.Accepted
});
_personsList.Add(new Person{ 
    FirstName = "Admin", 
    LastName = "Admin", 
    Age = 20, 
    Patronymic = "Admin", 
    Login = "Admin", 
    Password = "Admin", 
    Role = Roles.Admin, 
    Status = StatusUser.Accepted});
_personsList.Add(new Person
{
    FirstName = "Moderator",
    LastName = "Moderator",
    Age = 20,
    Patronymic = "Moderator",
    Login = "Moderator",
    Password = "Moderator",
    Role = Roles.Moderator,
    Status = StatusUser.Accepted
});
_personsList.Add(new Person
{
    FirstName = "Creator",
    LastName = "Creator",
    Age = 20,
    Patronymic = "Creator",
    Login = "Creator",
    Password = "Creator",
    Role = Roles.Creator,
    Status = StatusUser.Accepted
});
_personsList.Add(new Person
{
    FirstName = "Manager",
    LastName = "Manager",
    Age = 20,
    Patronymic = "Manager",
    Login = "Manager",
    Password = "Manager",
    Role = Roles.Manager,
    Status = StatusUser.Accepted
});

var _createModerator = new CreateModerator(_personsList, _personsRequestsList, _massage);
var massage = new Massage();
var dutys = new GetMoney();
int i = 1;
var dto = new InputUserDto();

while(i++ < 100)
{
    dto = new InputUserDto();
    string inputCommand = Command(inputCommand = string.Empty);
    if (inputCommand == "Registration")
        Registration();
    else if (inputCommand == "Create Admin")
        CreateAdmin();
    else if (inputCommand == "Login")
    {
        Login(ref dto); Password(ref dto);
        int idxCheckAcceptedUser = FindIndex(dto.Login);
        inputCommand = string.Empty;
        if (_personsList[idxCheckAcceptedUser].Status == StatusUser.Accepted)
            inputCommand = Command(inputCommand = string.Empty);
        else
            throw new Exception("Sorry but your request not Accepted");
        if (inputCommand == "Get money")
        {
            AmounMoney(); Payday();
        }
        else if (inputCommand == "Personal area")
            PersonalArea(dto.Login);
        else if (inputCommand == "Show user")
            FirstName(ref dto);



        _autorization.Login(dto, massage, inputCommand);
    }

}    


void MyMassages(string Login)
{
    int idx = FindIndex(Login);
    int number = 0;
    for(int i = 0; i < _massage.Count; i++)
    {
        if (_massage[i].RecipientName == _personsList[idx].FirstName)
        {
            number++;
            write.WriteMassage(number, _massage[i]);
        }
    }
}
void Registration()
{
    FirstName(ref dto); LastName(ref dto);
    Patronymic(ref dto); DateOfBirth(ref dto);
    Login(ref dto); Password(ref dto);
    _autorization.Registration(dto);
}
void CreateAdmin()
{
    FirstName(ref dto); LastName(ref dto);
    Patronymic(ref dto); DateOfBirth(ref dto);
    Login(ref dto); Password(ref dto);
    _createAdmin.Registration(dto);
}
void CreateModerator()
{
    FirstName(ref dto); LastName(ref dto);
    Patronymic(ref dto); DateOfBirth(ref dto);
    Login(ref dto); Password(ref dto);
    _createModerator.Registration(dto);
}
void PersonalArea(string Login)
{
    Console.WriteLine(_personsList[FindIndex(Login)].ToString());
}
void FirstName(ref InputUserDto inputUserDtoFirstName)
{
    Console.Write("First name: ");
    inputUserDtoFirstName.FirstName = Console.ReadLine();
}
void LastName(ref InputUserDto inputUserDtoLastName)
{
    Console.Write("Last name: ");
    inputUserDtoLastName.LastName = Console.ReadLine();
}
void Patronymic(ref InputUserDto inputUserDtoPatronymic)
{
    Console.Write("Patronymic: ");
    inputUserDtoPatronymic.Patronymic = Console.ReadLine();
}
void DateOfBirth(ref InputUserDto inputUserDtoDateOfBirth)
{
    Console.Write("Date of birth: ");
    inputUserDtoDateOfBirth.DateOfBirth = DateTime.Parse(Console.ReadLine());
}
void Login(ref InputUserDto inputUserDtoLogin)
{
    Console.Write("Login: ");
    inputUserDtoLogin.Login = Console.ReadLine();
}
void Password(ref InputUserDto inputUserDtoPassword)
{
    Console.Write("Password: ");
    inputUserDtoPassword.Password = Console.ReadLine();
}
void AmounMoney()
{
    Console.Write("Amoun money: ");
    dto.AmountMoney = int.Parse(Console.ReadLine());
}
void Payday()
{
    Console.Write("Payday: ");
    dto.Payday = DateTime.Parse(Console.ReadLine());
}
string Command(string command)
{
    Console.Write("Enter your command:> ");
    command = Console.ReadLine();
    return command;
}
void SendMassage(ref Massage sendMassage)
{
    Console.Write("Enter your massage: ");
    sendMassage.SendMassage = Console.ReadLine();
    Console.Write("Enter theme massage: ");
    sendMassage.Theme = Console.ReadLine();
    Console.Write("Recipient: ");
    string role = Console.ReadLine();
    if (role == "Admin")
        sendMassage.Recipient = Roles.Admin;
    else if (role == "Moderator")
        sendMassage.Recipient = Roles.Moderator;
}
int FindIndex(string Login)
{
    int idx = _personsList.FindIndex(x => x.Login.Equals(Login));
    return idx;
}