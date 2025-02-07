// let notes = [ ];
// let note = undefined; // selected note
async function renderIndex() {
    document.querySelector('#listidobject').innerHTML = '';
    try {
        let detailViewHtml = ''
        // load data async
        const token = localStorage.getItem('jwt-token');
        let news = await getNews();
        for (let i = 0; i < news.length; i++) 
        {
            debugger
            if (i > 2 && !token) {
                detailViewHtml = asText`
                <div class="moneymaker animated animatedFadeInUp fadeInUp" id="moneymaker">
                    <p>Please <a class="blue">Login</a> to view more!</p>
                </div>
                `
                document.querySelector('#listidobject').innerHTML += detailViewHtml;
                return
            }
                detailViewHtml = asText`
                <div class="news-item animated animatedFadeInUp fadeInUp">
                    <img src='${news[i].pictureUrl}' alt="News 1">
                    <div class="news-content">
                        <h3>${news[i].name}</h3>
                        <p>${news[i].description}</p>
                    </div>
                </div>
                `
                document.querySelector('#listidobject').innerHTML += detailViewHtml;
        }
    }
    catch (err) {
        console.warn(err);
    }
}
/*
// link to 'create note' feature
document.querySelector('#create-button').addEventListener('click',  () => {
    window.location.href = './pages/create.html';
});*/
renderIndex();            
