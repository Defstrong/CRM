using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;
using System;
using System.Runtime.CompilerServices;

namespace CRM.Services
{
    public partial class Autorization
    {
        public static List<Person> persons;
        public void Login(InputUserDto DtoLogin, Massage massage, string command)
        {
            int idx = person.FindIndex(x => x.Login.Equals(DtoLogin.Login) && x.Password.Equals(DtoLogin.Password));

            if (idx != -1)
            {
                string NameUser = string.Empty, Choice = string.Empty;
                if (person[idx].Role == Roles.User)
                {
                    if (person[idx].Status == StatusUser.Accepted)
                    {
                        if (command == "Get money")
                            GetMoney(DtoLogin, idx);
                        else if (command == "Status dutys")
                            StatusDutys(idx);
                        else if (command == "My massages")
                            MyMassages(person[idx].FirstName);
                    }
                    else
                        throw new Exception("Sorry your request is not Accepted");

                }
                else if (person[idx].Role == Roles.Admin)
                {
                    if (command == "Show users")
                    {
                        for (int i = 0; i < person.Count; i++)
                        {
                            if (person[i].Role == Roles.User)
                                write.WriteUserData(person[i]);
                        }
                    }
                    else if (command == "Show user")
                    {
                        int idxUser = person.FindIndex(x => x.FirstName == DtoLogin.FirstName);
                        if (idxUser != -1)
                            write.WriteUserData(person[idxUser]);
                        else throw new Exception("Eror 404\nUser is not found");
                    }
                    else if (command == "Massages")
                        MyMassages(person[idx].FirstName);

                }
                else if (person[idx].Role == Roles.Moderator)
                {
                    if (command == "Requests")
                    {
                        for (int i = 0; i < person.Count; i++)
                        {
                            if (person[i].Role == Roles.User && person[i].Status == StatusUser.Pending)
                            {
                                write.WriteUserData(person[i]);
                            }
                        }
                        write.Choice(ref NameUser, ref Choice);
                        int idxUserForChoiceRegistration =
                            person.FindIndex(x => x.FirstName.Equals(NameUser)
                            && x.Status == StatusUser.Pending);
                        if (idxUserForChoiceRegistration != -1)
                        {
                            if (Choice == "Accepted")
                                person[idxUserForChoiceRegistration].Status = StatusUser.Accepted;
                            else if(Choice == "Refuse")
                                person[idxUserForChoiceRegistration].Status = StatusUser.Refuse;
                        }
                        else throw new Exception("Eror 404");
                    }
                }
                else if (person[idx].Role == Roles.Creator)
                {
                    write.ChoiceCreator(ref NameUser);
                    int idxUserForChange = person.FindIndex(x => x.FirstName.Equals(NameUser));
                    if (command == "Delete user" && idxUserForChange != -1)
                    {
                        person.Remove(person[idxUserForChange]);
                    }
                    else if (command == "Edit" && idxUserForChange != -1)
                    {
                        EditUser(person[idxUserForChange]);
                    }
                    else throw new Exception("Eror 404 user is not found\n");
                }
                else if (person[idx].Role == Roles.Manager)
                {
                    if (command == "Requests")
                    {
                        for (int i = 0; i < requestsUser.Count; i++)
                        {
                            if (requestsUser[i].StatusDuty == StatusUser.Pending)
                            {
                                write.WriteUserRequest(requestsUser[i]);
                            }
                        }
                        write.Choice(ref NameUser, ref Choice);
                        int idxUserForChoiceRequest =
                            requestsUser.FindIndex(x => x.Name.Equals(NameUser)
                            && x.StatusDuty == StatusUser.Pending);
                        if (idxUserForChoiceRequest != -1)
                        {
                            if (Choice == "Accepted")
                                requestsUser[idxUserForChoiceRequest].StatusDuty = StatusUser.Accepted;
                            else if (Choice == "Refuse")
                                requestsUser[idxUserForChoiceRequest].StatusDuty = StatusUser.Refuse;
                        }
                        else throw new Exception("Eror 404. Request is not found");
                    }
                }
            }
            else throw new Exception("Eror 404\nUser is not found");
        }

        public void EditUser(Person UserForEdit)
        {
            int i = 0;
            while(i++ <= 6)
            {
                string atribut = string.Empty, change = string.Empty;
                write.ChoiceAtribut(ref atribut, ref change);
                if (atribut == "First name")
                    UserForEdit.FirstName = change;
                else if (atribut == "Last name")
                    UserForEdit.LastName = change;
                else if (atribut == "Patronymic")
                    UserForEdit.Patronymic = change;
                else if (atribut == "Age")
                    UserForEdit.Age = int.Parse(change);
                else if (atribut == "Login")
                    UserForEdit.Login = change;
                else if (atribut == "Password")
                    UserForEdit.Password = change;
                else if (atribut == string.Empty)
                    break;
            }
        }
        public void GetMoney(InputUserDto DtoGetMoney, int index)
        {
            requestsUser.Add(new GetMoney
            {
                Payday = DtoGetMoney.Payday,
                Name = person[index].FirstName,
                Age = person[index].Age,
                CountMoney = DtoGetMoney.AmountMoney
            }) ;
        }
        public void MyMassages(string Name)
        {
            int number = 0;
            for (int i = 0; i < massages.Count; i++)
            {
                if (massages[i].RecipientName == Name)
                {
                    number++;
                    write.WriteMassage(number, massages[i]);
                }
            }
        }
        public void StatusDutys(int idx)
        {
            int idxRequest = requestsUser.FindIndex(x => x.Name == person[idx].FirstName);
            foreach (var ii in requestsUser)
            {
                if (ii.Name == person[idx].FirstName && ii.StatusDuty != StatusUser.Pending)
                {
                    write.Status(requestsUser[idxRequest].StatusDuty);
                    write.WriteDutys(ii.Print());
                }
            }
        }

    }
}
