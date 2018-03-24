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
        /// <summary>
        /// Simple ConsoleApp to connect to Ethereum Consortium Blockchain on Azure to deploy a contract and run functions and transactions.
        /// This project depends on the Solidity project from Lab 2. So make sure to run `npm run build`.
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine("Blockchain - Ethereum - ConsoleApp");

            string senderAddress = "0x???"; // TODO 1
            string password = "test";

            Console.WriteLine(new string('-', 80));

            TestService(senderAddress, password).Wait(1000 * 60 * 5); // Wait max 5 minutes

            Console.WriteLine(new string('-', 80));
        }

        private static async Task TestService(string fromAddress, string password)
        {
            var account = new ManagedAccount(fromAddress, password);

            var web3 = new Web3Geth(account, "http://???.westeurope.cloudapp.azure.com:8545/"); // TODO 2

            bool deployNewContract = true;
            string contractAddress = null;
            if (deployNewContract)
            {
                var gasForDeployContract = new HexBigInteger(1500000);
                Console.WriteLine("Deploying contract (can take some time)");
                contractAddress = await SimpleStorageContractService.DeployContractAsync(web3, fromAddress, 1, "mstack.nl", null, gasForDeployContract).ConfigureAwait(false);
                Console.WriteLine($"Deploying contract done, address = {contractAddress}");
            }

            // Create an instance from the SimpleStorageContractService service which
            // abstracts all calls to the SmartContract.
            ISimpleStorageContractService service = new SimpleStorageContractService(web3, contractAddress);

            bool setNumberResult = await service.ExecuteTransactionAsync(srv => srv.SetNumberAsync(fromAddress, 500)).ConfigureAwait(false);
            Console.WriteLine($"setNumberResult = '{setNumberResult}'.");

            var getNumberValue = await service.GetNumberCallAsync(fromAddress).ConfigureAwait(false);
            Console.WriteLine($"The stored number value is '{getNumberValue}'.");

            bool setStringResult = await service.ExecuteTransactionAsync(srv => srv.SetStringAsync(fromAddress, "mstack.nl test"));
            Console.WriteLine($"setStringResult = '{setStringResult}'.");

            var getStringValue = await service.GetStringCallAsync(fromAddress).ConfigureAwait(false);
            Console.WriteLine($"The stored string value is '{getStringValue}'.");
        }
    }
}