using System;
using System.Threading.Tasks;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Geth;
using Lab2.SmartContracts;
using Nethereum.Hex.HexTypes;

namespace ConsoleApp
{
    class Program
    {
        /// <summary>
        /// Simple ConsoleApp to connect to your local Ethereum to deploy a contract and run functions and transactions.
        /// This project depends on the Solidity project.
        /// So make sure to run `npm run build`.
        /// 
        /// Also make sure to start `\Tools\Blockchain\testchain.bat` before running this console app.
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine("Blockchain - Ethereum - ConsoleApp");

            string senderAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            string password = "password";

            Console.WriteLine(new string('-', 80));

            TestService(senderAddress, password).Wait(60000);

            Console.WriteLine(new string('-', 80));
        }

        private static async Task TestService(string fromAddress, string password)
        {
            var account = new ManagedAccount(fromAddress, password);

            var web3 = new Web3Geth(account);

            bool deployNewContract = true; // TODO 0
            string contractAddress = null; // TODO 0
            if (deployNewContract)
            {
                var gasForDeployContract = new HexBigInteger(15000000);
                Console.WriteLine("Deploying contract (can take some time)");
                contractAddress = await SimpleStorageContractService.DeployContractAsync(web3, fromAddress, 1, "mstack.nl", null, gasForDeployContract);
                Console.WriteLine($"Deploying contract done, address = {contractAddress}");
            }

            // Create an instance from the SimpleStorageContractService service which
            // abstracts all calls to the SmartContract.
            ISimpleStorageContractService service = new SimpleStorageContractService(web3, contractAddress);

            // TODO 1:
            // bool setNumberResult =  await service.ExecuteTransactionAsync((srv) => srv.SetNumberAsync(fromAddress, 500));

            // TODO 2:
            // var getNumberValue = await service.GetNumberCallAsync(fromAddress);
            // Console.WriteLine($"The stored number value is '{getNumberValue}'.");

            // TODO 3:
            // bool setStringResult =  await service.ExecuteTransactionAsync((srv) => srv.SetStringAsync(fromAddress, "mstack.nl test"));

            // TODO 4:
            // var getStringValue = await service.GetStringCallAsync(fromAddress);
            // Console.WriteLine($"The stored string value is '{getStringValue}'.");
        }
    }
}