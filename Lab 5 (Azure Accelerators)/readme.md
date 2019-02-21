# Vehicle and VehicleRegistry Contracts

## Folder structure

- Solidity; Folder for the SmartContracts and Libraries written in Solidity
- ConsoleAppGanache; A .NET Core Console app to deploy and test the contract to a local test Ganache instance

### Example JSON for Azure Accelerators - registry-generator

``` js
{
    "ItemName":"Vehicle",
    "Version":1,
    "IsRegistryAddressKnownAtCreation": true,
    "Ownership":
    {
        "OwnershipType": "None",
        "IsOwnerKnownAtCreation": false,
    },
    "Properties": [
      {
        "PropertyName":"LicenseNumber",
        "PropertyDataType":"string",
        "IndexType": "PrimaryIndex"
      },
      {
        "PropertyName":"VIN",
        "PropertyDataType":"string",
        "IndexType": "Index"
      },
      {
        "PropertyName":"Color",
        "PropertyDataType":"string"
      }
    ],
    "Media":
    {
        "AssociatedMedia": "None"
    }
}
```