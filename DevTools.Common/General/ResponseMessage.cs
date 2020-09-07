using System.Net.NetworkInformation;

namespace DevTools.Common.General
{
    public class ResponseMessage
    {
        public const string InvalidCredential = "Invalid email Or password ";

        public const string AccountNotActive = "Please confirm your Account before login ";

        public const string RoleNotFound = "Role was not Found ";

        public const string EmailAlreadyExist = "Email already exist in system";

        public const string MobileAlreadyExist = "mobile already exist in sysem";

        public const string UserNotFound = "user was not found";

        public const string UserUpdateSuccessfully = "user was update successfully";

        public const string AdminDeleteNotAllowed = "you are not allowed to delete admin";

        public const string DeleteYourSelfNotAllowed = "you can not delete your self";

        public const string DeleteUserSuccessfully = "Delete User Successful";

        public const string UpdateSettingSuccessfully = "update setting Successfull";

        public const string TicketWasNotFound = "the ticket was not found";

        public const string TicketEditSuccessfully = "the ticket Edit successfully";

        public const string ChangeNotAllowed = "you are only allowed to delete your Reply ";

        public const string WrongPassword = "wrong password please try again";

        public const string PasswordRecentlyUsed = "this password recently used plaese pick an other on";

        public const string PasswordChangeSuccessfully = "the password change successfully";

        public const string ForgetPasswordSentSuccessfully = "forget password sent to your account successfully";

        public const string TemplateNotFound = "the template was not found";
    }
}