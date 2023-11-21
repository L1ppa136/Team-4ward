import { Outlet, Link } from 'react-router-dom';
import "./Layout.css";
import { useState } from 'react';
import Login from "../Login.jsx";

const Layout = () => {
    const [isLoggedIn, setLoggedIn] = useState(false);

    const handleLogin = () => {
        setLoggedIn(true);
    };

    const handleLogout = () => {
        setLoggedIn(false);
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
