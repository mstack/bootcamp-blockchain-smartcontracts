const path = require('path');

let rootPath = __dirname;
console.log(`.solcover --> currentPath: '${rootPath}'`);

if (rootPath.endsWith('coverageEnv')) {
    rootPath = stripPath(rootPath, 'coverageEnv');
    console.log(`.solcover --> Updated rootPath to ${rootPath}`);
}

const trufflePath = path.join(rootPath, 'node_modules', '.bin', 'truffle');
console.log(`.solcover --> trufflePath: '${trufflePath}'`);

module.exports = {
    port: 8555,
    norpc: true,
    skipFiles: ['contracts/Migrations.sol'],

    // See https://truffleframework.com/docs/truffle/reference/truffle-commands
    // compileCommand: `${trufflePath} compile --all --network coverage`,
    // testCommand: `${trufflePath} test --compile-all --network coverage`,
};