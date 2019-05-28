using System;
using System.Numerics;
using System.Threading.Tasks;
using DefaultNamespace;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;

namespace ConsoleAppRopsten
{
    static class Program
    {
        private const string Endpoint = "https://ropsten.infura.io/v3/f4cdb4bfdc9d40078ac87d737f1d5c9f";

        private static string AccountAddress = "0xB2177c1A1BB4B81cd218f791a7cF792D60C9B3e0";

        // https://nethereum.readthedocs.io/en/latest/Nethereum.Workbooks/docs/nethereum-using-account-objects/#sending-a-transaction
        private static readonly Account Account = new Account("3351FD107238FDC877840015AFD2009225473F24180550D0A0FE7F92FFEFACCE");

        private static string _contractAddress = "0x28aec8f8ddaa699cfe1786848f363d07101966b8";

        public static void Main(string[] args)
        {
            TestService().Wait(15 * 60 * 1000);
        }

        private static async Task TestService()
        {
            var web3 = new Web3Geth(Account, Endpoint);

            var balance = await web3.Eth.GetBalance.SendRequestAsync(AccountAddress);
            Console.WriteLine($"Current balance = {balance.Value}");

            Console.WriteLine("Deploying Contract...");
            _contractAddress = await SimpleStorageContractService.DeployContractAsync(web3, AccountAddress, new BigInteger(5), "xx", null, new HexBigInteger(1000000));
            Console.WriteLine($"Deploying Contract done. Address = {_contractAddress}"); // 

            await TestOther();
        }

        private static async Task TestOther()
        {
            Console.WriteLine("TestOther");
            var web3 = new Web3Geth(Account, Endpoint);

            ISimpleStorageContractService service = new SimpleStorageContractService(web3, _contractAddress);

            string s1 = await service.GetStringCallAsync(AccountAddress);
            Console.WriteLine($"GetStringCallAsync: {s1}");

            await service.ExecuteTransactionAsync(srv => srv.SetStringAsync(AccountAddress, "stef_" + DateTime.Now));

            string s2 = await service.GetStringCallAsync(AccountAddress);
            Console.WriteLine($"GetStringCallAsync: {s2}");
        }
    }
}