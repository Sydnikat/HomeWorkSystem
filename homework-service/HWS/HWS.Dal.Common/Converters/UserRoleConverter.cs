using System.ComponentModel;

namespace HWS.Dal.Common.Converters
{
    public static class UserRoleConverter
    {
        public static Domain.User.UserRole toDomain(this UserCommon.UserRole role)
        {
            switch (role)
            {
                case UserCommon.UserRole.Student:
                    return Domain.User.UserRole.Student;
                case UserCommon.UserRole.Teacher:
                    return Domain.User.UserRole.Teacher;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
