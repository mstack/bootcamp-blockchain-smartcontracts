pragma solidity 0.4.19;


contract SimpleStorageContract {
    int private _version;
    string private _description;
    uint private storedNumber;
    string private storedString;

    // Constructor
    function SimpleStorageContract(int version, string description) public {
        _version = version;
        _description = description;
    }

    function setNumber(uint value) public {
        if (value < 10) {
            storedNumber = 1;
        } else {
            storedNumber = 10;
        }
    }

    function getNumber() public constant returns (uint) {
        return storedNumber;
    }

    function setString(string value) public {
        storedString = value;
    }

    function getString() public constant returns (string) {
        return storedString;
    }

    function getVersion() public constant returns (int, string) {
        return (_version, _description);
    }
}