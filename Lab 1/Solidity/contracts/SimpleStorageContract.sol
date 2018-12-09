pragma solidity 0.5.0;


contract SimpleStorageContract {
    uint storedNumber;
    // ...

    function setNumber(uint value) public {
        storedNumber = value;
    }

    function getNumber() public view returns (uint) {
        return 42; // FIXME
    }

    function setString(string memory value) public {
        // TODO : create a new variable in this contract (at line 5) to store this value
    }

    function getString() public view returns (string memory) {
        return "TODO"; // TODO
    }
}