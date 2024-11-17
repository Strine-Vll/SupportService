namespace Application.Exceptions;

public class WalletNotFoundException : Exception
{
    public int WalletId { get; set; }

    public WalletNotFoundException() : base("Счетов по вашему запросу не найдено") { }

    public WalletNotFoundException(int walletId) : base($"Счёт с id = '{walletId}' не найден")
    {
        WalletId = walletId;
    }
}
