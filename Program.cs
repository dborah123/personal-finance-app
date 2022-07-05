// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Account account0 = new Account("./test-file");
DateTime datetime = DateTime.Now;
Transaction transaction = new Transaction(2, 20.0, TransactionType.FURNITURE, datetime);
account0.addTransaction(transaction);
Console.WriteLine(account0.toString());
account0.printAccountTransactions();

account0.unmountMetadata();
account0.unmountData();