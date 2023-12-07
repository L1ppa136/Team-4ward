import React from 'react';

const PrivateRoute = ({ element, roles }) => {
    const userRole = JSON.parse(localStorage.getItem('role'));

    return (
        userRole && roles.includes(userRole) ? (
            element
        ) : (
            <div>Login as {roles.join(' or ')}</div>
        )
    );
};

export default PrivateRoute;
