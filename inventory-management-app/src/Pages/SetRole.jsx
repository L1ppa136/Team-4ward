import React, { useState } from 'react';
import axios from 'axios';
import SetRoleForm from '../Components/Forms/SetRoleForm';

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
        console.log("role: ", formdata.role);
        console.log("name: ", formdata.userName);
        try {
            const response = await axios.patch('api/Admin/SetRole', formdata);

            if (response && response.data) {
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
                <div className='setRole'>
                    <SetRoleForm
                        handleSubmit={handleSubmit}
                        formdata={formdata}
                        handleInputChange={handleInputChange}
                    />
                </div>
            ) : (
                responseState.message === 'Request failed with status code 405' ? (
                    <div className='setRole'>
                        <div className='error-text'>
                            You have no right to change roles.
                        </div>
                        <SetRoleForm
                            handleSubmit={handleSubmit}
                            formdata={formdata}
                            handleInputChange={handleInputChange}
                        />
                    </div>
                ) : responseState.hasOwnProperty("DuplicateEmail") ? (
                    <div className='setRole'>
                        <div className='error-text'> {responseState.DuplicateEmail[0]} </div>
                        <SetRoleForm
                            handleSubmit={handleSubmit}
                            formdata={formdata}
                            handleInputChange={handleInputChange}
                        />
                    </div>
                ) :
                    <div className='setRole'>
                        <div className='success-text'> {formdata.userName} has a new role as {formdata.role}. </div>
                        <SetRoleForm
                            handleSubmit={handleSubmit}
                            formdata={formdata}
                            handleInputChange={handleInputChange}
                        />
                    </div>
            )}
        </div>
    );
};

export default SetRole;
