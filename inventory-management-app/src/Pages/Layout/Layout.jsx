import { Outlet, Link, useNavigate } from 'react-router-dom';
import "./Layout.css";
import { useState } from 'react';
import Login from "../Login.jsx";
import mainLogo from './Warehouse01.png';

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
        navigate('/');
    };

    return (
        <div className='Layout'>
            <nav>
                <ul>
                    <li className='imageLi'>
                        <Link to='/'>
                            <img src={mainLogo} alt="logo" className='mainLogo' />
                        </Link>
                    </li>

                    {/* Login button (conditionally rendered) */}
                    {!isLoggedIn && (
                        <li className='loginLi'>
                            <Login onLogin={handleLogin} isLoggedIn={isLoggedIn} />
                        </li>
                    )}
                    <li>
                        {!isLoggedIn ? (
                            <Link to='/Register'>
                                <button>Register</button>
                            </Link>
                        ) : (null)}
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
                    {/* Logout button (conditionally rendered) */}
                    {isLoggedIn && (
                        <li className='logoutLi'>
                            <button className='logoutBtn' onClick={handleLogout}>Logout</button>
                        </li>
                    )}


                </ul>
            </nav>
            <Outlet />
        </div>
    );
};

export default Layout;
