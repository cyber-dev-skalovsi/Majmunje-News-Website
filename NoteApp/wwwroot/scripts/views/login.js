async function submitLoginForm(e) {
    e.preventDefault();

    const email = document.querySelector('#login-email').value;
    const password = document.querySelector('#login-password').value;

    try {
        const loginInfo = await login(email, password);
        if (loginInfo.jwt !== undefined) {
            localStorage.setItem('jwt-token', loginInfo.jwt);
            /*document.querySelector('#login-error').innerText = 'Login Succsesful.';*/
            window.location.href = '../';
        }
        else {
            document.querySelector('#login-error').innerText = 'Wrong Password!';
        }
    }
    catch (err) {
        document.querySelector('#login-error').innerText = err.message;
    }
}

document.querySelector('form').addEventListener('submit', submitLoginForm);
