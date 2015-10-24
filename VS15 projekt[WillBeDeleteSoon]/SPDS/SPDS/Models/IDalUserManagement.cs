using SPDS;

namespace MSSQLModel
{
    public interface IDalUserManagement
    {
        void InsertUser(User user,Permission perm);
        Permission GetPermById(int id);
        User GetUserByEmail(string email);
    }
}