import React from 'react';

const LoginForm = ({handleSubmit, formdata, handleInputChange }) => {
    return (
        <form onSubmit={handleSubmit} className='loginForm'>
            <label>Username:</label>
            <input type='text' name='userName' value={formdata.userName} onChange={handleInputChange} required />
            <label>Password:</label>
            <input type='password' name='password' value={formdata.password} onChange={handleInputChange} required />
            <button type='submit' className='submitBtn'>Login</button>
        </form>
    )
};
export default LoginForm;