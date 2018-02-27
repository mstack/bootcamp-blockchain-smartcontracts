// const helper = require('./helper');
const chai = require('chai'); // http://chaijs.com/
const chaiAsPromised = require('chai-as-promised'); // http://chaijs.com/plugins/chai-as-promised/

chai.use(chaiAsPromised);
chai.should();
const assert = chai.assert;

const MultiplyContract = artifacts.require('MultiplyContract.sol');

contract('MultiplyContract', () => {
    let contract;

    beforeEach('wait for deployed contract before each test', async () => {
        contract = await MultiplyContract.deployed();
    });

    it('Multiply', async () => {
        // Assign
        const value = 100;

        // Act
        const result = await contract.multiply(value);

        // Assert
        assert.equal(result, 400);
    });

    it('Multiply two values', async () => {
        // Assign
        const value1 = 3;
        const value2 = 4;

        // Act
        const result = await contract.multiplyTwoValues(value1, value2);

        // Assert
        assert.equal(result, 12);
    });
});
