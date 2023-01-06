using CRM.DTO;
using CRM.Models;
using CRM.Services;
using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Diagnostics.Tracing;

var _personsList = new List<Person>();
var _personsRequestsList = new List<GetMoney>();
var _massages = new List<Massage>();
var write = new Write();
var _autorization = new Autorization(_personsList, _personsRequestsList, _massages);
var _createAdmin = new CreateAdmin(_personsList, _personsRequestsList, _massages);
var _userServices = new UserServices(_personsList,_personsRequestsList, _massages);
var _adminServices = new AdminServices(_personsList,_personsRequestsList, _massages);
var _moderatorServices = new ModeratorServices(_personsList,_personsRequestsList, _massages);
var _managerServices = new ManagerServices(_personsList,_personsRequestsList, _massages);
var _allStatistic = new Statistic();

int countAcceptedRequestRegistration = 0, countRefuseRequestRegistration = 0;

_personsList.Add(new Person
{
    FirstName = "Bob",
    LastName = "Bob",
    Age = 20,
    Patronymic = "Bob",
    Login = "Bob",
    Password = "1",
    Role = Roles.User,
    Status = StatusUser.Accepted,
    Id = Guid.NewGuid()
});;
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
    FirstName = "Manager",
    LastName = "Manager",
    Age = 20,
    Patronymic = "Manager",
    Login = "Manager",
    Password = "Manager",
    Role = Roles.Manager,
    Status = StatusUser.Accepted
});

var _createModerator = new CreateModerator(_personsList, _personsRequestsList, _massages);
var massage = new Massage();
var dutys = new GetMoney();
int i = 1;
var dto = new InputUserDto();

while(i++ < 100)
{
    WriteCountStatusRequestRegistration(_allStatistic.countAcceptedRequestRegistration, "accepted");
    WriteCountStatusRequestRegistration(_allStatistic.countRefuseRequestRegistration, "refuse");
    dto = new InputUserDto();
    string inputCommand = string.Empty; 
    Command(ref inputCommand);
    if (inputCommand == "Registration")
        Registration();
    else if (inputCommand == "Login")
    {
        Login(ref dto); Password(ref dto);
        Guid idPerson = _autorization.Login(dto);
        Command(ref inputCommand);
        if (dto.Role == Roles.User)
            UserAction(inputCommand, idPerson);
        else if (dto.Role == Roles.Admin)
            AdminAction(inputCommand, idPerson);
        else if (dto.Role == Roles.Moderator)
            ModeratorAction(inputCommand);
        else if (dto.Role == Roles.Manager)
            ManagerAction(inputCommand, idPerson);
    }

}
void UserAction(string CommandUser, Guid UserId)
{
    int idx = FindIndex(UserId);
    _managerServices.OverdueRequest();
    dto.Id = UserId;
    if (CommandUser == "Get money")
    {
        AmounMoney(); Payday();
        _userServices.GetMoney(dto, idx);
    }
    else if (CommandUser == "Status dutys")
        StatusDuty(UserId);
    else if (CommandUser == "Personal area")
        PersonalArea(idx);
    else if (CommandUser == "My massages")
        MyMassages(UserId);
    else if (CommandUser == "Delete profile")
        _userServices.DeleteUser(UserId);
    else if (CommandUser == "Edit profile")
        EditUser(UserId);
    else if (CommandUser == "Delete duty")
        DeleteDuty(UserId);
    else if (CommandUser == "Send massage")
        UserSendMassage(idx);
    else if (CommandUser == "Debt off")
        PayTheDebtOff(UserId);


}
// User Methods
void PayTheDebtOff(Guid UserId)
{
    ShowAllIdUserRequestGetMoney(UserId);
    _userServices.PayTheDebtOff(EnterIdRequestForTheDebtOff());
}
Guid EnterIdRequestForTheDebtOff()
{
    Console.Write("Please enter Id request: ");
    Guid IdRequest = Guid.Parse(Console.ReadLine());

    return IdRequest;
}
void ShowAllIdUserRequestGetMoney(Guid UserId)
{
    foreach (var ii in _personsRequestsList)
    {
        if (ii.IdSender == UserId && ii.StatusDuty == StatusUser.Accepted)
        {
            Console.WriteLine(ii.Id);
            write.Status(ii.StatusDuty);
            write.WriteDutys(ii.ToString());
        }
    }
}
void StatusDuty(Guid UserId)
{
    foreach (var ii in _personsRequestsList)
    {
        if (ii.IdSender == UserId && ii.StatusDuty != StatusUser.Pending)
        {
            write.Status(ii.StatusDuty);
            write.WriteDutys(ii.ToString());
        }
    }
}
void PersonalArea(int idx)
{
    if (_personsList[idx].Status == StatusUser.Accepted)
        Console.WriteLine(_personsList[idx].ToString());
    else
        throw new Exception("User is not found");
}
void MyMassages(Guid Id)
{
    ShowAllIdMassagesForUser(Id);
    bool b = false;
    int idx = _massages.FindIndex(x => x.IdSender.Equals(Id));
    foreach(var ii in _massages)
    {
        if(ii.IdRecipient == Id)
        {
            Console.WriteLine(ii.ToString());
            b = true;
        }
    }
    if(b)
        ShowMassage();
    else
        Console.WriteLine("\nYou dot't have massages\n");
}

void ShowMassage()
{
    Guid IdMassage = Guid.Parse(Console.ReadLine());
    int idx = _massages.FindIndex(x => x.Id.Equals(IdMassage));
    Console.WriteLine($"\n{_massages[idx].ToString()}\n{_massages[idx].Text}");
}
void ShowAllIdMassagesForUser(Guid Id)
{
    int idx = _massages.FindIndex(x => x.IdSender.Equals(Id));
    foreach (var ii in _massages)
    {
        if (ii.IdRecipient == Id)
            Console.WriteLine(ii.Id);
    }
}
void EditUser(Guid Id)
{
    int i = 0;
    InputUserDto EditUser = new InputUserDto();
    while (i++ <= 6)
    {
        string atribut = string.Empty, change = string.Empty;
        atribut = EnterAnother(ref atribut, "change");
        change = EnterAnother(ref change, "changer");
        if (atribut == "First name")
            EditUser.FirstName = change;
        else if (atribut == "Last name")
            EditUser.LastName = change;
        else if (atribut == "Patronymic")
            EditUser.Patronymic = change;
        else if (atribut == "Age")
            EditUser.Age = int.Parse(change);
        else if (atribut == "Login")
            EditUser.Login = change;
        else if (atribut == "Password")
            EditUser.Password = change;
        else if (atribut == string.Empty)
            break;
    }
    _userServices.EditUser(EditUser, Id);
}
void DeleteDuty(Guid UserId)
{
    ShowMyTransaction(UserId);
    Guid inputIdTransaction = Guid.Empty;
    EnterId(ref inputIdTransaction);
    _userServices.DeleteDuty(inputIdTransaction);
}
void UserSendMassage(int idx)
{
    _userServices.SendMassage(InputUserDataSendMassage(dto, idx));
}
InputUserDto InputUserDataSendMassage(InputUserDto DataSendMassage, int IndexUser)
{
    string AdminOrManager;
    Console.Write("Who do you want to send a message to Admin or Manager: ");
    AdminOrManager = Console.ReadLine();
    if (AdminOrManager == "Admin")
        DataSendMassage.Role = Roles.Admin;
    else if (AdminOrManager == "Manager")
        DataSendMassage.Role = Roles.Manager;
    else
        throw new Exception("Eror. Person is not found");
    Console.Write("Please enter theme massage: ");
    DataSendMassage.Theme = Console.ReadLine();
    Console.Write("Please enter text massage: ");
    DataSendMassage.Text = Console.ReadLine();
    DataSendMassage.Id = _personsList[IndexUser].Id;
    DataSendMassage.FirstName = _personsList[IndexUser].FirstName;
    return DataSendMassage;
}
//



void ManagerAction(string CommandManager, Guid IdManager)
{
    if (CommandManager == "Statistic")
        StatisticGetMoney();
    else if (CommandManager == "Requests")
    {
        ShowUsersIdTransaction();
        ShowUsersGetMoney();
        string Choice = string.Empty; Guid Id = Guid.Empty;
        write.Choice(ref Id, ref Choice);
        int idx = _personsRequestsList.FindIndex(x => x.Id.Equals(Id));

        _managerServices.ChoiceRequest(idx, Choice);
        if (Choice == "Accepted")
            _allStatistic.countAcceptedRequestGetMoney++;
        else if (Choice == "Refuse")
            _allStatistic.countRefuseRequestGetMoney++;
    }
    else if (CommandManager == "Send massage")
        ManagerSendMassage();
    else if (CommandManager == "My massages")
        MyMassagesManager(IdManager);


}
//Manager Methods
void MyMassagesManager(Guid Id)
{
    ShowAllIdMassagesForManager(Id);
    bool b = false;
    int idx = _massages.FindIndex(x => x.IdSender.Equals(Id));
    foreach (var ii in _massages)
    {
        if (ii.Role == Roles.Manager)
        {
            Console.WriteLine(ii.ToString());
            b = true;
        }
    }
    if (b)
        ShowMassage();
    else
        Console.WriteLine("\nYou don't have massage\n");
}
void ShowMassageManager()
{
    Guid IdMassage = Guid.Parse(Console.ReadLine());
    int idx = _massages.FindIndex(x => x.Id.Equals(IdMassage));
    Console.WriteLine($"\n{_massages[idx].ToString()}\n{_massages[idx].Text}");
}
void ShowAllIdMassagesForManager(Guid Id)
{
    int idx = _massages.FindIndex(x => x.IdSender.Equals(Id));
    foreach (var ii in _massages)
    {
        if (ii.Role == Roles.Manager)
            Console.WriteLine(ii.Id);
    }
}
void StatisticGetMoney()
{
    Console.WriteLine("Accepted Request: " + _allStatistic.countAcceptedRequestGetMoney);
    Console.WriteLine("Refuse Request: " + _allStatistic.countRefuseRequestGetMoney);
}
void ShowUsersGetMoney()
{
    foreach (var ii in _personsRequestsList)
    {
        if (ii.StatusDuty == StatusUser.Pending)
            Console.WriteLine(ii.ToString());
    }
}
void ManagerSendMassage()
{
    _managerServices.SendMassage(InputDataSendMassageForUser(dto));
}
//



void ModeratorAction(string CommandAdmin)
{

    if(CommandAdmin == "Requests")
    {
        ShowAllUsersId<Person>(_personsList, StatusUser.Pending);
        ShowUsers(StatusUser.Pending);
        string Choice = string.Empty; Guid Id = Guid.Empty;
        write.Choice(ref Id, ref Choice);
        int idx = _personsList.FindIndex(x => x.Id.Equals(Id));

        _moderatorServices.ChoiceRequest(idx, Choice);
        if (Choice == "Refuse")
        {
            _allStatistic.countRefuseRequestRegistration++;
            string textСauseRefusalOfRegistration = string.Empty;
            _personsList[idx].CauseRefuseRegistration = СauseRefusalOfRegistration(textСauseRefusalOfRegistration);
        }
        else if (Choice == "Accepted")
            _allStatistic.countAcceptedRequestRegistration++;
    }
}
//Moderator Methods
void ShowAllUsersId<T>(List<T> PersonsId, StatusUser Status) where T : Person
{
    bool presence = false;
    for (int i = 0; i < PersonsId.Count; i++)
    {
        if (PersonsId[i].Role == Roles.User && PersonsId[i].Status == Status)
        {
            Console.WriteLine($"{PersonsId[i].FirstName} {PersonsId[i].LastName}: Id: {PersonsId[i].Id.ToString()}");
            presence = true;
        }
    }
    if (!presence)
        throw new Exception("You don't have requests");
}
//



void AdminAction(string CommandAdmin, Guid Id)
{
    if (CommandAdmin == "Show user")
    {
        ShowAllUsersId(_personsList, StatusUser.Accepted);
        Guid IdUser = new Guid();
        EnterId(ref IdUser);
        int idxUser = FindIndex(IdUser);
        ShowUser(idxUser);
    }
    else if (CommandAdmin == "Show users")
        ShowUsers(StatusUser.Accepted);
    else if (CommandAdmin == "Send massage")
        AdminSendMassage();
    else if (CommandAdmin == "My massages")
        MyMassagesAdmin(Id);
}
//Admin Methods
void MyMassagesAdmin(Guid Id)
{
    ShowAllIdMassagesForAdmin(Id);
    bool b = false;
    int idx = _massages.FindIndex(x => x.IdSender.Equals(Id));
    foreach (var ii in _massages)
    {
        if (ii.Role == Roles.Admin)
        {
            Console.WriteLine(ii.ToString());
            b = true;
        }
    }
    if (b)
        ShowMassage();
    else
        Console.WriteLine("\nYou don't have massage\n");
}
void ShowMassageAdmin()
{
    Guid IdMassage = Guid.Parse(Console.ReadLine());
    int idx = _massages.FindIndex(x => x.Id.Equals(IdMassage));
    Console.WriteLine($"\n{_massages[idx].ToString()}\n{_massages[idx].Text}");
}
void ShowAllIdMassagesForAdmin(Guid Id)
{
    int idx = _massages.FindIndex(x => x.IdSender.Equals(Id));
    foreach (var ii in _massages)
    {
        if (ii.Role == Roles.Admin)
            Console.WriteLine(ii.Id);
    }
}
void EnterId(ref Guid Id)
{
    Id = Guid.Parse(Console.ReadLine());
}
void ShowUser(int idx)
{
    Console.WriteLine(_personsList[idx].ToString());
}
void ShowUsers(StatusUser Status)
{
    foreach(var ii in _personsList)
    {
        if (ii.Role == Roles.User && ii.Status == Status)
            Console.WriteLine(ii.ToString());
    }
}
void AdminSendMassage()
{
    _adminServices.SendMassage(InputDataSendMassageForUser(dto));
}



/////////////////

InputUserDto InputDataSendMassageForUser(InputUserDto DataSendMassage)
{
    ShowAllUserId();
    string EnterUserId;
    Console.Write("Enter user Id: ");
    EnterUserId = Console.ReadLine();
    Console.Write("Please enter theme massage: ");
    DataSendMassage.Theme = Console.ReadLine();
    Console.Write("Please enter text massage: ");
    DataSendMassage.Text = Console.ReadLine();
    DataSendMassage.Id = Guid.Parse(EnterUserId);
    return DataSendMassage;
}
void ShowAllUserId()
{
    for (int i = 0; i < _personsList.Count; i++)
        if (_personsList[i].Status == StatusUser.Accepted && _personsList[i].Role == Roles.User)
            Console.WriteLine($"Name: {_personsList[i].FirstName} - Id: {_personsList[i].Id}");
}

void WriteCountStatusRequestRegistration(int count, string status)
{
    Console.WriteLine($"Count {status} registration: {count}");
}
string СauseRefusalOfRegistration(string Сause)
{
    Console.Write("Enter your cause: ");
    Сause = Console.ReadLine();

    return Сause;
}
string СauseRefusalOfDuty(string Сause)
{
    Console.Write("Enter your cause: ");
    Сause = Console.ReadLine();
    return Сause;
}
void ShowMyTransaction(Guid Id)
{
    foreach (var ii in _personsRequestsList)
    {
        if (ii.IdSender == Id)
            Console.WriteLine($"Name: {ii.Name} - Id Transaction: {ii.Id}");
    }
}
void ShowAllUserIdGetMoney()
{
    bool presence = false;
    for (int i = 0; i < _personsRequestsList.Count; i++)
    {
        if (_personsRequestsList[i].StatusDuty == StatusUser.Pending)
        {
            Console.WriteLine($"Name: {_personsRequestsList[i].Name} - Id: {_personsRequestsList[i].Id}");
            presence = true;
        }
    }
    if (!presence)
        throw new Exception("You don't have requests");
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
string Command(ref string command)
{
    Console.Write("Enter your command:> ");
    command = Console.ReadLine();
    return command;
}
int FindIndex(Guid Id)
{
    int idx = _personsList.FindIndex(x => x.Id.Equals(Id));
    return idx;
}
string EnterAnother(ref string another, string change)
{
    Console.Write($"Enter {change}: ");
    another = Console.ReadLine();
    Console.WriteLine();
    return another;
}
void ShowUsersIdTransaction()
{
    foreach(var ii in _personsRequestsList)
    {
        if(ii.StatusDuty == StatusUser.Pending)
            Console.WriteLine($"Name: {ii.Name} - Id Transaction: {ii.Id}");
    }
}