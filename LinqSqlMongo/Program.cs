using LinqSqlMongo.DB;
using Microsoft.IdentityModel.Tokens;

namespace LinqSqlMongo
{
    class Program
    {
        static void Main(string[] args)
        {
            var sql = new MySql();
            var mongo = new MyMongo();
            
            string path = "C:\\Data\\licensed-drivers.json";

            var lst = ReadFile.GetData(path);
            int command = 0;
            List<Penalty> tempLst;

            if (lst == null)
            {
                Console.WriteLine("Error reading file");
                return;
            }

            do
            {
                ShowMenu();

                command = ReadCommand();

                switch (command)
                {
                    case 1:
                        Console.Write("\n[1] Raw\n" +
                                      "[2] Ordered By Company name\n\n" +
                                      "[any other number] Back\n\n" +
                                      "Option: ");

                        switch (ReadCommand())
                        {
                            case 1:
                                foreach (var item in lst)
                                    Console.WriteLine(item + "\n");
                                break;
                            case 2:
                                Console.WriteLine("Penalties ordered by company name\n");
                                AppliedPenalties.PrintData(TestFilters.OrderedByCompanyName(lst));
                                break;
                        }

                        break;

                    case 2:
                        Console.WriteLine($"\nRow quantity: {TestFilters.GetCountRecords(lst)}");
                        break;

                    case 3:
                        Console.Write("\nInsert the first three digits: ");

                        tempLst = TestFilters.GetPenaltiesByCPF(lst, ReadString());

                        sql.InsertProcessControl("Filtering by cpf digits", tempLst.Count);

                        AppliedPenalties.PrintData(tempLst);
                        break;

                    case 4:
                        Console.Write("\nInsert penalty Vigency year: ");

                        tempLst = TestFilters.GetPenaltiesByVigencyYear(lst, ReadCommand());

                        sql.InsertProcessControl("Filtering by Vigency year", tempLst.Count);

                        AppliedPenalties.PrintData(tempLst);
                        break;

                    case 5:
                        Console.Write("\nInsert Company name:");

                        tempLst = TestFilters.GetPenaltiesByCompanyName(lst, ReadString());

                        sql.InsertProcessControl("Filtering by Company name", tempLst.Count);

                        AppliedPenalties.PrintData(tempLst);
                        break;

                    case 6:
                        if (!sql.IsEmpty())
                            Console.WriteLine("\nSQL already populated!");
                        else
                        {
                            sql.InsertPenalties(lst);

                            sql.InsertProcessControl("Populating SQL", lst.Count);

                            Console.WriteLine("\nSuccessfully populated!");
                        }

                        break;

                    case 7:
                        if (!mongo.IsEmpty())
                            Console.WriteLine("\nMongo already populated!");
                        else
                        {
                            mongo.InsertPenalties(sql.RetrieveAll());
                            Console.WriteLine("\nSuccessfully populated!");
                        }

                        break;

                    case 8:
                        Console.WriteLine(TestFilters.GenerateXML(lst));
                        break;
                }

            } while (command > 1 && command < 8);
        }

        public static void ShowMenu()
        {
            Console.WriteLine("\n > COMMANDS <\n");

            Console.Write("[1] Show all\n" +
                          "[2] Row Quantity\n" +
                          "[3] Find by CPF\n" +
                          "[4] Find by Vigency Year\n" +
                          "[5] Find by Company Name\n" +
                          "[6] Populate sql\n" +
                          "[7] Populate Mongo\n" +
                          "[8] Show XML\n" +
                          "[any other number] Exit\n\n" +
                          "Option: ");
        }

        public static string ReadString()
        {
            string? str;

            do
            {
                str = Console.ReadLine();
            } while (str.IsNullOrEmpty());

            return str;
        }

        public static int ReadCommand()
        {
            int command;

            try
            {
                command = int.Parse(ReadString());
            }
            catch (Exception)
            {
                Console.WriteLine("This isn't a number! >:(\n");
                return ReadCommand();
            }

            return command;
        }
    }
}