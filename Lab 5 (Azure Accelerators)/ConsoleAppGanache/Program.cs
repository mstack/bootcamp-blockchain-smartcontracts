using BlockChain.SmartContracts.Vehicle1;
using BlockChain.SmartContracts.VehicleRegistry1;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts.Managed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;


namespace ConsoleAppGanache
{
    static class Program
    {
        private const string Endpoint = "http://localhost:7545";

        // Note that the password does not matter when connecting to local Ganache
        private static readonly ManagedAccount ContractOwner = new ManagedAccount("0x5f8206Cb73897BF21ECA2305b17DDc09FCC06eDA", "test");
        private static readonly Web3Geth web3 = new Web3Geth(ContractOwner, Endpoint);

        private static IVehicleRegistry1Service registryService;
        private static string _registryAddress;

        private static IVehicle1Service vehicleService1;
        private static string _vehicleAddress1;
        private static string _vin1;

        private static IVehicle1Service vehicleService2;
        private static string _vehicleAddress2;
        private static string _vin2;

        /// <summary>
        /// ConsoleApp to connect to local Ganache.
        /// 
        /// https://github.com/trufflesuite/ganache/
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Local Ganache - VehicleRegistry1 & Vehicle1");

            Console.WriteLine(new string('-', 80));
            TestServiceAsync().GetAwaiter().GetResult();
            Console.WriteLine(new string('_', 80));
        }

        private static async Task TestServiceAsync()
        {
            await DeployVehicleRegistryAsync();

            await OpenVehicleRegistryAsync();

            await DeployVehiclesAsync();

            var num = await registryService.GetNumberOfRegisteredVehiclesCallAsync();
            Console.WriteLine(num);

            bool isRegistered = await registryService.IsRegisteredVehicleLicenseNumberCallAsync("L-9999");
            Console.WriteLine(isRegistered);

            string licenseNumber = await vehicleService2.LicenseNumberCallAsync();
            Console.WriteLine("licenseNumber = " + licenseNumber);

            string mediaUrl = await vehicleService2.MediaUriCallAsync();
            Console.WriteLine("mediaUrl = " + mediaUrl);
        }

        private static async Task OpenVehicleRegistryAsync()
        {
            var contractUpdatedEvent = registryService.GetWorkbenchContractUpdated();
            var contractUpdatedEventFilter = await contractUpdatedEvent.CreateFilterAsync();

            var receipt = await registryService.ExecuteTransactionAsync(srv => srv.OpenRegistryAsync(ContractOwner.Address));
            Console.WriteLine("OpenRegistryAsync : receipt.TransactionHash " + receipt.TransactionHash);

            Console.WriteLine("Waiting for GetWorkbenchContractUpdated events:");
            bool eventReceived = false;
            do
            {
                var contractUpdatedEvents = await contractUpdatedEvent.GetAllChanges(contractUpdatedEventFilter);

                foreach (var @event in contractUpdatedEvents)
                {
                    eventReceived = true;
                    Console.WriteLine("event: " + JsonConvert.SerializeObject(@event.Event, Formatting.Indented));
                }
            } while (!eventReceived);
        }

        private static async Task DeployVehicleRegistryAsync()
        {
            var gasForDeployContract = new HexBigInteger(4000000);
            Console.WriteLine("Deploying Contract VehicleRegistry1Service...");
            _registryAddress = await VehicleRegistry1Service.DeployContractAsync(web3, ContractOwner.Address, "a", "b", null, gasForDeployContract);
            registryService = new VehicleRegistry1Service(web3, _registryAddress);
        }

        private static async Task DeployVehiclesAsync()
        {
            Console.WriteLine("Deploying Vehicles...");
            var gasForDeployContract = new HexBigInteger(4000000);
            _vin1 = "VIN: 1000";
            _vehicleAddress1 = await Vehicle1Service.DeployContractAsync(web3, ContractOwner.Address, _registryAddress, "L-1234", _vin1, "blue", 2000, null, gasForDeployContract);
            Console.WriteLine("_vehicleAddress1 = " + _vehicleAddress1);
            vehicleService1 = new Vehicle1Service(web3, _vehicleAddress1);

            _vin2 = "VIN: 2000";
            _vehicleAddress2 = await Vehicle1Service.DeployContractAsync(web3, ContractOwner.Address, _registryAddress, "L-9999", _vin2, "red", 2018, null, gasForDeployContract);
            Console.WriteLine("_vehicleAddress2 = " + _vehicleAddress2);
            vehicleService2 = new Vehicle1Service(web3, _vehicleAddress2);

            Func<IVehicle1Service, Task<string>> func = (srv) => srv.AddMediaAsync(ContractOwner.Address,
                "https://github.com/StefH/QboxNext/blob/master/resources/logo_256x256.png?raw=true",
                "Hash",
                "MetaDataHash");
            var receipt = await vehicleService2.ExecuteTransactionAsync(func);

            await vehicleService2.AddMediaAsync(ContractOwner.Address,
                "https://github.com/StefH/QboxNext/blob/master/resources/logo_256x256.png?raw=true",
                "Hash",
                "MetaDataHash");
        }
    }
}