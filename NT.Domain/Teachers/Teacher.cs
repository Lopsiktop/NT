using ErrorOr;
using NT.Domain.Common;
using NT.Domain.Common.Errors;
using NT.Domain.Users;

namespace NT.Domain.Teachers;

public class Teacher : Entity
{
    public string Name { get; private set; }

    public string Surname { get; private set; }

    public string Patronymic { get; private set; }

    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    private Teacher() { }

    private Teacher(string name, string surname, string patronymic, User user)
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        User = user;
        UserId = user.Id;
    }

    public static List<Error> Validate(string? name, string? surname, string? patronymic, User? user)
    {
        var errors = new List<Error>();

        if (name is not null && string.IsNullOrWhiteSpace(name))
            errors.Add(Errors.Teacher.NameCannotBeEmpty);

        if (surname is not null && string.IsNullOrWhiteSpace(surname))
            errors.Add(Errors.Teacher.SurnameCannotBeEmpty);

        if (patronymic is not null && string.IsNullOrWhiteSpace(patronymic))
            errors.Add(Errors.Teacher.PatronymicCannotBeEmpty);

        if (user is null)
            errors.Add(Errors.Teacher.UserCannotBeNull);

        return errors;
    }

    public static ErrorOr<Teacher> Create(string name, string surname, string patronymic, User user)
    {
        var errors = Validate(name, surname, patronymic, user);
        if (errors.Count != 0)
            return errors;

        return new Teacher(name, surname, patronymic, user);
    }

    public List<Error>? Update(string name, string surname, string patronymic, User user)
    {
        var errors = Validate(name, surname, patronymic, user);
        if (errors.Count != 0)
            return errors;

        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        User = user;

        return null;
    }

    public List<Error>? Path(Dictionary<string, object> dict)
    {
        string? name = dict[nameof(Name)].ToString();
        string? surname = dict[nameof(Surname)].ToString();
        string? patronymic = dict[nameof(Patronymic)].ToString();
        User? user = dict[nameof(User)] as User;

        var errors = Validate(name, surname, patronymic, User.Empty());
        if (errors.Count != 0)
            return errors;

        if (name is not null)
            Name = name;

        if(surname is not null)
            Surname = surname;

        if(patronymic is not null)
            Patronymic = patronymic;

        if(user is not null) 
            User = user;

        return null;
    }
}
