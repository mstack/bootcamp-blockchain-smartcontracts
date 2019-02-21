pragma solidity ^0.5.2;


import "./Vehicle1.sol";


contract WorkbenchBase {
    event WorkbenchContractCreated(string applicationName, string workflowName, address originatingAddress);
    event WorkbenchContractUpdated(string applicationName, string workflowName, string action, address originatingAddress);

    string internal ApplicationName;
    string internal WorkflowName;

    constructor(string memory applicationName, string memory workflowName) internal {
        ApplicationName = applicationName;
        WorkflowName = workflowName;
    }

    function ContractCreated() internal {
        emit WorkbenchContractCreated(ApplicationName, WorkflowName, msg.sender);
    }

    function ContractUpdated(string memory action) internal {
        emit WorkbenchContractUpdated(ApplicationName, WorkflowName, action, msg.sender);
    }
}


contract VehicleRegistry1 is WorkbenchBase("VehicleRegistry1", "VehicleRegistry1") {
    enum StateType { Created, Open, Closed }
    StateType public State;

    VehicleStruct[] public Vehicles;

    string public Name;
    string public Description;

    struct VehicleStruct {
        string LicenseNumber;
        string VIN;
        address ContractAddress;
        uint Index;
    }
    
    string[] private VehicleLicenseNumberIndex;
    string[] private VehicleVINIndex;
    address[] private VehicleContractAddressIndex;
    
    mapping(string => VehicleStruct) private VehicleLicenseNumberLookup;
    mapping(string => VehicleStruct) private VehicleVINLookup;
    mapping(address => VehicleStruct) private VehicleContractAddressLookup;
    
    event LogNewVehicle (string _LicenseNumber, string _VIN, address _ContractAddress, uint index);
    event LogUpdateVehicle (string _LicenseNumber, string _VIN, address _ContractAddress, uint index);
    
    constructor(string memory _Name, string memory _Description) public {
        Name = _Name;
        Description = _Description;
        State = StateType.Created;
        ContractCreated();
    }

    function OpenRegistry() public
    {
        State = StateType.Open;        
        ContractUpdated("OpenRegistry");
    }

    function CloseRegistry() public
    {
        State = StateType.Closed;
        ContractUpdated("CloseRegistry");
    }

    //Lookup to see if a contract address for a Vehicle contract is already registered
    function isRegisteredVehicleLicenseNumber(string memory VehicleLicenseNumber)
    public view
    returns(bool isRegistered)
    {
        if (VehicleLicenseNumberIndex.length == 0) return false;
        string memory var1 = VehicleLicenseNumberIndex[VehicleLicenseNumberLookup[VehicleLicenseNumber].Index];
        string memory var2 = VehicleLicenseNumber;
        return (keccak256(abi.encodePacked(var1)) == keccak256(abi.encodePacked(var2)));
    }
    
    //Lookup to see if a contract address for a Vehicle contract is already registered
    function isRegisteredVehicleVIN(string memory VehicleVIN)
    public view
    returns(bool isRegistered)
    {
        if (VehicleVINIndex.length == 0) return false;
        string memory var1 = VehicleVINIndex[VehicleVINLookup[VehicleVIN].Index];
        string memory var2 = VehicleVIN;
        return (keccak256(abi.encodePacked(var1)) == keccak256(abi.encodePacked(var2)));
    }
    
    //Lookup to see if a contract address for a Vehicle contract is already registered
    function isRegisteredVehicleContractAddress(address  VehicleContractAddress)
    public view
    returns(bool isRegistered)
    {
        if (VehicleContractAddressIndex.length == 0) return false;
        address var1 = VehicleContractAddressIndex[VehicleContractAddressLookup[VehicleContractAddress].Index];
        address var2 = VehicleContractAddress;
        return (keccak256(abi.encodePacked(var1)) == keccak256(abi.encodePacked(var2)));
    }
    
    function RegisterVehicle(string memory _LicenseNumber, string memory _VIN, address _ContractAddress) 
    public
    {
        if (isRegisteredVehicleContractAddress(_ContractAddress)) revert();
    
        //Add lookup by LicenseNumber
        VehicleLicenseNumberLookup[_LicenseNumber].LicenseNumber = _LicenseNumber;
        VehicleLicenseNumberLookup[_LicenseNumber].VIN = _VIN;
        VehicleLicenseNumberLookup[_LicenseNumber].ContractAddress = _ContractAddress;
        VehicleLicenseNumberLookup[_LicenseNumber].Index = VehicleLicenseNumberIndex.push(_LicenseNumber) - 1;
    
        //Add lookup by VIN
        VehicleVINLookup[_VIN].LicenseNumber = _LicenseNumber;
        VehicleVINLookup[_VIN].VIN = _VIN;
        VehicleVINLookup[_VIN].ContractAddress = _ContractAddress;
        VehicleVINLookup[_VIN].Index = VehicleVINIndex.push(_VIN) - 1;
    
        //Add lookup by ContractAddress
        VehicleContractAddressLookup[_ContractAddress].LicenseNumber = _LicenseNumber;
        VehicleContractAddressLookup[_ContractAddress].VIN = _VIN;
        VehicleContractAddressLookup[_ContractAddress].ContractAddress = _ContractAddress;
        VehicleContractAddressLookup[_ContractAddress].Index = VehicleContractAddressIndex.push(_ContractAddress) - 1;
        emit LogNewVehicle(
                _LicenseNumber, _VIN, _ContractAddress,
                VehicleContractAddressLookup[_ContractAddress].Index
        );
        ContractUpdated("RegisterVehicle");
    }
    
    function RegisterVehicle32(bytes32 _LicenseNumber, bytes32 _VIN, address _ContractAddress) 
    public
    {
        return RegisterVehicle(
            bytes32ToString(_LicenseNumber),
            bytes32ToString(_VIN),
            _ContractAddress);
    }
    
    function getLicenseNumberByAddress(address VehicleContractAddress)
    public view
    returns(string memory VehicleLicenseNumber)
    {
        if (!isRegisteredVehicleContractAddress(VehicleContractAddress)) revert();
        return (VehicleContractAddressLookup[VehicleContractAddress].LicenseNumber);
    }
    
    function getAddressByLicenseNumber(string memory VehicleLicenseNumber)
    public view
    returns(address VehicleContractAddress)
    {
        string memory idx = (VehicleLicenseNumber);
        if (!isRegisteredVehicleLicenseNumber(idx)) revert();
        return VehicleLicenseNumberLookup[idx].ContractAddress;
    }
    
    function getLicenseNumberByAddress32(address VehicleContractAddress)
    public view
    returns(bytes32 VehicleLicenseNumber)
    {
        if (!isRegisteredVehicleContractAddress(VehicleContractAddress)) revert();
        return stringToBytes32(VehicleContractAddressLookup[VehicleContractAddress].LicenseNumber);
    }
    
    function getAddressByLicenseNumber32(bytes32 VehicleLicenseNumber)
    public view
    returns(address VehicleContractAddress)
    {
        string memory idx = bytes32ToString(VehicleLicenseNumber);
        if (!isRegisteredVehicleLicenseNumber(idx)) revert();
        return VehicleLicenseNumberLookup[idx].ContractAddress;
    }
    
    function getVINByAddress(address VehicleContractAddress)
    public view
    returns(string memory VehicleVIN)
    {
        if (!isRegisteredVehicleContractAddress(VehicleContractAddress)) revert();
        return (VehicleContractAddressLookup[VehicleContractAddress].VIN);
    }
    
    function getAddressByVIN(string memory VehicleVIN)
    public view
    returns(address VehicleContractAddress)
    {
        string memory idx = (VehicleVIN);
        if (!isRegisteredVehicleVIN(idx)) revert();
        return VehicleVINLookup[idx].ContractAddress;
    }
    
    function getVINByAddress32(address VehicleContractAddress)
    public view
    returns(bytes32 VehicleVIN)
    {
        if (!isRegisteredVehicleContractAddress(VehicleContractAddress)) revert();
        return stringToBytes32(VehicleContractAddressLookup[VehicleContractAddress].VIN);
    }
    
    function getAddressByVIN32(bytes32 VehicleVIN)
    public view
    returns(address VehicleContractAddress)
    {
        string memory idx = bytes32ToString(VehicleVIN);
        if (!isRegisteredVehicleVIN(idx)) revert();
        return VehicleVINLookup[idx].ContractAddress;
    }
    
    function getNumberOfRegisteredVehicles() 
    public
    view
    returns(uint count)
    {
        return VehicleContractAddressIndex.length;
    }

    function getVehicleAtIndex(uint index)
    public
    view
    returns(address VehicleContractAddress)
    {
        return VehicleContractAddressIndex[index];
    }

    /******************* Utils functions *******************************************/   
    function stringToBytes32(string memory source) public pure returns (bytes32 result) {
        bytes memory tempEmptyStringTest = bytes(source);
        if (tempEmptyStringTest.length == 0) {
            return 0x0;
        }

        assembly {
            result := mload(add(source, 32))
        }
    }
    
    function bytes32ToString (bytes32 x) public pure returns (string memory result) {
        bytes memory bytesString = new bytes(32);
        uint charCount = 0;
        for (uint j  = 0; j < 32; j++) {
            byte char = byte(bytes32(uint(x) * 2 ** (8 * j)));
            if (char != 0) {
                bytesString[charCount] = char;
                charCount++;
            }
        }
        bytes memory bytesStringTrimmed = new bytes(charCount);
        for (uint j = 0; j < charCount; j++) {
            bytesStringTrimmed[j] = bytesString[j];
        }
        return string(bytesStringTrimmed);
    }

    function stringToAddress(string memory _a) public pure returns (address _parsedAddress) {
        bytes memory tmp = bytes(_a);
        uint160 iaddr = 0;
        uint160 b1;
        uint160 b2;
        for (uint i = 2; i < 2 + 2 * 20; i += 2) {
            iaddr *= 256;
            b1 = uint160(uint8(tmp[i]));
            b2 = uint160(uint8(tmp[i + 1]));
        
            if ((b1 >= 97) && (b1 <= 102)) {
                b1 -= 87;
            } else if ((b1 >= 65) && (b1 <= 70)) {
                b1 -= 55;
            } else if ((b1 >= 48) && (b1 <= 57)) {
                b1 -= 48;
            }

            if ((b2 >= 97) && (b2 <= 102)) {
                b2 -= 87;
            } else if ((b2 >= 65) && (b2 <= 70)) {
                b2 -= 55;
            } else if ((b2 >= 48) && (b2 <= 57)) {
                b2 -= 48;
            }
            iaddr += (b1 * 16 + b2);
        }
        return address(iaddr);
    }

}