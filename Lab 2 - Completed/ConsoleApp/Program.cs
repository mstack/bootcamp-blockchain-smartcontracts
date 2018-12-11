using System;
using System.Threading.Tasks;
using Lab2.SmartContracts;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;

namespace ConsoleApp
{
    class Program
    {
        private const string Endpoint = "http://localhost:7545"; // Ganache listens on this endpoint

        /// <summary>
        /// Simple ConsoleApp to connect to Ganache (personal Ethereum blockchain) to deploy a contract and run functions and transactions.
        /// This project depends on the Solidity project.
        /// So make sure to run `npm run build`.
        /// 
        /// Also make sure to start `Ganache` before running this console app.
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine("Blockchain - Ethereum - ConsoleApp - Ganache");

            string senderAddress = "0x5f8206Cb73897BF21ECA2305b17DDc09FCC06eDA";
            string password = "test";

            Console.WriteLine(new string('-', 80));

            TestService(senderAddress, password).Wait(1000 * 60 * 5); // Wait max 5 minutes

            Console.WriteLine(new string('-', 80));
        }

        private static async Task TestService(string fromAddress, string password)
        {
            var account = new ManagedAccount(fromAddress, password);

            var web3 = new Web3Geth(account, Endpoint);

            bool deployNewContract = true;
            string contractAddress = null;
            if (deployNewContract)
            {
                var gasForDeployContract = new HexBigInteger(3000000);
                Console.WriteLine("Deploying contract");
                contractAddress = await SimpleStorageContractService.DeployContractAsync(web3, fromAddress, 1, "mstack.nl", null, gasForDeployContract);
                Console.WriteLine($"Deploying contract done, address = {contractAddress}");
            }

            // Create an instance from the SimpleStorageContractService service which abstracts all calls to the SmartContract.
            ISimpleStorageContractService service = new SimpleStorageContractService(web3, contractAddress);

            var setNumberResult = await service.ExecuteTransactionAsync(srv => srv.SetNumberAsync(fromAddress, 500));

            var getNumberValue = await service.GetNumberCallAsync(fromAddress);
            Console.WriteLine($"The stored number value is '{getNumberValue}'.");

            var setStringResult = await service.ExecuteTransactionAsync(srv => srv.SetStringAsync(fromAddress, "mstack.nl test"));

            var getStringValue = await service.GetStringCallAsync(fromAddress);
            Console.WriteLine($"The stored string value is '{getStringValue}'.");
        }
    }
}