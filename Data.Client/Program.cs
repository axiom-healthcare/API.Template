using Data.Models;
using System;

namespace Data.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var contextBuilder = Config.GetDbContextOptions();

            using (var data = new Context(contextBuilder))
            {
                data.Entities.Add(new Entity()
                {
                    Name = "Test"
                });
                data.SaveChanges();
            }

            Console.WriteLine("Entity Created");
        }
    }
}
