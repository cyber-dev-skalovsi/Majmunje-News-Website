/**
 * Converts <html> tags to human readable text so the tags won't be executed by the browser anymore.
 * @param {Array<string>} strings Optional: Strings which should be processed.
 * @returns {Promise<News>} Returns the concatenated string.
 */
function asText(strings) {
    const [, ...args] = arguments;
    let txt = '';
    strings.forEach((str, idx) => {
        txt += str;
        if (idx < args.length) {
            txt += String(args[idx])
                .replace(/&/g, '&amp;') // codiert & zu &amp;
                .replace(/</g, '&lt;')  // codiert < zu &lt; 
                .replace(/>/g, '&gt;'); // codiert > zu &gt; 
        }
    });
    return txt;
}