using Domain.CustomAttributes;

namespace Domain.Enums;

public enum UserRoleEnum
{
    [Description("Анонимный пользователь")]
    Anonymous,

    [Description("Пользователь")]
    User,
    [Description("Специалист поддержки")]
    SupportSpecialist,

    [Description("Менеджер")]
    Manager
}
