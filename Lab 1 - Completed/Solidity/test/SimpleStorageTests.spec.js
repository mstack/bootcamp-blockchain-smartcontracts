const helper = require('./helper');
const chai = require('chai'); // http://chaijs.com/
const chaiAsPromised = require('chai-as-promised'); // http://chaijs.com/plugins/chai-as-promised/

chai.use(chaiAsPromised);
chai.should();
const assert = chai.assert;

const SimpleStorageContract = artifacts.require('SimpleStorageContract.sol');

contract('SimpleStorageContract', (accounts) => {
    const owner = accounts[0];

    let contract;

    beforeEach('create new contract before each test', async () => {
        contract = await SimpleStorageContract.new({ from: owner });
    });

    it('Set and Get Number', async () => {
        // Assign
        const value = 100;

        // Act
        await contract.setNumber(value);

        const result = await contract.getNumber();

        // Assert
        assert.equal(result, 100);
    });

    it('Set and Get String', async () => {
        // Assign
        const value = 'mstack.nl';

        // Act
        await contract.setString(value);

        const result = await contract.getString();

        // Assert
        assert.equal(result, 'mstack.nl');
    });
});
