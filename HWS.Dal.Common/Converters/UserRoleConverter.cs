using System.ComponentModel;

namespace HWS.Dal.Common
{
    public static class UserRoleConverter
    {
        public static Domain.User.UserRole toDomain(this User.UserRole role)
        {
            switch (role)
            {
                case User.UserRole.Student:
                    return Domain.User.UserRole.Student;
                case User.UserRole.Teacher:
                    return Domain.User.UserRole.Teacher;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
