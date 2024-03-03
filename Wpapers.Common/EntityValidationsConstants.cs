namespace Wpapers.Common
{
    public static class EntityValidationsConstants
    {
        public static class WallpapersValidations
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 100;
        }
        public static class ApplicationUserValidations
        {
            public const int NicknameMinLength = 2;
            public const int NicknameMaxLength = 20;          
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 50;
        }
    }
}
