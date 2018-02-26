# Lab 1

## Info

This lab will help you:

- learn the basics about the Solidity SmartContract programming language
- implement new functionality in a SmartContract
- update or add unit-test to test this SmartContract

## Folder structure

- Solidity: folder for the SmartContracts and unit-tests written in Solidity

## Details

This lab contains two simple SmartContracts:

- `SimpleStorageContract` this contract can store and retrieve number and sring values
- `CalculatorContract` this contract is initialized with a default multiply factor and can do some basic multiplications
- Both contracts are unit tested.

## Steps

Execute the following steps:

### Step 1

Go to the terminal in VS Code and type:

- `cd Lab 1`
- `cd Solidity`
- `npm i` (to install all required node packages)

### Step 2

Go to the terminal in VS Code and type:

- `npm test` (this command will use [Truffle](https://github.com/trufflesuite/truffle) run the test, and you will see some test fail)

``` x
1) Contract: SimpleStorageContract Set and Get Number:
     AssertionError: expected { Object (s, e, ...) } to equal 100
      at Context.it (test\SimpleStorageTests.spec.js:30:16)
      at <anonymous>
      at process._tickCallback (internal/process/next_tick.js:188:7)
```

Open the SimpleStorageContract.sol file at `Lab 1\Solidity\Contracts` and fix the **FIXME** issue in `function getNumber()`.

### Step 3

Open the SimpleStorageContract.sol file at `Lab 1\Solidity\Contracts` and implement the **TODO** statements so that a string value is also correctly stored in the contract.

### Step 4

Now it's time to also write unit-tests for the string value logic, so open the unit-test `Lab 1\Solidity\test\SimpleStorage.spec.js` and uncomment the string value tests.

Rerun the test with `npm test` to verify that the code works fine.

### Step 5

Check the Solidity contract code with a linter using the `npm run lint` command. View the warnings/errors and try to fix these.