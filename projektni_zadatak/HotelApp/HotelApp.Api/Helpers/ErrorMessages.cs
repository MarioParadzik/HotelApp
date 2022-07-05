namespace HotelApp.Api.Helpers
{
    public static class ErrorMessages
    {
        public const string EmailTaken = "Email already taken";
        public const string PasswordLength = "Password need to be at least 8 characters long";
        public const string PasswordUppercaseLetter = "Password need to include at least one uppercase character";
        public const string PasswordLowercaseLetter = "Password need to include at least one downcase character";
        public const string PasswordDigit = "Password need to include at least one digit";
        public const string PasswordSpecialCharacter = "Password need to include at least one special character";
        public const string PasswordMatch = "Password does not match.";
        public const string LocationNotExisting = "Location does not exist for given Id.";
        public const string ActiveStatus = "Cannot update hotel with active status.";
        public const string DateNotFree = "Date is allready reserved.";
    }
}
