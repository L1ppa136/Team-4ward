import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import LoginForm from '../Components/Forms/LoginForm';
import { useAuth } from '../Components/AuthContext';

const Login = () => {
    const initialFormData = {
        userName: '',
        password: '',
    };

    const { login } = useAuth();
    const navigate = useNavigate();
    const [formdata, setFormData] = useState(initialFormData);
    const [error, setError] = useState(null);

    const handleInputChange = (e) => {
        setFormData({ ...formdata, [e.target.name]: e.target.value });
    };

    const loginUser = async (formData) => {
        try {
            const response = await axios.post('api/Authentication/Login', formData);
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
                login();
                navigate('/User');
            } else {
                console.error('Invalid response:', responseData);
                setError(responseData);
            }
        } catch (error) {
            console.error('Login failed:', error);
            setError(error);
        }
    };

    return (
        <div className='login'>
            <div className='login-title'>Login</div>
            {error ? (
                <div>
                    {error.hasOwnProperty('Bad credentials') ? (
                        <div>
                            <div className='error-text'>{error['Bad credentials'][0]}</div>
                            <LoginForm
                                handleSubmit={handleSubmit}
                                formdata={formdata}
                                handleInputChange={handleInputChange}
                            />
                        </div>
                    ) : (
                        <div>
                            An unexpected error occurred.
                            <LoginForm
                                handleSubmit={handleSubmit}
                                formdata={formdata}
                                handleInputChange={handleInputChange}
                            />
                        </div>
                    )}
                </div>
            ) : (
                <LoginForm
                    handleSubmit={handleSubmit}
                    formdata={formdata}
                    handleInputChange={handleInputChange}
                />
            )}
        </div>
    );
};

export default Login;
