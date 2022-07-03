
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
    AccountType accountType { get; set; }
    double totalValue { get; set; }
    List<Transaction>? transactions;
    int numTransactions { get; set; }
    
    public Account() {
        mountMetadata();
        mountData();
    }

    private void mountMetadata() {
        /**
         * Retrieves necessary metadata about the account from corresponding csv file
         */
        using (var reader = new StreamReader("./account.metadata.csv")) {
            // Parsing line into list of string values
            string? line = reader.ReadLine();
            if (line == null) {
                throw new Exception(
                    "Error with account.metadata.csv: data not there"
                );
            }
            string[] values = line.Split(';');

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
        using (var reader = new StreamReader("account.data.csv")) {
            while (!reader.EndOfStream) {
                string? line = reader.ReadLine();
                if (line == null) {
                    throw new Exception(
                        "Error with account.data.csv: data not there"
                    );
                }
                string[] values = line.Split(';');

                
            }
        }
    }
}