async function saveEditView() {
    debugger
    const name = document.querySelector('#edit-titel').innerText;
    const description = document.querySelector('#edit-description').innerText;
    const pictureurl = document.querySelector('#picture-url').innerText;

    try {
        const token = localStorage.getItem('jwt-token');
        const news = {
            name: name,
            description: description,
            pictureurl: pictureurl
            /*dueDate: (dueDate !== null) ? dueDate.toISOString() : null*/
        };

        await addNews(token, news);
        window.location.href = '../';
    }
    catch (err) {
        console.warn(err);
    }
}

document.querySelector(".submit-moneymaker").addEventListener("click", (e) => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
        window.location.href = '../pages/login.html';
        return
    }
    e.preventDefault();
    saveEditView();
});
