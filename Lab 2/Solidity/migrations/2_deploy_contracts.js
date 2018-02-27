const SimpleStorageContract = artifacts.require('SimpleStorageContract');

module.exports = (deployer) => {
    deployer.deploy(SimpleStorageContract, 1, 'version by mstack.nl');
};
