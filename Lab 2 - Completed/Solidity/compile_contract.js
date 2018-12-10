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
const generateAllInterfacesAndImplementations = process.argv.length > 5 ? JSON.parse(process.argv[5]) : false;
const combineContracts = process.argv.length > 6 ? JSON.parse(process.argv[6]) : false;

const contractBaseFilename = path.basename(inputContractFilename);
const inputContractContent = fs.readFileSync(inputContractFilename, 'utf-8');

const sources = {};
sources[contractBaseFilename] = { content: inputContractContent };

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

function stripContractContent(contractContent, baseContractName) {
    const lines = contractContent.split('\r\n');

    for (let index = 0; index < lines.length; index += 1) {
        // Remove pragma solidity, except for baseContract
        if (lines[index].startsWith('pragma solidity')) {
            if (!baseContractName) {
                lines[index] = '';
            }
        }

        // Remove all import statements
        if (lines[index].startsWith('import')) {
            lines[index] = '';
        }

        // Remove all contract lines, except for baseContract
        if (lines[index].startsWith('contract')) {
            if (baseContractName) {
                lines[index] = `contract ${baseContractName} {`;
            } else {
                lines[index] = '';
            }
        }
    }

    lines[lines.length - 1] = '';

    return lines.join('\r\n');
}

function generateContractService(outPath, contractName, ns, abi, bytecode, generatorName) {
    const serviceFilename = path.join(`../../Common/Solidity/templates/${generatorName}.ejs`);
    const interfaceFilename = path.join(`../../Common/Solidity/templates/${generatorName}-interface.ejs`);

    const combinedInput = {
        _contractName: contractName,
        abi: JSON.parse(abi),
        bytecode,
        namespace: ns,
    };

    console.log(`${contractName}: generate C# interface(s)`);
    const templateInterface = ejs.compile(fs.readFileSync(interfaceFilename, 'utf8'));
    fs.writeFileSync(path.join(outPath, `I${contractName}Service.Generated.cs`), templateInterface(combinedInput));

    console.log(`${contractName}: generate C# implementation(s)`);
    const templateService = ejs.compile(fs.readFileSync(serviceFilename, 'utf8'));
    fs.writeFileSync(path.join(outPath, `${contractName}Service.Generated.cs`), templateService(combinedInput));
}

console.log(`Compiling contracts with solc version '${solc.version()}'`);

// See https://solidity.readthedocs.io/en/latest/using-the-compiler.html#input-description
const inputObject = {
    language: 'Solidity',
    sources,
    settings: {
        optimizer: {
            enabled: true,
            runs: 10,
        },
        outputSelection: {
            '*': {
                '*': ['abi', 'evm.bytecode.object'],
            },
        },
    },
};

const combinedContractName = contractBaseFilename.replace('.sol', '');
let combinedContractContent = stripContractContent(inputContractContent, combinedContractName);
function solidityResolveImport(contractFilename) {
    console.log(`Resolving contract '${contractFilename}'`);
    const contractContent = fs.readFileSync(path.join(path.dirname(inputContractFilename), contractFilename), 'utf-8');

    if (combineContracts) {
        combinedContractContent = `${combinedContractContent}${stripContractContent(contractContent)}`;
    }

    return { contents: contractContent };
}

const input = JSON.stringify(inputObject);
const output = JSON.parse(solc.compile(input, solidityResolveImport));
if (output.errors && output.errors.length > 0) {
    console.log(`Errors found:\r\n${JSON.stringify(output.errors, null, 4)}`);
}

function generateFilesForContract(contracts, contractFilename) {
    const contractName = Object.keys(contracts[contractFilename])[0];
    const abi = JSON.stringify(contracts[contractFilename][contractName].abi);
    const code = contracts[contractFilename][contractName].evm.bytecode.object;

    const abiFilename = path.join(outputPath, `${contractName}.abi`);
    const binFilename = path.join(outputPath, `${contractName}.bin`);

    console.log(`${contractName}: generate ABI and ByteCode`);
    fs.writeFileSync(abiFilename, abi, 'utf-8');
    fs.writeFileSync(binFilename, abi, 'utf-8');

    console.log(`${contractName}: generate C# helper classes`);
    fs.writeFileSync(path.join(outputPath, `${contractName}.Generated.cs`), generateContractClass(contractName, preferredNamespace, abi, code), 'utf-8');

    generateContractService(outputPath, contractName, preferredNamespace, abi, code, 'cs-service');
}

if (combineContracts) {
    console.log(`Generate combined contract file '${contractBaseFilename}'`);
    fs.writeFileSync(path.join(outputPath, contractBaseFilename), `${combinedContractContent}\r\n}`, 'utf-8');
}

if (generateAllInterfacesAndImplementations) {
    Object.keys(output.contracts).forEach((contractFilename) => {
        generateFilesForContract(output.contracts, contractFilename);
    });
} else {
    generateFilesForContract(output.contracts, contractBaseFilename);
}
/* eslint-enable no-console */
