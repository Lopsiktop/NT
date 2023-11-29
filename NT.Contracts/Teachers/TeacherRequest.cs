namespace NT.Contracts.Teachers;

public record TeacherRequest(string Name, string Surname, string Patronymic, int UserId)
    : Request;