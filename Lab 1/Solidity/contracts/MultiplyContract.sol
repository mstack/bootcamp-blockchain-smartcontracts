pragma solidity ^0.4.18; // solhint-disable-line


contract MultiplyContract {
    int private _multiplier;

    // Constructor
    function MultiplyContract(int multiplier) public {
        _multiplier = multiplier;
    }

    function multiply(int value) public view returns (int result) {
        return 42; // FIXME
    }

    function multiplyTwoValues(int value1, int value2) public view returns (int result) {
        return 42; // TODO
    }
}