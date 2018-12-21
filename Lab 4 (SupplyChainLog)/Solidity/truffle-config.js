/* eslint-disable no-console */
const path = require('path');

let rootPath = __dirname;
console.log(`truffle-config --> currentPath: '${rootPath}'`);

const coverageEnvPath = `${path.sep}coverageEnv`;
if (rootPath.endsWith(coverageEnvPath)) {
    rootPath = rootPath.substring(0, rootPath.lastIndexOf(coverageEnvPath));
    console.log(`truffle-config --> Updated rootPath to ${rootPath}`);
}

const solcPath = path.join(rootPath, 'node_modules', 'solc');
console.log(`truffle-config --> solcPath: '${solcPath}'`);

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
            version: solcPath,
        },
    },

    solc: {
        optimizer: {
            enabled: true,
            runs: 10,
        },
    },
};
