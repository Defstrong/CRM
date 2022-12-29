using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;

namespace First_lesson.CRM.Services
{
    public partial class ClassMethods
    {
        static public InputUserDto InputUserData = new InputUserDto();
        static public CreatorServices creatorServices = new CreatorServices();
        private const string creatorName = "Creator";
        private const string creatorPassword = "Creator";

        private const string loginManager = "Manager";
        private const string passwordManager = "Manager";

        public static string inputTeg;
        private const string moderName = "Moder";
        private const string moderPassword = "Moder";
        private static int numberRequest;
        Roles userRole;

        public static List<ManagerServices> listRequestsForGetMoney = new List<ManagerServices>();
        public static List<User> usersList = new List<User>();
        public static List<User> requests = new List<User>();
    }
}
