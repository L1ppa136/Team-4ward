import React, { useState } from 'react';
import axios from 'axios';
import './Login.css';
import LoginForm from '../Components/Forms/LoginForm';

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
                localStorage.setItem('userName', responseData.userName);
                localStorage.setItem('email', responseData.email);
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
                    <LoginForm
                        handleSubmit={handleSubmit}
                        formdata={formdata}
                        handleInputChange={handleInputChange}
                    />
                </div>
            ) : (
                responseState.hasOwnProperty("Bad credentials") ? (
                    <div>
                        {responseState["Bad credentials"][0]}
                        {/* Render the form again to allow the user to try again */}
                        <LoginForm
                            handleSubmit={handleSubmit}
                            formdata={formdata}
                            handleInputChange={handleInputChange}
                        />
                    </div>
                ) : (
                    <div>{responseState.userName} has been successfully logged in.</div>
                )
            )}
        </div>
    );
};

export default Login;
