using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.Contracts;

// Created with 'Solidity to C# generator' by stef.heyenrath@mstack.nl (mStack B.V.)
// Based on abi-code-gen (https://github.com/Nethereum/abi-code-gen)
namespace Lab2.SmartContracts
{
    public interface ISimpleStorageContractService
    {
        Task<bool> ExecuteTransactionAsync(Func<Task<string>> func, int timeoutInSeconds = 120);

        Function GetFunctionGetVersion();
        Function GetFunctionSetNumber();
        Function GetFunctionSetString();
        Function GetFunctionGetString();
        Function GetFunctionGetNumber();


        Task<string> GetStringCallAsync();
        Task<string> GetStringCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<BigInteger> GetNumberCallAsync();
        Task<BigInteger> GetNumberCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);

        Task<string> SetNumberAsync(string addressFrom, BigInteger value, HexBigInteger gas = null, HexBigInteger valueAmount = null);
        Task<string> SetStringAsync(string addressFrom, string value, HexBigInteger gas = null, HexBigInteger valueAmount = null);

        Task<GetVersion> GetVersionCallAsync();
        Task<GetVersion> GetVersionCallAsync(string addressFrom,  HexBigInteger gas = null, HexBigInteger valueAmount = null);

    }
}
