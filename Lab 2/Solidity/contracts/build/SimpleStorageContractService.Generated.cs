using System;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;

// Created with 'Solidity to C# generator' by stef.heyenrath@mstack.nl (mStack B.V.)
// Based on abi-code-gen (https://github.com/Nethereum/abi-code-gen)
namespace Lab2.SmartContracts
{
    public class SimpleStorageContractService : ISimpleStorageContractService
    {
        public static string ABI = @"[{""constant"":true,""inputs"":[],""name"":""getVersion"",""outputs"":[{""name"":"""",""type"":""int256""},{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""value"",""type"":""uint256""}],""name"":""setNumber"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""name"":""value"",""type"":""string""}],""name"":""setString"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""getString"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""getNumber"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""name"":""version"",""type"":""int256""},{""name"":""description"",""type"":""string""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""}]";

        public static string ByteCode = "0x6060604052341561000f57600080fd5b6040516105803803806105808339810160405280805191906020018051600084905590910190506001818051610049929160200190610051565b5050506100ec565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061009257805160ff19168380011785556100bf565b828001600101855582156100bf579182015b828111156100bf5782518255916020019190600101906100a4565b506100cb9291506100cf565b5090565b6100e991905b808211156100cb57600081556001016100d5565b90565b610485806100fb6000396000f30060606040526004361061006c5763ffffffff7c01000000000000000000000000000000000000000000000000000000006000350416630d8e6e2c81146100715780633fb5c1cb146101025780637fcaf6661461011a57806389ea642f1461016b578063f2c9ecd8146101f5575b600080fd5b341561007c57600080fd5b61008461021a565b60405182815260406020820181815290820183818151815260200191508051906020019080838360005b838110156100c65780820151838201526020016100ae565b50505050905090810190601f1680156100f35780820380516001836020036101000a031916815260200191505b50935050505060405180910390f35b341561010d57600080fd5b6101186004356102cd565b005b341561012557600080fd5b61011860046024813581810190830135806020601f820181900481020160405190810160405281815292919060208401838380828437509496506102e995505050505050565b341561017657600080fd5b61017e610300565b60405160208082528190810183818151815260200191508051906020019080838360005b838110156101ba5780820151838201526020016101a2565b50505050905090810190601f1680156101e75780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b341561020057600080fd5b6102086103a9565b60405190815260200160405180910390f35b60006102246103af565b6000546001808054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156102be5780601f10610293576101008083540402835291602001916102be565b820191906000526020600020905b8154815290600101906020018083116102a157829003601f168201915b50505050509050915091509091565b600a8110156102e05760016002556102e6565b600a6002555b50565b60038180516102fc9291602001906103c1565b5050565b6103086103af565b60038054600181600116156101000203166002900480601f01602080910402602001604051908101604052809291908181526020018280546001816001161561010002031660029004801561039e5780601f106103735761010080835404028352916020019161039e565b820191906000526020600020905b81548152906001019060200180831161038157829003601f168201915b505050505090505b90565b60025490565b60206040519081016040526000815290565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061040257805160ff191683800117855561042f565b8280016001018555821561042f579182015b8281111561042f578251825591602001919060010190610414565b5061043b92915061043f565b5090565b6103a691905b8082111561043b57600081556001016104455600a165627a7a7230582036e71d1aeef8f8e1590a28d533e0c70a18f3ebf6afebf34c785b991cac8a22ad0029";

        public static async Task<string> DeployContractAsync(Web3 web3, string addressFrom, BigInteger version, string description, CancellationTokenSource token = null, HexBigInteger gas = null)
        {
            if (gas == null)
            {
                BigInteger estimatedGas = await web3.Eth.DeployContract.EstimateGasAsync(ABI, ByteCode, addressFrom).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            var transactionReceipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(ABI, ByteCode, addressFrom, gas, token, version, description).ConfigureAwait(false);
            return transactionReceipt.ContractAddress;
        }

        private readonly Web3 _web3;
        private readonly Contract _contract;

        public SimpleStorageContractService(Web3 web3, string address)
        {
            _web3 = web3;
            _contract = _web3.Eth.GetContract(ABI, address);
        }

        public async Task<bool> ExecuteTransactionAsync(Func<Task<string>> func, int timeoutInSeconds = 120)
        {
            string transaction = await func();
            var receipt = await _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaction).ConfigureAwait(false);

            int count = 0;
            while (receipt == null && count < timeoutInSeconds * 2)
            {
                await Task.Delay(500);
                receipt = await _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transaction).ConfigureAwait(false);
                count++;
            }

            return receipt != null;
        }

        public Function GetFunctionGetVersion()
        {
            return _contract.GetFunction("getVersion");
        }

        public Function GetFunctionSetNumber()
        {
            return _contract.GetFunction("setNumber");
        }

        public Function GetFunctionSetString()
        {
            return _contract.GetFunction("setString");
        }

        public Function GetFunctionGetString()
        {
            return _contract.GetFunction("getString");
        }

        public Function GetFunctionGetNumber()
        {
            return _contract.GetFunction("getNumber");
        }



        public Task<string> GetStringCallAsync()
        {
            var function = GetFunctionGetString();
            return function.CallAsync<string>();
        }

        public async Task<string> GetStringCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetString();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<string>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }

        public Task<BigInteger> GetNumberCallAsync()
        {
            var function = GetFunctionGetNumber();
            return function.CallAsync<BigInteger>();
        }

        public async Task<BigInteger> GetNumberCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetNumber();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallAsync<BigInteger>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }


        public async Task<string> SetNumberAsync(string addressFrom, BigInteger value, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionSetNumber();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, value).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, value).ConfigureAwait(false);
        }

        public async Task<string> SetStringAsync(string addressFrom, string value, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionSetString();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount, value).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.SendTransactionAsync(addressFrom, gas, valueAmount, value).ConfigureAwait(false);
        }


        public Task<GetVersion> GetVersionCallAsync()
        {
            var function = GetFunctionGetVersion();
            return function.CallDeserializingToObjectAsync<GetVersion>();
        }

        public async Task<GetVersion> GetVersionCallAsync(string addressFrom,   HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionGetVersion();

            if (gas == null)
            {
                BigInteger estimatedGas = await function.EstimateGasAsync(addressFrom, gas, valueAmount).ConfigureAwait(false);
                gas = new HexBigInteger(estimatedGas);
            }

            return await function.CallDeserializingToObjectAsync<GetVersion>(addressFrom, gas, valueAmount).ConfigureAwait(false);
        }



    }

    [FunctionOutput]
    public class GetVersion
    {
        [Parameter("int256", "", 1)]
        public BigInteger A { get; set; }

        [Parameter("string", "", 2)]
        public string B { get; set; }

    }



}
