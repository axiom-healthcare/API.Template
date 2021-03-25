using Data.Models;
using System;

namespace Data.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use Data Access Components to perform opertions on the Data Store
            using (var Data = new Context(Config.GetDbContextOptions()))
            {
                // The `Entities` class in the `Data` namspace may seem strange. 
                // `Entities` was used to convey the message that a model may represent any `Entity`.
                // Example: If you have `Customers` in your Data Store and you want to add a new `Customer` then `Entities` below would be `Customers`
                Data.Entities.Add(new Entity()
                    {
                        Name = "Test"
                    });
                Data.SaveChanges();
            }

            Console.WriteLine("Entity Created");
        }
    }
}
