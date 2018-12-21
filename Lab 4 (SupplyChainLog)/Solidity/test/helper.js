const utf8 = require('utf8');

const ERROR_REVERT = 'VM Exception while processing transaction: revert';
const ERROR_INVALID_ADDRESS = 'invalid address';
const ERROR_FROMFIELD = 'The send transactions "from" field must be defined!';

function padToBytes32(n) {
    let result = n;
    while (result.length < 64) {
        result += '0';
    }
    return `0x${result}`;
}

function fromUtf8(value) {
    const str = utf8.encode(value);
    let hex = '';
    for (let i = 0; i < str.length; i += 1) {
        const code = str.charCodeAt(i);
        if (code === 0) {
            break;
        }
        const n = code.toString(16);
        hex += n.length < 2 ? `0${n}` : n;
    }

    return padToBytes32(hex);
}

// not tested yet...
// function toUtf8(hex) {
//     // Find termination
//     var str = "";
//     var i = 0, l = hex.length;
//     if (hex.substring(0, 2) === '0x') {
//         i = 2;
//     }
//     for (; i < l; i += 2) {
//         var code = parseInt(hex.substr(i, 2), 16);
//         if (code === 0) {
//             break;
//         }
//         str += String.fromCharCode(code);
//     }

//     return utf8.decode(str);
// }

module.exports = {
    fromUtf8,
    ERROR_REVERT,
    ERROR_INVALID_ADDRESS,
    ERROR_FROMFIELD,
};
