function waitForElm(selector) {
    return new Promise(resolve => {
        if (document.querySelector(selector)) {
            return resolve(document.querySelector(selector));
        }

        const observer = new MutationObserver(mutations => {
            if (document.querySelector(selector)) {
                observer.disconnect();
                resolve(document.querySelector(selector));
            }
        });

        observer.observe(document.body, {
            childList: true,
            subtree: true
        });
    });
}

function moneymakerelem() {
    waitForElm('.moneymaker').then((elm) => { //waits for the element to be created, since it doesn't detect class "moneymaker" on startup and "sleep(5000)" would be to unstable 
        console.log("hello")
        let divElement = document.getElementsByClassName("moneymaker")[0];
        if (divElement) {
            divElement.addEventListener("click", function () {
                window.location.href = '../pages/login.html';
            });
        }
    });
}


let divElement2 = document.getElementsByClassName("auth-tab-logout")[0];
if (divElement2) {
    divElement2.addEventListener("click", function () {
        localStorage.removeItem('jwt-token');
        window.location.href = '../';
    })
}

let divElementgoback = document.getElementsByClassName("topleftbutton")[0];
if (divElementgoback) {
    divElementgoback.addEventListener("click", function () {
        window.location.href = '..';
    });
}
moneymakerelem()