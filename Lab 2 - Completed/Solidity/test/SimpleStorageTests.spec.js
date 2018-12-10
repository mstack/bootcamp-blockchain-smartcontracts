// const helper = require('./helper');
const chai = require('chai'); // http://chaijs.com/
const chaiAsPromised = require('chai-as-promised'); // http://chaijs.com/plugins/chai-as-promised/

chai.use(chaiAsPromised);
chai.should();
const assert = chai.assert;

const SimpleStorageContract = artifacts.require('SimpleStorageContract.sol');

contract('SimpleStorageContract', () => {
    let contract;

    beforeEach('create new contract before each test', async () => {
        contract = await SimpleStorageContract.deployed();
    });

    it('Set and Get Number (test 1)', async () => {
        // Assign
        const value = 0;

        // Act
        await contract.setNumber(value);

        const result = await contract.getNumber();

        // Assert
        assert.equal(result, 1);
    });

    it('Set and Get Number (test 2)', async () => {
        // Assign
        const value = 42;

        // Act
        await contract.setNumber(value);

        const result = await contract.getNumber();

        // Assert
        assert.equal(result, 42);
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

    it('getVersion', async () => {
        // Act
        const result = await contract.getVersion();

        // Assert
        assert.equal(result.version, 1);
        assert.equal(result.description, 'version by mstack.nl');
    });
});
