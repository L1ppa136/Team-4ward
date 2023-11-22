import { React, useEffect, useState } from 'react';
import axios from 'axios';

const User = () => {

    const [roles, setRoles] = useState(null);
    const [hasData, setHasData] = useState(false);

    const userName = localStorage.getItem('userName')
    const email = localStorage.getItem('email');
    const token = localStorage.getItem('accessToken');

    useEffect(() => {
        if (userName && email) {
            setHasData(true);
        }
    }, [])


    const getUserRoles = async (userName) => {
        if (hasData) {
            try {
                const response = await axios.post('http://localhost:5179/Authentication/Roles', { userName });

                if (response && response.data) {
                    setRoles(response.data);
                    localStorage.setItem('roles', response.data);
                    console.log("response: ", response.data);

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
        const fetchData = async () => {
            await getUserRoles(userName);
        };
    
        fetchData();
    }, [hasData]);
    


    return (
        <div className='userData'>
        {roles && (
            <ul>
                <li>Username: {userName}</li>
                <li>Email: {email}</li>
                <li>Roles:
                    <ul>
                        {roles.map((role, index) => (
                            <li key={index}>{role}</li>
                        ))}
                    </ul>
                </li>
            </ul>
        )}
    </div>
    )
}

export default User;