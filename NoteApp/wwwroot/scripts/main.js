function checkjwttoken() { //edits the tabbar based on conditions (only displays differently, backend still checks the authentication)
    const token = localStorage.getItem('jwt-token');
    if (token) {
        try {
            JSON.parse(atob(token.split('.')[1]));
        }
        catch {
            detailViewHtml = asText`
            <a href="../pages/login.html" class="auth-tab">Log In</a>
            <a href="../pages/signup.html" class="auth-tab">Sign Up</a>
            `
            document.querySelector('#auth-option').innerHTML += detailViewHtml;
            localStorage.removeItem('jwt-token');
            return
        }
        const payload = JSON.parse(atob(token.split('.')[1]));
        if (payload.email == "admin@example.com") {
            detailViewHtml = asText`
            <a href="../pages/news.html" class="auth-tab-create">Create +</a>
            <a href="../pages/login.html" class="auth-tab">Log In</a>
            <a href="../pages/signup.html" class="auth-tab">Sign Up</a>
            <a class="auth-tab-logout">Logout</a>
            `
            document.querySelector('#auth-option').innerHTML += detailViewHtml;
        } else {
            detailViewHtml = asText`
            <a href="../pages/login.html" class="auth-tab">Log In</a>
            <a href="../pages/signup.html" class="auth-tab">Sign Up</a>
            <a class="auth-tab-logout">Logout</a>
            `
            document.querySelector('#auth-option').innerHTML += detailViewHtml;
        }
    }
    else {
        detailViewHtml = asText`
            <a href="pages/login.html" class="auth-tab">Log In</a>
            <a href="pages/signup.html" class="auth-tab">Sign Up</a>
            `
        document.querySelector('#auth-option').innerHTML += detailViewHtml;
    }
}
checkjwttoken()
