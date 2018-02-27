const SimpleStorageContract = artifacts.require('SimpleStorageContract');
const MultiplyContract = artifacts.require('MultiplyContract');

module.exports = (deployer) => {
    deployer.deploy(SimpleStorageContract);

    const multiplier = 4;
    deployer.deploy(MultiplyContract, multiplier);
};
