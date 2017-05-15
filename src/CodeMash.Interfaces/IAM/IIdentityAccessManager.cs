using ServiceStack;
using ServiceStack.Auth;

namespace CodeMash.Interfaces.IAM
{
    public interface IIdentityAccessManager
    {
        AuthenticateResponse Login(string userNameOrEmail, string password, bool rememberMe = false, string continueUrl = "");
        AuthenticateResponse Login(Authenticate authenticate);
        RegisterResponse Register(Register register, bool confirmEmail = false);
        RegisterResponse Register(string userNameOrEmail, string password, string continueUrl, bool confirmEmail = false);

        void Logout();

        bool IsInRole(string role);

        bool HasPermission(string permission);

        void UpdateUser(UserAuth userAuth);

        bool ChangePassword(string userId, string oldPassword, string newPassword, string newPasswordAgain = "");

        IUserAuth GetUserById(string id);

        bool DeleteUserById(int id);

    }
}
