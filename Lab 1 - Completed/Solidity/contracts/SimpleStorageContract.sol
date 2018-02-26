pragma solidity 0.4.19;


contract SimpleStorageContract {
    uint private storedNumber;
    string private storedString;

    function setNumber(uint value) public {
        storedNumber = value;
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
}