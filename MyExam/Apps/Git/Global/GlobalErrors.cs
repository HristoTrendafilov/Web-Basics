namespace Git.Global
{
    public class GlobalErrors
    {
        //user
        public const string invalidUsername = "Username must be between 5 and 20 characters!";
        public const string usernameAlreadyTaken = "Username is already taken!";
        public const string emailAlreadyTaken = "Email is already taken!";
        public const string invalidEmail = "Invalid email!";
        public const string invalidPassword = "Password must be between 6 and 20 characters!";
        public const string passwordsDontMatch = "Passwords don't match!";
        public const string invalidLogout = "Only logged in users can logout!";

        //repository
        public const string invalidRepositoryName = "Name must be between 3 and 10 characters!";

        //commit
        public const string invalidCommitDescription = "Description should be more than 5 characters!";
    }
}
