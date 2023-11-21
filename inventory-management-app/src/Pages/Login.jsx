import React, { useState } from 'react';
import axios from 'axios';

const Login = () => {
    const [formdata, setFormData] = useState({
        "userName": '',
        "password": ''
    });

    const handleInputChange = (e) => {
        setFormData({ ...formdata, [e.target.name]: e.target.value });
    }

    const handleSubmit = async (e) => {
        e.preventDefault();

        console.log(formdata);

        try {
            const response = await axios.post('http://localhost:5179/Authentication/Login', formdata);

            // Check if 'response' is defined and has a 'data' property
            if (response && response.data) {
                console.log(response.data);
            } else {
                console.error('Invalid response:', response);
            }
        } catch (error) {
            // Check if 'error.response' is defined and has a 'data' property
            if (error.response && error.response.data) {
                console.error('Login failed:', error.response.data);
            } else {
                console.error('Unexpected error:', error);
            }
        }
    };


    return (
        <div>
            <h2>Login</h2>
            <form onSubmit={handleSubmit}>
                <label>Username:</label>
                <input type='text' name='username' value={formdata.username} onChange={handleInputChange} required />
                <label>Password:</label>
                <input type='password' name='password' value={formdata.password} onChange={handleInputChange} required />
                <button type='submit'>Submit</button>
            </form>
        </div>
    );
};

export default Login;