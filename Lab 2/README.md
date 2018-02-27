# Lab 2

## Info

This lab (continues on Lab 1) and will teach you how to:

- run code coverage to see how much code is covered by unit-tests
- auto generate C# Interfaces and Service classes from the SmartContract
- deploy the SmartContract using a bare-bone console application to your local Ethereum Blockchain using GETH

## What is Go-Ethereum (GETH)

Go Ethereum is one of the three original implementations (along with C++ and Python) of the Ethereum protocol. It is written in **Go**, fully open source and licensed under the GNU LGPL v3. See also [geth.ethereum.org](https://geth.ethereum.org/).

If you have some generic or detailed questions, please feel free to ask these at mStack colleagues.

## Folder structure

- `Solidity`: folder for the SmartContracts written in Solidity and unit-tests written in javascript.
- `ConsoleApp`: folder for the SmartContracts written in Solidity and unit-tests written in javascript.

## Details

This lab contains two simple SmartContracts:

- `SimpleStorageContract` this contract can store and retrieve number and string values
- `MultiplyContract` this contract is initialized with a default multiply factor and can do some basic multiplications

## Steps

Execute the following steps in order to follow this Lab.

### Step 1

Go to the terminal in VS Code and type:

- `cd Lab 2`
- `cd Solidity`
- `npm i` (to install all required node packages)

### Step 2

Go to the terminal in VS Code and type:

- `npm test`, this command will use [Truffle](https://github.com/trufflesuite/truffle) run the test. Note that also the linter is run.

All tests should pass.

### Step 3

Go to the terminal in VS Code and type:
`npm run coverage`, this command will start a **TestRPC** (a local in-memory Ethereum Virtual Machine in JavaScript) and **Solidity Coverage** to determine the unit-test code coverage.

In the console output, you should see something like:

``` x
[Coverage] ----------------------------|----------|----------|----------|----------|----------------|
[Coverage] File                        |  % Stmts | % Branch |  % Funcs |  % Lines |Uncovered Lines |
[Coverage] ----------------------------|----------|----------|----------|----------|----------------|
[Coverage]  contracts\                 |    77.78 |       50 |    83.33 |    77.78 |                |
[Coverage]   SimpleStorageContract.sol |    77.78 |       50 |    83.33 |    77.78 |          18,37 |
[Coverage] ----------------------------|----------|----------|----------|----------|----------------|
[Coverage] All files                   |    77.78 |       50 |    83.33 |    77.78 |                |
[Coverage] ----------------------------|----------|----------|----------|----------|----------------|
```

A more detailed code coverage HTML file is generated at `Lab 2\Solidity\coverage\index.html`. Open this file in an interner browser. This should look like:
![Coverage](coverage-lab2.png)

Click the **contracts** link and drill down to the contract file:
![Coverage](coverage-contract-lab2.png)

### Step 4

Add some more tests to increase the code coverage to 100% by updating the unit-tests in `Lab 2\Solidity\test\SimpleStorageTests.spec.js`.

Rerun the test with `npm run coverage` to verify that the coverage is 100%.

### Step 5

Now that the SmartContract has passed the linter, unit-tests and code-coverage, it's time to start coding in c# to execute some calls on this contract.

Run the command `npm run build` to generate C# Interfaces and Service classes from this SmartContract. You should see this output in the console:

``` x
Compiling contracts
SimpleStorageContract: generate ABI and ByteCode
SimpleStorageContract: generate C# helper classes
SimpleStorageContract: generate C# interfaces
SimpleStorageContract: generate C# implementations
```

### Step 6

Now open the `Lab 2\ConsoleApp` folder in Visual Code.

### Step 7

