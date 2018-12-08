pragma solidity 0.5.0;


contract SimpleStorageContract {
    int private version;
    string private description;
    uint private storedNumber;
    string private storedString;

    constructor(int _version, string memory _description) public {
        version = _version;
        description = _description;
    }

    function setNumber(uint value) public {
        if (value < 10) {
            storedNumber = 1;
        } else {
            storedNumber = 10;
        }
    }

    function getNumber() public view returns (uint) {
        return storedNumber;
    }

    function setString(string memory value) public {
        storedString = value;
    }

    function getString() public view returns (string memory) {
        return storedString;
    }

    function getVersion() public view returns (int, string memory) {
        return (version, description);
    }
}