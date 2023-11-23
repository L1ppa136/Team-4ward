import React, { useEffect, useState } from 'react';
import axios from 'axios';

const User = () => {
    const [roles, setRoles] = useState([]);
    const [userName, setUserName] = useState(null);
    const [email, setEmail] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            const storedUserName = localStorage.getItem('userName');
            const storedEmail = localStorage.getItem('email');

            if (storedUserName && storedEmail) {
                setUserName(storedUserName);
                setEmail(storedEmail);

                try {
                    const response = await axios.post('http://localhost:5179/Authentication/Roles', { userName: storedUserName });

                    if (response && response.data) {
                        setRoles(response.data);
                        localStorage.setItem('roles', JSON.stringify(response.data));
                    } else {
                        console.error('Invalid response:', response);
                    }
                } catch (error) {
                    console.error('Unexpected error:', error);
                }
            } else {
                console.log("No user data");
            }
        };

        fetchData();
    }, []);

    return (
        <div className='userData'>
            {userName && roles.length > 0 && (
                <ul>
                    <div className='name'>{userName}</div>
                    <div>e-mail: {email}</div>
                    <div>roles:
                        <ul>
                            {roles.map((role, index) => (
                                <div key={index}>{role}</div>
                            ))}
                        </ul>
                    </div>
                </ul>
            )}
        </div>
    );
};

export default User;
