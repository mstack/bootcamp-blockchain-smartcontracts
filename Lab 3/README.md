# Lab 3

## Info

This lab (continues on Lab 2) and will teach you how to:

- deploy the SmartContract using a console application to Ethereum Consortium Blockchain on Azure
- execute functions on the SmartContract which is deployed to Ethereum Consortium Blockchain on Azure

If you have some generic or detailed questions, please feel free to ask these at mStack colleagues.

## Folder structure

- `ConsoleApp`: folder for the ConsoleApp (C#).

## Details

This lab contains one simple SmartContract:

- `SimpleStorageContract` this contract can store and retrieve number and string values

## Steps

Execute the following steps in order to follow this Lab.

### Step 1

Now open a new command terminal in Visual Code, and type:

- `cd Lab 3`
- `cd ConsoleApp`
- `dotnet build` (if all is fine, a restore should be done and the build should complete with 0 errors.)

### Step 2

Open the file `Lab 3\ConsoleApp\Program.cs` in Visual Code.

- `TODO 1` : make sure to fill in your assigned address. Note that the password is always `test`.
- `TODO 2` : replace `http://???.westeurope.cloudapp.azure.com:8545/` by the correct URL provided by mStack.
- Run the ConsoleApp `dotnet run` and the output should look like this:

``` x
Blockchain - Ethereum - ConsoleApp
--------------------------------------------------------------------------------
Deploying contract (can take some time)
Deploying contract done, address = 0x97da25343187cbc1d31d98b1fd244cec5fc77f86
setNumberResult = 'True'.
The stored number value is '10'.
setStringResult = 'True'.
The stored string value is 'mstack.nl test'.
```

### Step 3

You can experiment by calling some other functions or providing other values.

### Step 4

Each time you run the full program, a new contract is deployed, if you want to keep using the same contract, just remember the contract address and use that one. (See also step 11 from Lab 2)

### Step 5

Done