import React from 'react';

const LoginForm = ({ handleSubmit, formdata, handleInputChange }) => {
    return (
        <form onSubmit={handleSubmit} className='loginForm'>
            <input type='text' placeholder='Username' name='userName' value={formdata.userName} onChange={handleInputChange} required />
            <input type='password' placeholder='Password' name='password' value={formdata.password} onChange={handleInputChange} required />
            <button type='submit' className='submitBtn'>Send</button>
        </form>
    )
};
export default LoginForm;