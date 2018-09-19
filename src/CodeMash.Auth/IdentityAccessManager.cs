using System;
using CodeMash.Interfaces;
using CodeMash.ServiceModel;
using ServiceStack;
using ServiceStack.Auth;

namespace CodeMash.Auth
{
    public class IdentityAccessManager : IIdentityAccessManager
    {
        public ICodeMashSettings CodeMashSettings { get; set; }
        
        public bool ChangePassword(string userId, string oldPassword, string newPassword, string newPasswordAgain = "")
        {
            if (!string.IsNullOrEmpty(newPasswordAgain))
            {
                if (string.Compare(newPassword, newPasswordAgain) != 0)
                {
                    throw new ArgumentException("Passwords didn't match");
                }
            }

            var response = CodeMashSettings.Client.Put(new ChangePassword { NewPassword1 = newPassword, OldPassword = oldPassword, NewPassword2 = newPassword });

            if (response != null)
            {
                return response.Result;
            }
            return false;
        }

        public bool DeleteUserById(int id)
        {
            var response = CodeMashSettings.Client.Delete(new DeleteUser { Id = id });

            if (response != null)
            {
                return response.Result;
            }
            return false;
        }

        public IUserAuth GetUserById(string id)
        {
            var response = CodeMashSettings.Client.Get(new GetUser { Id = id });

            if (response != null)
            {
                return response.Result;
            }
            return null;
        }

        public IUserAuth GetUserByName(string name)
        {
            var response = CodeMashSettings.Client.Get(new GetUserByName { Name = name });

            if (response != null)
            {
                return response.Result;
            }
            return null;
        }

        public IUserAuth GetUserByEmail(string email)
        {
            var response = CodeMashSettings.Client.Get(new GetUserByEmail { Email = email });

            if (response != null)
            {
                return response.Result;
            }
            return null;
        }

        public bool HasPermission(string permission)
        {
            var response = CodeMashSettings.Client.Get(new HasPermission { Permission = permission });

            if (response != null)
            {
                return response.Result;
            }
            return false;
        }

        public bool IsInRole(string role)
        {
            var response = CodeMashSettings.Client.Get(new HasRole { Role = role });

            if (response != null)
            {
                return response.Result;
            }
            return false;
        }

        public AuthenticateResponse Login(string userNameOrEmail, string password, bool rememberMe = false, string continueUrl = "")
        {
            var authenticate = new Authenticate
            {
                UserName = userNameOrEmail,
                Password = password,
                RememberMe = rememberMe,
                Continue = continueUrl

            };
            return CodeMashSettings.Client.Post<AuthenticateResponse>("/auth/credentials", authenticate);
        }

        public AuthenticateResponse Login(Authenticate authenticate)
        {
            return CodeMashSettings.Client.Post<AuthenticateResponse>("/auth/credentials", authenticate);
        }

        public void Logout()
        {
//#if NET452
//                CodeMashBase.Client.Post(new Authenticate() { provider = ServiceStack.Auth.AuthenticateService.LogoutAction });
//#endif
//#if NETCOREAPP1_1
//            CodeMashBase.Client.Post(new Authenticate() { provider = AuthenticateService.LogoutAction });
//#endif
            CodeMashSettings.Client.Post(new Authenticate() { provider = AuthenticateService.LogoutAction });
        }

        public RegisterResponse Register(Register register, bool confirmEmail = false)
        {
            if (confirmEmail)
            {
                register.AutoLogin = false;
            }

            var response = CodeMashSettings.Client.Post(register);
            return response;
        }

        public RegisterResponse Register(string userNameOrEmail, string password, string continueUrl, bool confirmEmail = false)
        {
            var userName = string.Empty;
            var email = string.Empty;

            if (userNameOrEmail.Contains("@"))
            {
                email = userNameOrEmail;
                userName = userNameOrEmail.Substring(0, userNameOrEmail.IndexOf("@"));
            }
            else
            {
                userName = userNameOrEmail;
            }

            var register = new Register
            {
                UserName = userName,
                Email = email,
                AutoLogin = true,
                Continue = continueUrl,
                Password = password,
                DisplayName = string.IsNullOrEmpty(email) ? userName : email
            };

            if (confirmEmail)
            {
                register.AutoLogin = false;
            }

            var response = CodeMashSettings.Client.Post(register);
            return response;
        }

        public void UpdateUser(UserAuth userAuth)
        {
            CodeMashSettings.Client.Put<UserAuth>("/iam/users", userAuth);
        }
    }
}

