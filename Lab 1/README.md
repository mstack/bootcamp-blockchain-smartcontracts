# Lab 1

## Info

This lab will help you:

- learn the basics about the Solidity SmartContract programming language
- implement new functionality in a SmartContract
- update or add unit-test to test this SmartContract

## What is Solidity

Solidity is a contract-oriented programming language for writing smart contracts. It is used for implementing smart contracts on various blockchain platforms. It was developed by Gavin Wood, Christian Reitwiessner, Alex Beregszaszi, Yoichi Hirai and several former Ethereum core contributors to enable writing smart contracts on blockchain platforms such as Ethereum.

## Learn basic of Solidity

There are multiple sites and video's to learn Solidity. Here are some videos as a refernce for understanding Solidity:

- [`Solidity - Smart contracts 1 - Installation/first steps`](https://www.youtube.com/watch?v=9_coM_g7Dbg) - Time: 12:35
- [`Solidity - Smart contracts 2 – Inheritance`](https://www.youtube.com/watch?v=HAvDbKttijY) - Time: 10:41
- [`Solidity - Smart contracts 3 – Modifiers`](https://www.youtube.com/watch?v=FGnv8Vfu9bY) - Time: 8:53
- [`Solidity - Smart contracts 4 – Variables`](https://www.youtube.com/watch?v=n9yzr5ved_k) - Time: 18:04
- [`Solidity - Smart contracts 5 - Contracts interactions`](https://www.youtube.com/watch?v=m9Zb49RNGis) - Time: 20:05

If you have some generic or detailed questions, please feel free to ask these at mStack colleagues.

## Folder structure

- `Solidity`: folder for the SmartContracts written in Solidity and unit-tests written in javascript.

## Details

This lab contains two simple SmartContracts:

- `SimpleStorageContract` this contract can store and retrieve number and string values
- `CalculatorContract` this contract is initialized with a default multiply factor and can do some basic multiplications
- Both contracts are unit tested

## Steps

Execute the following steps in order to follow this Lab.

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

  2) Contract: MultiplyContract Multiply:
     AssertionError: expected { Object (s, e, ...) } to equal 400
      at Context.it (test\MultiplyTests.spec.js:26:16)
      at <anonymous>
      at process._tickCallback (internal/process/next_tick.js:188:7)
```

Open the **SimpleStorageContract.sol** file at `Lab 1\Solidity\Contracts` and fix the **FIXME** issue in `function getNumber()`.

Open the **MultiplyContract.sol** file at `Lab 1\Solidity\Contracts` and fix the **FIXME** issues in this contract.

### Step 3

Open the **SimpleStorageContract.sol** file at `Lab 1\Solidity\Contracts` and implement the **TODO** statements so that a string value is also correctly stored in the contract.

Open the **MultiplyContract.sol** file at `Lab 1\Solidity\Contracts` and implement the **TODO** statements so that two values are correctly multiplied.

### Step 4

Now it's time to also write unit-tests for the string value logic, so open the unit-test `Lab 1\Solidity\test\SimpleStorageTests.spec.js` and uncomment and fix the string value tests.

Rerun the test with `npm test` to verify that the code works fine.

### Step 5

Add some more tests to test the multiply from two numbers. Open the unit-test `Lab 1\Solidity\test\MultiplyTests.spec.js` and uncomment the 'Multiply two values' test.

Rerun the test with `npm test` to verify that the code works fine.

### Step 6

Check the Solidity contracts code with a linter using the `npm run lint` command. View the warnings/errors and try to fix these.

### Step 7

All done, if you have questions please ask and take also a look at the `Lab 1 - Completed` folder to see the completed