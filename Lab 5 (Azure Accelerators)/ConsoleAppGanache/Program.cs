using System;
using System.Threading.Tasks;
using BlockChain.SmartContracts.Vehicle1;
using BlockChain.SmartContracts.VehicleRegistry1;
using Nethereum.Geth;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts.Managed;


namespace ConsoleAppGanache
{
    static class Program
    {
        private const string Endpoint = "http://localhost:7545";

        // Note that the password does not matter when connecting to local Ganache
        private static readonly ManagedAccount ContractOwner = new ManagedAccount("0x5f8206Cb73897BF21ECA2305b17DDc09FCC06eDA", "test");

        private static string _registryAddress;
        private static string _vehicleAddress1;
        private static string _vin1;
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
            Console.WriteLine("Local Ganache - VehicleRegistry1");

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

            var gasForDeployContract = new HexBigInteger(4000000);
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Deploying Contract VehicleRegistry1Service...");
            _registryAddress = await VehicleRegistry1Service.DeployContractAsync(web3, ContractOwner.Address, "a", "b", null, gasForDeployContract);
            IVehicleRegistry1Service registryService = new VehicleRegistry1Service(web3, _registryAddress);

            await registryService.OpenRegistryAsync(ContractOwner.Address);

            Console.WriteLine(DateTime.Now);

            _vin1 = "VIN: 1000";
            _vehicleAddress1 = await Vehicle1Service.DeployContractAsync(web3, ContractOwner.Address, _registryAddress, "L-1234", _vin1,  "blue", null, gasForDeployContract);
            Console.WriteLine("_vehicleAddress1 = " + _vehicleAddress1);

            _vin2 = "VIN: 2000";
            _vehicleAddress2 = await Vehicle1Service.DeployContractAsync(web3, ContractOwner.Address, _registryAddress, "L-9999", _vin2, "red", null, gasForDeployContract);
            Console.WriteLine("_vehicleAddress2 = " + _vehicleAddress2);

            var num = await registryService.GetNumberOfRegisteredVehiclesCallAsync();
            Console.WriteLine(num);

            var v1Address = await registryService.GetVehicleAtIndexCallAsync(0);
            Console.WriteLine(v1Address);
            IVehicle1Service vehicleService = new Vehicle1Service(web3, v1Address);

            string l = await vehicleService.LicenseNumberCallAsync();
            Console.WriteLine(l);

            bool isRegistered = await registryService.IsRegisteredVehicleLicenseNumberCallAsync("L-9999");
            Console.WriteLine(isRegistered);
        }
    }
}