import React from 'react';

const RegisterForm = ({handleSubmit, formdata, handleInputChange}) => {
    return(
        <form className='loginForm' onSubmit={handleSubmit}>
                <label>Email:</label>
                <input type='email' name='email' placeholder='Email' value={formdata.email} onChange={handleInputChange} required />
                <label>Username:</label>
                <input type='text' name='username' placeholder='Username' value={formdata.username} onChange={handleInputChange} required />
                <label>Password:</label>
                <input type='password' name='password' placeholder='Password' value={formdata.password} onChange={handleInputChange} required />
                <button type='submit' className='submitBtn'>Submit</button>
              </form>
    )
}
export default RegisterForm;