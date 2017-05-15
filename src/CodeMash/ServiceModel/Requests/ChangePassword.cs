﻿using ServiceStack;

namespace CodeMash
{
    public class ChangePassword : IReturn<ChangePasswordResponse>
    {
        public string OldPassword { get; set; }
        public string NewPassword1 { get; set; }
        public string NewPassword2 { get; set; }
    }
}