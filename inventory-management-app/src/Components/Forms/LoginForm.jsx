import React from 'react';

const LoginForm = ({handleSubmit, formdata, handleInputChange }) => {
    return (
        <form onSubmit={handleSubmit} className='loginForm'>
            <label>Username:</label>
            <input type='text' name='userName' placeholder="enter username" value={formdata.userName} onChange={handleInputChange} required />
            <label>Password:</label>
            <input type='password' name='password' placeholder="enter password" value={formdata.password} onChange={handleInputChange} required />
            <button type='submit'>Submit</button>
        </form>
    )
};
export default LoginForm;