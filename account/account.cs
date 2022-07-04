
enum AccountType {
    CHECKING,
    CREDITCARD,
    MUTUALFUND,
    RETIREMENT,
    OTHER,
}

/**
 * Class representing a basic account. Meant to be a parent class to specific account
 */
class Account {
    string? accountName { get; set; }
    string? path { get; set; }
    AccountType accountType { get; set; }
    double totalValue { get; set; }
    List<Transaction>? transactions;
    int numTransactions { get; set; }
    
    public Account(string path) {
        this.transactions = new List<Transaction>();
        this.path = path;
        mountMetadata();
        mountData();
    }

    private void mountMetadata() {
        /**
         * Retrieves necessary metadata about the account from corresponding csv file
         */
        if (this.path == null) throw new Exception("path is null");

        string actualPath = this.path + "/account.metadata.csv";

        using (var reader = new StreamReader(actualPath)) {
            // Parsing line into list of string values
            string? line = reader.ReadLine();
            if (line == null) {
                throw new Exception(
                    "Error with account.metadata.csv: data not there"
                );
            }
            string[] values = line.Split('\t');

            // Assigning attributes and casting as necessary
            this.accountName = values[0];
            Enum.TryParse(values[1], out AccountType parsedAccountType);
            this.accountType = parsedAccountType;
            this.totalValue = Convert.ToDouble(values[2]);
        }
    }

    private void mountData() {
        /**
         * Retrieves transaction data from corresponding csv file
         */
        if (transactions == null) {
            transactions = new List<Transaction>();
        }
        double totalAmount = 0.0;

        if (this.path == null) throw new Exception("path is null");
        string actualPath = this.path + "/account.data.csv";

        using (var reader = new StreamReader(actualPath)) {
            while (!reader.EndOfStream) {
                string? line = reader.ReadLine();
                if (line == null) {
                    throw new Exception(
                        "Error with account.data.csv: data not there"
                    );
                }
                string[] values = line.Split(',');

                double amount = Convert.ToDouble(values[0]);
                Enum.TryParse(values[1], out TransactionType parsedTransactionType);
                // DateTime datetime = DateTime.Parse(values[2]);
                DateTime datetime = DateTime.Now;

                Transaction transaction = new Transaction(
                    amount,
                    parsedTransactionType,
                    datetime
                );

                this.transactions.Add(transaction);
                totalAmount += amount;

                Console.WriteLine(transaction.toString());
            }
        }
        this.numTransactions = this.transactions.Count;
        this.totalValue = totalAmount;

        Console.WriteLine(this.toString());
    }

    public string toString() {
        return string.Format(
            "Account: {0}\tType: {1}\tTotal Value ${2}\t# of Transactions: {3}",
            this.accountName, this.accountType, this.totalValue, this.numTransactions
        );
    }
}