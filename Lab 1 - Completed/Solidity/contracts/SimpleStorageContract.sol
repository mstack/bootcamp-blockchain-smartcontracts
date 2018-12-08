pragma solidity 0.5.0;


contract SimpleStorageContract {
    uint private storedNumber;
    string private storedString;

    function setNumber(uint value) public {
        storedNumber = value;
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
}