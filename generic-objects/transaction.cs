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
    double amount { get; set; }
    TransactionType transactionType { get; set; }
    DateTime datetime { get; set; }

    // Constructor
    public Transaction(
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
        return string.Format("Transaction: ${0} of type {1}",
            this.amount, this.transactionType);
    }


}