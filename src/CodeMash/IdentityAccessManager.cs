using CodeMash.Interfaces.IAM;
using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack;
using ServiceStack.Auth;

namespace CodeMash
{
    public class IdentityAccessManager : IIdentityAccessManager
    {
        public bool ChangePassword(string userId, string oldPassword, string newPassword, string newPasswordAgain = "")
        {
            if (!string.IsNullOrEmpty(newPasswordAgain))
            {
                if (string.Compare(newPassword, newPasswordAgain) != 0)
                {
                    throw new ArgumentException("Passwords didn't match");
                }
            }

            var response = CodeMashBase.Client.Put(new ChangePassword { NewPassword1 = newPassword, OldPassword = oldPassword, NewPassword2 = newPassword });

            if (response != null)
            {
                return response.Result;
            }
            return false;
        }

        public bool DeleteUserById(int id)
        {
            var response = CodeMashBase.Client.Delete(new DeleteUser { Id = id });

            if (response != null)
            {
                return response.Result;
            }
            return false;
        }

        public IUserAuth GetUserById(string id)
        {
            var response = CodeMashBase.Client.Get(new GetUser { Id = id });

            if (response != null)
            {
                return response.Result;
            }
            return null;
        }

        public IUserAuth GetUserByName(string name)
        {
            var response = CodeMashBase.Client.Get(new GetUserByName { Name = name });

            if (response != null)
            {
                return response.Result;
            }
            return null;
        }

        public IUserAuth GetUserByEmail(string email)
        {
            var response = CodeMashBase.Client.Get(new GetUserByEmail { Email = email });

            if (response != null)
            {
                return response.Result;
            }
            return null;
        }

        public bool HasPermission(string permission)
        {
            var response = CodeMashBase.Client.Get(new HasPermission { Permission = permission });

            if (response != null)
            {
                return response.Result;
            }
            return false;
        }

        public bool IsInRole(string role)
        {
            var response = CodeMashBase.Client.Get(new HasRole { Role = role });

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
            return CodeMashBase.Client.Post(authenticate);
        }

        public AuthenticateResponse Login(Authenticate authenticate)
        {
            return CodeMashBase.Client.Post(authenticate);
        }

        public void Logout()
        {
//#if NET452
//                CodeMashBase.Client.Post(new Authenticate() { provider = ServiceStack.Auth.AuthenticateService.LogoutAction });
//#endif
//#if NETCOREAPP1_1
//            CodeMashBase.Client.Post(new Authenticate() { provider = AuthenticateService.LogoutAction });
//#endif
            CodeMashBase.Client.Post(new Authenticate() { provider = AuthenticateService.LogoutAction });
        }

        public RegisterResponse Register(Register register, bool confirmEmail = false)
        {
            if (confirmEmail)
            {
                register.AutoLogin = false;
            }

            var response = CodeMashBase.Client.Post(register);
            if (response != null && string.IsNullOrEmpty(response.ResponseStatus.ErrorCode))
            {
                var user = GetUserById(response.UserId);
                
                if (user != null)
                {
                    if (user.Meta == null)
                    {
                        user.Meta = new Dictionary<string, string>();
                    }

                    user.Meta.Add("IsConfirmed", confirmEmail?  "false" : "true");
                    user.Meta.Add("IsEnabled", "true");

                    UpdateUser((UserAuth)user);
                }
            }

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

            var response = CodeMashBase.Client.Post(register);
            if (response != null && string.IsNullOrEmpty(response.ResponseStatus.ErrorCode))
            {
                var user = GetUserById(response.UserId);

                if (user != null)
                {
                    if (user.Meta == null)
                    {
                        user.Meta = new Dictionary<string, string>();
                    }

                    user.Meta.Add("IsConfirmed", confirmEmail ? "false" : "true");
                    user.Meta.Add("IsEnabled", "true");

                    UpdateUser((UserAuth)user);
                }
            }

            return response;
        }

        public void UpdateUser(UserAuth userAuth)
        {
            CodeMashBase.Client.Put<UserAuth>("/iam/users", userAuth);
        }
    }
}

