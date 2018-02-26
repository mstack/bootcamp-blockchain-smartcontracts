pragma solidity 0.4.19;


contract SimpleStorageContract {
    uint storedNumber;
    // ...

    function setNumber(uint value) public {
        storedNumber = value;
    }

    function getNumber() public constant returns (uint) {
        return 42; // FIXME
    }

    function setString(string value) public {
        // TODO : create a new variable in this contract (at line 5) to store this value
    }

    function getString() public constant returns (string) {
        return "TODO"; // TODO
    }
}