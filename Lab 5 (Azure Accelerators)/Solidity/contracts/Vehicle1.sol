pragma solidity ^0.5.2;


import "./VehicleRegistry1.sol";


contract Vehicle1 is WorkbenchBase("VehicleRegistry1", "Vehicle1") {

    // Registry
    VehicleRegistry1 MyVehicleRegistry;
    address public RegistryAddress;

    //Common Properties
    //Set of States
    enum StateType { Active, Retired }
    StateType public State;
    string public RetirementRecordedDateTime;


    //Vehicle specific property declarations
    //-------------------------------------------
    string public LicenseNumber;
    string public VIN;
    string public Color;




    
    //Constructor Function for Vehicle
    //-------------------------------------
    
    constructor (address _RegistryAddress, string memory _LicenseNumber, string memory _VIN, string memory _Color) public {
    
        LicenseNumber = _LicenseNumber;
         
        VIN = _VIN;
         
        Color = _Color;
         
        RegistryAddress = _RegistryAddress;
    
         MyVehicleRegistry = VehicleRegistry1(RegistryAddress);
         
        MyVehicleRegistry.RegisterVehicle32(
            stringToBytes32(LicenseNumber),
            stringToBytes32(VIN),
            address(this));
    
        ContractCreated();
    }







    
    //Retire Function for Vehicle
    function  Retire(string memory retirementRecordedDateTime) public {
        RetirementRecordedDateTime = retirementRecordedDateTime;
        State = StateType.Retired;
        ContractUpdated("Retire");
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