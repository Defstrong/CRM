using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DTO
{
    class Write
    {
        public void WriteMassage(int number,Massage massage) =>
            Console.WriteLine($"{number}){ massage.Name }\t Theme: {massage.Theme} \n {massage.SendMassage}");
        public void WriteDutys(string duty) =>
            Console.WriteLine(duty);
        public void WriteUserData(Person user) =>
            Console.WriteLine(user.ToString());
        public void WriteUserRequest(GetMoney userRequest) =>
            Console.WriteLine(userRequest.Print());
        public void ChoiceAtribut(ref string atribut, ref string change)
        {
            Console.Write("Enter atribut: ");
            atribut = Console.ReadLine();
            Console.Write("Enter replacement: ");
            change = Console.ReadLine();
        }
        public void Choice(ref string Name, ref string Choice)
        {
            Console.Write("Enter your choice: ");
            Choice = Console.ReadLine();
            Console.Write("Enter user name: ");
            Name = Console.ReadLine();
        }
        public void ChoiceCreator(ref string Name)
        {
            Console.Write("Enter user name: ");
            Name = Console.ReadLine();
        }
        public void Status(StatusUser status) =>
            Console.WriteLine(status);
    }
}
