// Copied from https://ethereum.stackexchange.com/questions/11081/install-solc-compiler-on-windows-8/11086#11086
// and
// https://github.com/Nethereum/abi-code-gen
/* eslint-disable no-console */
const solc = require('solc');
const fs = require('fs');
const ejs = require('ejs');
const path = require('path');

const inputContractFilename = process.argv[2];
const outputPath = process.argv[3];
const preferredNamespace = process.argv.length > 4 ? process.argv[4] : 'CustomNameSpace';

const contractBasename = path.basename(inputContractFilename);

const input = {};
input[contractBasename] = fs.readFileSync(inputContractFilename, 'utf-8');

function solidityFindImports(contractFilename) {
    return { contents: fs.readFileSync(path.join(path.dirname(inputContractFilename), contractFilename), 'utf-8') };
}

function generateContractClass(contractName, ns, abi, code) {
    const abiFormatted = abi.replace(/"/g, '""');
    const c = `namespace ${ns}
{
    public class ${contractName}
    {
        public const string ABI = @"${abiFormatted}";
        public const string ByteCode = "0x${code}";
    }
}`;

    return c;
}

function generateContractService(inputPath, outPath, contractName, ns, abi, bytecode, generatorName) {
    const serviceFilename = path.join(`../../Common/Solidity/templates/${generatorName}.ejs`);
    const interfaceFilename = path.join(`../../Common/Solidity/templates/${generatorName}-interface.ejs`);

    const combinedInput = {
        _contractName: contractName,
        abi: JSON.parse(abi),
        bytecode,
        namespace: ns,
    };

    console.log(`${contractName}: generate C# interfaces`);
    const templateInterface = ejs.compile(fs.readFileSync(interfaceFilename, 'utf8'));
    fs.writeFileSync(path.join(outPath, `I${contractName}Service.Generated.cs`), templateInterface(combinedInput));

    console.log(`${contractName}: generate C# implementations`);
    const templateService = ejs.compile(fs.readFileSync(serviceFilename, 'utf8'));
    fs.writeFileSync(path.join(outPath, `${contractName}Service.Generated.cs`), templateService(combinedInput));
}

console.log('Compiling contracts');
const optimizeBinaryCode = 1;
const output = solc.compile({ sources: input }, optimizeBinaryCode, solidityFindImports);
if (output.errors && output.errors.length > 0) {
    console.log('ERRORS found !');
    console.log(JSON.stringify(output.errors, null, 4));
}

Object.keys(output.contracts).forEach((key) => {
    const abi = output.contracts[key].interface;
    const code = output.contracts[key].bytecode;

    // Remove the : at the start from the contractName
    const contractName = key.split(':')[1];

    const abiFilename = path.join(outputPath, `${contractName}.abi`);

    console.log(`${contractName}: generate ABI and ByteCode`);
    fs.writeFileSync(abiFilename, abi, 'utf-8');
    fs.writeFileSync(path.join(outputPath, `${contractName}.bin`), code, 'utf-8');

    console.log(`${contractName}: generate C# helper classes`);
    fs.writeFileSync(path.join(outputPath, `${contractName}.Generated.cs`), generateContractClass(contractName, preferredNamespace, abi, code), 'utf-8');

    generateContractService(inputContractFilename, outputPath, contractName, preferredNamespace, abi, code, 'cs-service');
});
/* eslint-enable no-console */
