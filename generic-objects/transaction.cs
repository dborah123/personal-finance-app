enum TransactionType {
    NONE,
    GROCERIES,
    RENT,
    TRANSPORTATION,
    FURNITURE,
    SUBSRCIPTIONS,
    TRAVEL,
}

class Transaction {
    // Attributes
    public int id { get; set; }
    public double amount { get; set; }
    public TransactionType transactionType { get; set; }
    public DateTime datetime { get; set; }

    // Constructor
    public Transaction(
        int id,
        double amount,
        TransactionType transactionType,
        DateTime datetime
    ) {
        this.amount = amount;
        this.transactionType = transactionType;
        this.datetime = datetime;
    }

    // Methods
    public string toString() {
        return string.Format("Transaction: ${1} of type {2} id: {0}",
            this.id, this.amount, this.transactionType);
    }
}