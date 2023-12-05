import React, { useState } from 'react';
import axios from 'axios';
import RegisterForm from '../Components/Forms/RegisterForm';

const Registration = () => {
  const [formdata, setFormData] = useState({
    "email": '',
    "userName": '',
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
      const response = await axios.post('/Authentication/Register', formdata);

      // Check if 'response' is defined and has a 'data' property
      if (response && response.data) {
        console.log(response.data);
        setResponseState(response.data);
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
        <div>
          <h2>Registration</h2>
          <RegisterForm
            handleSubmit={handleSubmit}
            formdata={formdata}
            handleInputChange={handleInputChange}
          />
        </div>
      ) : (
        responseState.hasOwnProperty("PasswordTooShort") ? (
          <div>
            {responseState.PasswordTooShort[0]}
          </div>
        ) : responseState.hasOwnProperty("DuplicateEmail") ? (
          <div> {responseState.DuplicateEmail[0]} </div>
        ) : <div> {responseState.email} has been successfully registered. </div>
      )}
    </div>
  );

};

export default Registration;