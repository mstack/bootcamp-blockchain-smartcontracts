module.exports = {
    // See <http://truffleframework.com/docs/advanced/configuration> to customize your Truffle configuration!

    networks: {
        coverage: {
            host: 'localhost',
            network_id: '*',
            port: 8555,
            gas: 0xfffffffffff,
            gasPrice: 0x01,
        },
    },

    compilers: {
        solc: {
            version: '0.5.0',
        },
    },

    solc: {
        optimizer: {
            enabled: true,
            runs: 10,
        },
    },
};
