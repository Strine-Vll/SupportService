using Domain.CustomAttributes;

namespace Domain.Enums;

public enum UserRoleEnum
{
    [Description("Пользователь")]
    User = 1,

    [Description("Специалист поддержки")]
    SupportSpecialist,

    [Description("Менеджер")]
    Manager
}
