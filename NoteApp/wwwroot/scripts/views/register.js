async function submitRegisterForm(e) {
    e.preventDefault();

    const email = document.querySelector('#register-email').value;
    const password = document.querySelector('#register-password').value;
    const passwordConfirm = document.querySelector('#register-password-confirm').value;
    
    if (password === passwordConfirm) {
        try {
            const loginInfo = await register(email, password);
            localStorage.setItem('jwt-token', loginInfo.jwt);
            try {
                const token = localStorage.getItem('jwt-token');
                JSON.parse(atob(token.split('.')[1]));
                window.location.href = '../';
            } catch {
                document.querySelector('#register-error').innerText = "Email already in use!";
            }
        }
        catch (err) {
            document.querySelector('#register-error').innerText = err.message;
        }
    }
    else {
        document.querySelector('#register-error').innerText = "Password confirmation didn't match.";
    }
}

document.querySelector('form').addEventListener('submit', submitRegisterForm);