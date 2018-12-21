using System;
using System.Threading.Tasks;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts.Managed;
using Newtonsoft.Json;
using SupplyChain.BlockChain.SmartContracts;

namespace ConsoleAppGanache
{
    static class Program
    {
        private const string Endpoint = "http://localhost:7545";

        // Note that the password does not matter when connecting to local Ganache
        private static readonly ManagedAccount ContractOwner = new ManagedAccount("0x5f8206Cb73897BF21ECA2305b17DDc09FCC06eDA", "test");
        //private static readonly ManagedAccount Supplier1 = new ManagedAccount("0xD1280ecfD9E1Ca67FA4E8B961dBB5544c47E1aBc", "test");
        //private static readonly ManagedAccount Supplier2 = new ManagedAccount("0x78Ef5a39469624059552b699Ecb5f733D6aD1926", "test");
        private static readonly ManagedAccount Manufacturer1 = new ManagedAccount("0x67c0fFE1B87fa06e84BEf1B3fB90a9419A43778a", "test");
        //private static readonly ManagedAccount Manufacturer2 = new ManagedAccount("0xA33903341B76b8187eaBac0c0371DB6bb70C549a", "test");

        private static string _contractAddress;

        /// <summary>
        /// ConsoleApp to connect to local Ganache.
        /// 
        /// https://github.com/trufflesuite/ganache/
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Local Ganache - SupplyChainLog");

            Console.WriteLine(new string('-', 80));

            // Wait 15 minutes
            TestService().Wait(15 * 60 * 1000);

            Console.WriteLine(new string('_', 80));
        }

        //private static uint ToEpoch(DateTime date)
        //{
        //    return (uint)(date - new DateTime(1970, 1, 1)).TotalSeconds;
        //}

        private static async Task TestService()
        {
            var web3 = new Web3Geth(ContractOwner, Endpoint);

            // var gasForDeployContract = new HexBigInteger(3000000); // 2959216
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Deploying Contract...");
            _contractAddress = await SupplyChainLogContractService.DeployContractAsync(web3, ContractOwner.Address, null, null);
            Console.WriteLine("Deploying Contract done");

            Console.WriteLine(DateTime.Now);
            await TestOrders();
        }

        private static async Task TestOrders()
        {
            Console.WriteLine("TestOrders");
            var web3Manufacturer = new Web3Geth(Manufacturer1, Endpoint);

            ISupplyChainLogContractService service = new SupplyChainLogContractService(web3Manufacturer, _contractAddress);

            var orderEvent = service.GetAddOrderEvent();
            var orderFilterAll = await orderEvent.CreateFilterAsync();

            await service.ExecuteTransactionAsync(srv => srv.AddOrderAsync(Manufacturer1.Address, "ean 1"));
            await service.ExecuteTransactionAsync(srv => srv.AddOrderAsync(Manufacturer1.Address, "ean 2"));
            await service.ExecuteTransactionAsync(srv => srv.AddOrderAsync(Manufacturer1.Address, "ean 3"));

            try
            {
                await service.ExecuteTransactionAsync(srv => srv.AddOrderAsync(Manufacturer1.Address, "ean error"));
            }
            catch (Exception)
            {
                Console.WriteLine("Adding order with wrong ean fails");
            }

            var addLog = await orderEvent.GetFilterChanges(orderFilterAll);
            Console.WriteLine("OrderEvent log:");
            foreach (var log in addLog)
            {
                Console.WriteLine("log: " + JsonConvert.SerializeObject(log, Formatting.Indented));
            }

            var count = await service.GetOrderCountCallAsync(Manufacturer1.Address);
            Console.WriteLine("count: " + count);

            for (int idx = 0; idx < count; idx++)
            {
                var orderId = await service.GetOrderIdAtIndexCallAsync(Manufacturer1.Address, idx);
                var o = await service.GetOrderByIdCallAsync(Manufacturer1.Address, orderId);
                Console.WriteLine("order[" + idx + "] = " + JsonConvert.SerializeObject(new { order = o, uuid = new Guid(o.Id) }, Formatting.Indented));
            }
        }
    }
}