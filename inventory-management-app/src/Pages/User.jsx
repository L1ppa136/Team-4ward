import { React, useEffect, useState } from 'react';
import axios from 'axios';

const User = () => {

    const [role, setRole] = useState(null);
    const [hasData, setHasData] = useState(false);

    const userName = localStorage.getItem('userName')
    const email = localStorage.getItem('email');
    const token = localStorage.getItem('accessToken');

    useEffect(() => {
        if (userName && email) {
            setHasData(true);
        }
    }, [])


    const getUserRole = async (userName) => {
        if (hasData) {
            try {
                const response = await axios.post('http://localhost:5179/User/Role', { userName: userName });

                if (response && response.data) {
                    setRole(response.data);
                    localStorage.setItem('role', response.data);

                } else {
                    console.error('Invalid response:', response);
                }
                return response.data;
            } catch (error) {
                console.error('Unexpected error:', error);
            }
        } else {
            console.log("no user data");
        }
    };

    useEffect(() => {
        console.log(userName)
        getUserRole(userName);
        console.log(role);
    }, [])


    return (
        <div>
            Username: {userName},
            Email: {email},
            Token: {token}
            
            {role && (
                <div>
                    Role: {role},
                </div>
            )}
        </div>
    )
}

export default User;