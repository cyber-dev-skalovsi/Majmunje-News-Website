const environment = { apiRoot: 'http://localhost:4200' };

/**
 * Declares the JSON type with all properties for a single news object.
 * @typedef News
 * @type {object}
 * @property {string} id - ID of a news object.
 * @property {string} pictureURL - picture as a url
 * @property {string} name - A short title of the news object.
 * @property {string} description - A description text.
 */

/**
 * Gets all news object from the database.
 * @returns {Promise<News>} Returns all news object in the database.
 */

async function getNews() { //requires no token to get all the notes from the database
    const request = await fetch(`${environment.apiRoot}/api/news/`, {
        headers: {
            'Accept': 'application/json',
        },
        method: 'GET'
    });
    if (!request.ok) {
        throw new Error('Failed to fetch notes');
    }
    const data = await request.json();
    return data;
}
/**
 * Stores a new news object into the database.
 * @param {string} token JWT token with authentication info.
 * @param {News} news The news object to be sent to the server.
 * @returns {Promise<News>} Returns the created news object.
 */
async function addNews(token, news) {
    const request = await fetch(`${environment.apiRoot}/api/news/`, {
        headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
        },
        method: 'POST',
        body: JSON.stringify(news),
    });
    if (!request.ok) {
        console.error('Error in API call:', request.status);
        throw new Error('Failed to create news');
    }
    const data = await request.json();
    return data;
}
