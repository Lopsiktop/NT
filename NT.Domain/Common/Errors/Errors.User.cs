using ErrorOr;

namespace NT.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error ThisUserIsAlreadyExist =>
            Error.Conflict("User.ThisUserIsAlreadyExist", "Этот пользователь уже существует!");

        public static Error ThisUserIsNotExist =>
            Error.NotFound("User.ThisUserIsNotExist", "Этот пользователь несуществует!");

        public static Error WrongLoginOrPassword =>
            Error.Validation("User.WrongLoginOrPassword", "Неверный логин или пароль!");

        public static Error ThisTokenIsDisabled =>
            Error.Conflict("User.ThisTokenIsDisabled", "Данный токен отключен!");
    }
}
