import React, { useState } from 'react';
import axios from 'axios';

const SetRole = () => {
    const [formdata, setFormData] = useState({
        "userName": '',
        "role": ''
    });

    const [responseState, setResponseState] = useState('');

    const handleInputChange = (e) => {
        setFormData({ ...formdata, [e.target.name]: e.target.value });
    }

    const handleSubmit = async (e) => {
        e.preventDefault();

        console.log(formdata);

        try {
            const response = await axios.patch('http://localhost:5179/Authentication/SetRole', formdata);

            //Try to save token into a variable in login, and add to the header here

            if (response && response.data && response.data.token) {
                // Add the token to the axios defaults for subsequent requests


                console.log(response.data);
                setResponseState(response.data);
            } else {
                console.error('Invalid response:', response);
                setResponseState(response.data);
            }
        } catch (error) {
            // Check if 'error.response' is defined and has a 'data' property
            if (error.response && error.response.data) {
                console.error('Setting role failed:', error.response.data);
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
                    <form onSubmit={handleSubmit}>
                        <label>Username:</label>
                        <input type='text' name='userName' value={formdata.username} onChange={handleInputChange} required />
                        <label>Role:</label>
                        <input type='role' name='role' value={formdata.password} onChange={handleInputChange} required />
                        <button type='submit'>Submit</button>
                    </form>
                </div>
            ) : (
                responseState.message === 'Request failed with status code 405' ? (
                    <div>
                        You have no right to change roles.
                    </div>
                ) : responseState.hasOwnProperty("DuplicateEmail") ? (
                    <div> {responseState.DuplicateEmail[0]} </div>
                ) : <div> {formdata.userName} has a new role as {formdata.role}. </div>
            )}
        </div>
    );

};

export default SetRole;