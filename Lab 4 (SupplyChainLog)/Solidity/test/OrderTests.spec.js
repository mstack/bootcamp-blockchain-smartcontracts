const helper = require('./helper');
const chai = require('chai'); // http://chaijs.com/
const chaiAsPromised = require('chai-as-promised'); // http://chaijs.com/plugins/chai-as-promised/
const truffleAssert = require('truffle-assertions');

chai.use(chaiAsPromised);
chai.should();
const assert = chai.assert;

const SupplyChainLogContract = artifacts.require('SupplyChainLogContract.sol');

contract('SupplyChainLogContract - Order', (accounts) => {
    const ownerAccount = accounts[0];
    const manufacturerAccount = accounts[1];

    let contract;

    beforeEach('create new contract before each test', async () => {
        contract = await SupplyChainLogContract.new({ from: ownerAccount });
    });

    it('addOrder', async () => {
        // Arrange
        const name = 'name 1';

        // Act
        const result = await contract.addOrder(name, { from: manufacturerAccount });

        // Assert
        truffleAssert.eventEmitted(result, 'AddOrderEvent', (eventArgs) => {
            assert.isNotEmpty(eventArgs.id);
            assert.equal(eventArgs.name, name);
            return true;
        });
    });

    it('getOrderCount', async () => {
        // Act
        await contract.addOrder('n 1', { from: manufacturerAccount });
        await contract.addOrder('n 2', { from: manufacturerAccount });
        await contract.addOrder('n 3', { from: manufacturerAccount });

        const orderCount = await contract.getOrderCount({ from: manufacturerAccount });

        // Assert
        assert.equal(3, orderCount);
    });

    it('addOrder with invalid name (empty) should revert', async () => {
        // Act and Assert
        await contract.addOrder('', { from: manufacturerAccount })
            .should.be.rejected;
    });
});
