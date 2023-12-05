import React, { useEffect, useState } from 'react';
import axios from 'axios';

const User = () => {
    const [role, setRole] = useState([]);
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
                    const response = await axios.post('/Authentication/Roles', { userName: storedUserName });

                    if (response && response.data) {
                        setRole(response.data[0]);
                        localStorage.setItem('role', JSON.stringify(response.data[0]));
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
            {userName && role && (
                <ul>
                    <div className='name'>{userName}</div>
                    <div>e-mail: {email}</div>
                    <div>role: {role} </div>
                </ul>
            )}
        </div>
    );
};

export default User;
