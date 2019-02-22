const helper = require('./helper');
const chai = require('chai'); // http://chaijs.com/
const chaiAsPromised = require('chai-as-promised'); // http://chaijs.com/plugins/chai-as-promised/
const truffleAssert = require('truffle-assertions');

chai.use(chaiAsPromised);
chai.should();
const assert = chai.assert;

const VehicleRegistry = artifacts.require('VehicleRegistry1.sol');

const State = {
    Created: 0,
    Open: 1,
    Closed: 2,
};

contract('VehicleRegistry', (accounts) => {
    const ownerAccount = accounts[0];

    let contract;

    beforeEach('create new contract before each test', async () => {
        contract = await VehicleRegistry.new('app-name', 'app-description', { from: ownerAccount });

        // Assert
        // truffleAssert.eventEmitted(result, 'WorkbenchContractCreated', (eventArgs) => {
        //     assert.equal(eventArgs.applicationName, 'VehicleRegistry1');
        //     assert.equal(eventArgs.workflowName, 'VehicleRegistry1');
        //     assert.isNotEmpty(eventArgs.originatingAddress);

        //     return true;
        // });
    });

    it('State', async () => {
        // Arrange

        // Act
        const state = await contract.State();

        // Assert
        assert.equal(State.Created, state);
    });

    it('OpenRegistry', async () => {
        // Arrange

        // Act
        const result = await contract.OpenRegistry();
        const state = await contract.State();

        // Assert
        truffleAssert.eventEmitted(result, 'WorkbenchContractUpdated', (eventArgs) => {
            assert.equal(eventArgs.applicationName, 'VehicleRegistry1');
            assert.equal(eventArgs.workflowName, 'VehicleRegistry1');
            assert.equal(eventArgs.action, 'OpenRegistry');
            assert.isNotEmpty(eventArgs.originatingAddress);

            return true;
        });

        assert.equal(State.Open, state);
    });

    it('OpenRegistry and CloseRegistry', async () => {
        // Arrange
        await contract.OpenRegistry();

        // Act
        const result = await contract.CloseRegistry();
        const state = await contract.State();

        // Assert
        truffleAssert.eventEmitted(result, 'WorkbenchContractUpdated', (eventArgs) => {
            assert.equal(eventArgs.applicationName, 'VehicleRegistry1');
            assert.equal(eventArgs.workflowName, 'VehicleRegistry1');
            assert.equal(eventArgs.action, 'CloseRegistry');
            assert.isNotEmpty(eventArgs.originatingAddress);

            return true;
        });

        assert.equal(State.Closed, state);
    });

    it('getNumberOfRegisteredVehicles - 0', async () => {
        // Arrange
        await contract.OpenRegistry();

        // Act
        const count = await contract.getNumberOfRegisteredVehicles();

        // Assert
        assert.equal(0, count);
    });
});
