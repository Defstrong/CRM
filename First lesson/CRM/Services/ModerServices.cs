using First_lesson.CRM.Models;
using First_lesson.CRM.Enums;

namespace First_lesson.CRM.Services
{
    public class ModerServices
    {
        static string inputTeg;
        static List<User> requests = new List<User>();
        static List<User> usersList = new List<User>();
        static List<int> indexes = new List<int>();
        public static int countAcceptRequest = 0;
        public static int countRefuseRequest = 0;
        public static string Refuse;

        public ModerServices(ref List<User> Requests, ref List<User> UsersList)
        {
            requests = Requests;
            usersList = UsersList;
        }
        public static void ModerLogin()
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
                        countAcceptRequest++;
                        usersList.Add(requests[i]);
                        requests[i].Status = StatusUser.Accepted;
                    }
                    else if (inputTeg == "Refuse")
                    {
                        countRefuseRequest++;
                        Console.WriteLine("\t\t\tRejection reason\n");
                        Refuse = Console.ReadLine();
                        usersList.Add(requests[i]);
                        requests[i].Status = StatusUser.Refuse;
                        requests[i].UserStatus = Refuse;
                        indexes.Add(i);
                    }
                }
                for(int i = 0; i < requests.Count; i++)
                    requests.Remove(requests[i]);
            }
        }
    }
}
