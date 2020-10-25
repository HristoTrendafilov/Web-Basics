namespace Git.Global
{
    public class GlobalValidations
    {
        //user
        public const int UsernameMaxLength = 20;
        public const int UsernameMinLength = 5;

        public const int PasswordMaxLength = 20;
        public const int PasswordMinLength = 6;

        //repository
        public const int RepositoryNameMinLength = 3;
        public const int RepositoryNameMaxLength = 10;

        //commit
        public const int CommitDescriptionMinLength = 5;
    }
}
