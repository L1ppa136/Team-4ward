import React from 'react';
import SetRole from './SetRole.jsx';

const UserManager = () => {
    return (
        <div className='setUserRole'>
            <h2 style={{color: 'black'}}>Manage Users</h2>
            <h4>Set User Roles</h4>
            <SetRole />
        </div>
    )
};

export default UserManager;