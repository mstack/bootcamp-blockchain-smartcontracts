module.exports = {
    port: 8555,
    norpc: true,
    skipFiles: ['contracts/Migrations.sol'],
    testrpcOptions: '-p 8555 -l 0xFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF -g 0x1',
};