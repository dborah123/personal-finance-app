using System.Text;
using System.Globalization;

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
            string[] values = line.Split(',');

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

        var cultureInfo = new CultureInfo("en-US");

        using (var reader = new StreamReader(actualPath)) {
            while (!reader.EndOfStream) {
                string? line = reader.ReadLine();
                if (line == null) {
                    throw new Exception(
                        "Error with account.data.csv: data not there"
                    );
                }
                string[] values = line.Split(',');

                int id = int.Parse(values[0]);
                double amount = Convert.ToDouble(values[1]);
                Enum.TryParse(values[2], out TransactionType parsedTransactionType);
                DateTime datetime = DateTime.ParseExact(values[3], "M/d/yyyy", null);

                Transaction transaction = new Transaction(
                    id,
                    amount,
                    parsedTransactionType,
                    datetime
                );

                this.transactions.Add(transaction);
                totalAmount += amount;
            }
        }
        this.numTransactions = this.transactions.Count;
        this.totalValue = totalAmount;
    }

    public void unmountMetadata() {
        StringBuilder csv = new StringBuilder();

        // Retriving data to write
        if (this.accountName == null) this.accountName = "";
        string accountName = this.accountName;
        AccountType accountType = this.accountType;
        double totalValue = this.totalValue;

        string entry = $"{accountName},{accountType},{totalValue}\n";
        csv.Append(entry);

        if (this.path == null) throw new Exception("path is null");
        string actualPath = this.path + "/account.metadata.csv";
        File.WriteAllText(actualPath, csv.ToString());
    }

    public void unmountData() {
        if (transactions == null) return;
        StringBuilder csv = new StringBuilder();
        foreach (Transaction transaction in this.transactions) {
            int id = transaction.id;
            double amount = transaction.amount;
            TransactionType transactionType = transaction.transactionType;
            
            DateTime datetime = transaction.datetime;
            string datetimeString = datetime.ToString("M/d/yyyy");
            string entry = $"{id},{amount},{transactionType},{datetimeString}\n";
            
            csv.Append(entry);
        }

        if (this.path == null) throw new Exception("path is null");
        string actualPath = this.path + "/account.data.csv";
        File.WriteAllText(actualPath, csv.ToString());
    }

    public void addTransaction(Transaction transaction) {
        if (transactions == null) this.transactions = new List<Transaction>();
        this.transactions.Add(transaction);

        this.numTransactions = this.transactions.Count;
        this.totalValue += transaction.amount;
    }

    public string toString() {
        return string.Format(
            "Account: {0}\tType: {1}\tTotal Value ${2}\t# of Transactions: {3}",
            this.accountName, this.accountType, this.totalValue, this.numTransactions
        );
    }

    public void printAccountTransactions() {
        string firstLine = $"Transaction for {this.toString()}:";
        Console.WriteLine(firstLine);

        if (this.transactions == null) return;

        int id = 0;
        foreach (Transaction transaction in this.transactions) {
            string line = $"{id}. {transaction.toString()}";
            Console.WriteLine(line);
        }
    }
}