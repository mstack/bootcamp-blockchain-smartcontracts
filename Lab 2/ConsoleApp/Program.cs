using System;
using System.Threading.Tasks;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Geth;
using Lab2.SmartContracts;

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

            Console.WriteLine(new string('_', 80));
        }

        private static async Task TestService(string fromAddress, string password)
        {
            var account = new ManagedAccount(fromAddress, password);

            var web3 = new Web3Geth(account);

            Console.WriteLine("Deploying contract (can take some time)");
            string contractAddress = await SimpleStorageContractService.DeployContractAsync(web3, fromAddress, 1, "mstack.nl");
            Console.WriteLine($"Deploying contract done, address = {contractAddress}");

            ISimpleStorageContractService service = new SimpleStorageContractService(web3, contractAddress);

            // TODO 1:
            // await service.ExecuteTransactionAsync((srv) => srv.SetNumberAsync(fromAddress, 500));
        }
    }
}