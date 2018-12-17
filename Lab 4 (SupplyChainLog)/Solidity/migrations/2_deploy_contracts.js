const SupplyChainLogContract = artifacts.require('SupplyChainLogContract');

module.exports = (deployer) => {
    deployer.deploy(SupplyChainLogContract);
};
