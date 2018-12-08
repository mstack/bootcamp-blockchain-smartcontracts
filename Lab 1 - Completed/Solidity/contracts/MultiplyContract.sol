pragma solidity ^0.5.0; // solhint-disable-line


contract MultiplyContract {
    int private _multiplier;

    constructor(int multiplier) public {
        _multiplier = multiplier;
    }

    function multiply(int value) public view returns (int result) {
        return _multiplier * value;
    }

    function multiplyTwoValues(int value1, int value2) public pure returns (int result) {
        return value1 * value2;
    }
}