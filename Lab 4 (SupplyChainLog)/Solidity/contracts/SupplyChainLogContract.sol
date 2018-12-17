pragma solidity ^0.5.1; // solhint-disable-line


import "./OrderContract.sol";


contract SupplyChainLogContract is OrderContract {

    // Constructor
    constructor () public {
        owner = msg.sender;
    }
}