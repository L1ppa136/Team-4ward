import React, { useState } from 'react';
import axios from 'axios';
import './Login.css';

const Login = ({ onLogin, isLoggedIn }) => {
    const [formdata, setFormData] = useState({
        "userName": '',
        "password": ''
    });

    const [responseState, setResponseState] = useState('');

    const handleInputChange = (e) => {
        setFormData({ ...formdata, [e.target.name]: e.target.value });
    }

    const loginUser = async (formData) => {
        try {
            const response = await axios.post('http://localhost:5179/Authentication/Login', formData);
            return response.data;
        } catch (error) {
            throw error.response ? error.response.data : error;
        }
    };
    
    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const responseData = await loginUser(formdata);
    
            if (responseData && responseData.token) {
                // Add the token to the axios defaults for subsequent requests
                axios.defaults.headers.common['Authorization'] = `Bearer ${responseData.token}`;
                localStorage.setItem('accessToken', responseData.token);
                localStorage.setItem('userName', responseData.userName);
                localStorage.setItem('email', responseData.email);

                console.log(responseData);
                setResponseState(responseData);
                onLogin(); // Call the onLogin callback from the parent component
            } else {
                console.error('Invalid response:', responseData);
                setResponseState(responseData);
            }
        } catch (error) {
            console.error('Login failed:', error);
            setResponseState(error);
        }
    };
    

    return (
        <div className='login'>
            {responseState === '' ? (
                <div>
                    <form onSubmit={handleSubmit}>
                        <label>Username:</label>
                        <input type='text' name='userName' placeholder="enter username" value={formdata.userName} onChange={handleInputChange} required />
                        <label>Password:</label>
                        <input type='password' name='password' placeholder="enter password" value={formdata.password} onChange={handleInputChange} required />
                        <button type='submit'>Submit</button>
                    </form>
                </div>
            ) : (
                responseState.hasOwnProperty("Bad credentials") ? (
                    <div>
                        {responseState["Bad credentials"][0]}
                    </div>
                ) : <div>{responseState.userName} has been successfully logged in.</div>
            )}
        </div>
    );

};

export default Login;