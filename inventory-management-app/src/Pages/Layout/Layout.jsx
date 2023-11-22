import { Outlet, Link, useNavigate } from 'react-router-dom';
import "./Layout.css";
import { useState } from 'react';
import Login from "../Login.jsx";

const Layout = () => {
    const [isLoggedIn, setLoggedIn] = useState(false);
    const navigate = useNavigate();

    const handleLogin = () => {
        setLoggedIn(true);
        navigate('/User');
    };

    const handleLogout = () => {
        setLoggedIn(false);
        localStorage.removeItem('accessToken');
        localStorage.removeItem('userName');
        localStorage.removeItem('email');
    };

    return (
        <div className='Layout'>
            <nav>
                <ul>
                    <li>
                        <Link to='/Register'>
                            <button>Register</button>
                        </Link>
                    </li>
                    <li>
                        {isLoggedIn ? (
                            <button onClick={handleLogout}>Logout</button>
                        ) : (
                            <Login onLogin={handleLogin} isLoggedIn={isLoggedIn} />
                        )}
                    </li>
                    <li>
                        <Link to='/User'>
                            <button>Profile</button>
                        </Link>
                    </li>
                    <li>
                        <Link to='/SetRole'>
                            <button>Set Role</button>
                        </Link>
                    </li>
                    
                </ul>
            </nav>
            <Outlet />
        </div>
    );
};

export default Layout;
