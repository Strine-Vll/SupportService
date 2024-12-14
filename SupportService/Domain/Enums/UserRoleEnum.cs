using Domain.CustomAttributes;

namespace Domain.Enums;

public enum UserRoleEnum
{
    [Description("Анонимный пользователь")]
    Anonymous = 1,

    [Description("Пользователь")]
    User,

    [Description("Специалист поддержки")]
    SupportSpecialist,

    [Description("Менеджер")]
    Manager
}
