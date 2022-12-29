using First_lesson.CRM.DTO;
using First_lesson.CRM.Enums;
using First_lesson.CRM.Models;

namespace First_lesson.CRM.Services
{
    public partial class ClassMethods
    {
        public void Registration(
            ref InputUserDto dtoRegistration)
        {
            DataInput(ref dtoRegistration);

            requests.Add(new User(dtoRegistration, userRole = Roles.User));
        }
    }
}
