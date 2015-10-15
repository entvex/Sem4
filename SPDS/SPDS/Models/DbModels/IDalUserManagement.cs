using SPDS.Models.DbModels;

namespace MSSQLModel
{
    public interface IDALUserManagement
    {
        void InsertUser(User user, Permission perm);
        Permission GetPermById(int id);
        User GetUserByEmail(string email);
    }
}