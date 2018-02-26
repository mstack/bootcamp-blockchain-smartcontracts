const SimpleStorageContract = artifacts.require('SimpleStorageContract');

module.exports = (deployer) => {
    deployer.deploy(SimpleStorageContract);
};
