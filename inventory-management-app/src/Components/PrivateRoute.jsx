import React from 'react';
import { Link } from 'react-router-dom';

const PrivateRoute = ({ element, roles }) => {
    const userRole = JSON.parse(localStorage.getItem('role'));

    return (
        userRole && roles.includes(userRole) ? (
            element
        ) : (
            <Link to='/Login'><button>Login as {roles.join(' or ')}</button></Link>
        )
    );
};

export default PrivateRoute;
