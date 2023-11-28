import React, { useState } from 'react';
import axios from 'axios';
import './Login.css';

const Login = ({ onLogin, isLoggedIn }) => {
    const initialFormData = {
        "userName": '',
        "password": ''
    };

    const [formdata, setFormData] = useState(initialFormData);
    const [responseState, setResponseState] = useState('');

    const handleInputChange = (e) => {
        setFormData({ ...formdata, [e.target.name]: e.target.value });
    };

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
                axios.defaults.headers.common['Authorization'] = `Bearer ${responseData.token}`;
                //felesleges ha az Axiosba menti
                localStorage.setItem('accessToken', responseData.token);
                localStorage.setItem('userName', responseData.userName);
                localStorage.setItem('email', responseData.email);
                //SetTimeOut a backend-es idővel megegyezzővel hogy kitöröljük az axios headerben lévő tokent. Testelésre vár
                setResponseState(responseData);
                onLogin(); // Call the onLogin callback from the parent component
            } else {
                console.error('Invalid response:', responseData);
                setResponseState(responseData);
            }
        } catch (error) {
            console.error('Login failed:', error);
            setResponseState(error);
            // Reset the form state to allow the user to try again
            setFormData(initialFormData);
        }
    };

    return (
        <div className='login'>
            {responseState === '' ? (
                <div>
                    <form onSubmit={handleSubmit} className='loginForm'>
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
                        {/* Render the form again to allow the user to try again */}
                        <form onSubmit={handleSubmit} className='loginForm'>
                            <label>Username:</label>
                            <input type='text' name='userName' value={formdata.userName} onChange={handleInputChange} required />
                            <label>Password:</label>
                            <input type='password' name='password' value={formdata.password} onChange={handleInputChange} required />
                            <button type='submit'>Submit</button>
                        </form>
                    </div>
                ) : (
                    <div>{responseState.userName} has been successfully logged in.</div>
                )
            )}
        </div>
    );
};

export default Login;
