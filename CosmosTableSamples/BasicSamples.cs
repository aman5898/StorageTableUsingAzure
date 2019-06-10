using System;

namespace CosmosTableSamples
{
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos.Table;
    using Model;

    class BasicSamples
    {
        public async Task RunSamples()
        {
            Console.WriteLine("Azure Cosmos DB Table");
            Console.WriteLine();
            Console.WriteLine("Enter your Table Name");
            string str1 = Console.ReadLine();
            string tableName = str1;

            // Create or reference an existing table
            CloudTable table = await Common.CreateTableAsync(tableName);

            try
            {
                // Demonstrate basic CRUD functionality 
                await BasicDataOperationsAsync(table);
            }
            finally
            {
                //Delete the table
                //await table.DeleteIfExistsAsync();
                Console.WriteLine("App Ended");
            }
        }

        private static async Task BasicDataOperationsAsync(CloudTable table)
        {
            Console.WriteLine("Enter the count of entries you will make");
            int count=Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Enter details of user " + (i+1));
                Console.WriteLine();
                Console.WriteLine("Enter your first name");
                string str1 = Console.ReadLine();
                Console.WriteLine("Enter your last name");
                string str2 = Console.ReadLine();
                Console.WriteLine("Enter your email Address");
                string str3 = Console.ReadLine();
                Console.WriteLine("Enter your Phone number");
                string str4 = Console.ReadLine();
                // Create an instance of a customer entity. See the Model\CustomerEntity.cs for a description of the entity.
                CustomerEntity customer = new CustomerEntity(str1, str2)
                {
                    Email = str3,
                    PhoneNumber = str4
                };

                // Demonstrate how to insert the entity
                Console.WriteLine("Insert an Entity.");
                await SamplesUtils.InsertOrMergeEntityAsync(table, customer);
            }


            // Demonstrate how to Update the entity by changing the phone number
            //Console.WriteLine("Update an existing Entity using the InsertOrMerge Upsert Operation.");
            //customer.PhoneNumber = "9810738851";
            //await SamplesUtils.InsertOrMergeEntityAsync(table, customer);
            //Console.WriteLine();

            // Demonstrate how to Read the updated entity using a point query 
            Console.WriteLine("Retrieving data from cloud");
            Console.WriteLine("Enter The first name");
            string strFname = Console.ReadLine();
            Console.WriteLine("Enter your last name");
            string strLname = Console.ReadLine();
            //Console.WriteLine("Reading the updated Entity.");
            await SamplesUtils.RetrieveEntityUsingPointQueryAsync(table, strFname, strLname);
            Console.WriteLine();

            // Demonstrate how to Delete an entity
            //Console.WriteLine("Delete the entity. ");
            //await SamplesUtils.DeleteEntityAsync(table, customer);
            //Console.WriteLine();



            //TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>()
            //    .Where(
            //        TableQuery.CombineFilters(
            //            TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Aman"),
            //            TableOperators.And,
            //            TableQuery.GenerateFilterCondition("Email", QueryComparisons.Equal, "amn@gmail.com")
            //    ));

            //await table.ExecuteQuerySegmentedAsync<CustomerEntity>(query, null);
            //Console.WriteLine("AmanPrint");
        }
    }
}
