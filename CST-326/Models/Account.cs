namespace CST_326.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }

        public Account(int accountId, int userId, string accountNumber, string accountType, decimal balance, DateTime createdAt)
        {
            AccountId = accountId;
            UserId = userId;
            AccountNumber = accountNumber;
            AccountType = accountType;
            Balance = balance;
            CreatedAt = createdAt;
        }

        public Account()
        {
        }

        // Methods to handle deposit, withdrawal, etc., could be added here
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.", nameof(amount));
            }
            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive.", nameof(amount));
            }
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false; // Insufficient funds
        }
    }
}
