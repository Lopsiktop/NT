using ErrorOr;

namespace NT.Domain.Common.Errors;

public static partial class Errors
{
    public static class Teacher
    {
        public static Error NameCannotBeEmpty =>
            Error.Validation("Teacher.NameCannotBeEmpty", "Имя не может быть пустым!");

        public static Error SurnameCannotBeEmpty =>
            Error.Validation("Teacher.SurnameCannotBeEmpty", "Фамилия не может быть пустым!");

        public static Error PatronymicCannotBeEmpty =>
            Error.Validation("Teacher.PatronymicCannotBeEmpty", "Отчество не может быть пустым!");

        public static Error UserCannotBeNull =>
            Error.Validation("Teacher.UserCannotBeNull", "Пользователь не может быть null!");
    }
}
