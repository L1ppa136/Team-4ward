import React, { useState } from 'react';
import axios from 'axios';
import RegisterForm from '../Components/Forms/RegisterForm';

const Registration = () => {
  const [formdata, setFormData] = useState({
    "email": '',
    "username": '',
    "password": ''
  });

  const [responseState, setResponseState] = useState('');

  const handleInputChange = (e) => {
    setFormData({ ...formdata, [e.target.name]: e.target.value });
  }

  const handleSubmit = async (e) => {
    e.preventDefault();

    console.log(formdata);

    try {
      const response = await axios.post('api/Authentication/Register', formdata);

      // Check if 'response' is defined and has a 'data' property
      if (response && response.data) {
        console.log(response.data);
        setResponseState(response.data);
        setFormData({
          email: '',
          username: '',
          password: ''
        });

      } else {
        console.error('Invalid response:', response);
        setResponseState(response.data);
      }
    } catch (error) {
      // Check if 'error.response' is defined and has a 'data' property
      if (error.response && error.response.data) {
        console.error('Registration failed:', error.response.data);
        setResponseState(error.response.data);
      } else {
        console.error('Unexpected error:', error);
        setResponseState(error.response.data);
      }
    }
  };


  return (
    <div>
      {responseState === '' ? (
        <div className='register'>
          <div className='login-title'>Register</div>
          <RegisterForm
            handleSubmit={handleSubmit}
            formdata={formdata}
            handleInputChange={handleInputChange}
          />
        </div>
      ) : (
        responseState.hasOwnProperty("PasswordTooShort") ? (
          <div className='register'>
            <div className='login-title'>Register</div>
            <div className='error-text'>{responseState.PasswordTooShort[0]}</div>
            <RegisterForm
              handleSubmit={handleSubmit}
              formdata={formdata}
              handleInputChange={handleInputChange}
            />
          </div>
        ) : responseState.hasOwnProperty("DuplicateEmail") ? (
          <div className='register'>
            <div className='login-title'>Register</div>
            <div className='error-text'>{responseState.DuplicateEmail[0]}</div>
            <RegisterForm
              handleSubmit={handleSubmit}
              formdata={formdata}
              handleInputChange={handleInputChange}
            />
          </div>
        ) : (
          <div className='register'>
            <div className='login-title'>Register</div>
            <div className='success-text'>{responseState.email} has been successfully registered.</div>
            <RegisterForm
              handleSubmit={handleSubmit}
              formdata={formdata}
              handleInputChange={handleInputChange}
            />
          </div>
        )
      )}
    </div>

  );

};

export default Registration;