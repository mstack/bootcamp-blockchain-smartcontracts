pragma solidity 0.5.0;


contract MultiplyContract {
    int private _multiplier;

    constructor (int multiplier) public {
        _multiplier = multiplier;
    }

    function multiply(int value) public view returns (int result) {
        return int(42); // FIXME
    }

    function multiplyTwoValues(int value1, int value2) public view returns (int result) {
        return int(42); // TODO
    }
}