module.exports = {
    port: 8555,
    norpc: true,
    skipFiles: ['contracts/Migrations.sol'],

    // https://github.com/sc-forks/solidity-coverage/blob/master/docs/faq.md#running-truffle-as-a-local-dependency
    // compileCommand: '..\\node_modules\\.bin\\truffle compile',
    // testCommand: '..\\node_modules\\.bin\\truffle test --network coverage'
};