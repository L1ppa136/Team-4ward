import { Outlet, Link, useNavigate } from 'react-router-dom';
import "./Layout.css";
import axios from 'axios';
import { useState, useEffect } from 'react';
import Login from "../Login.jsx";
import mainLogo from '../pictures/Warehouse01.png';
import PageSelection from '../../Components/PageSelection.jsx';

const Layout = () => {
    const [isLoggedIn, setLoggedIn] = useState(false);
    const navigate = useNavigate();
    const [showPageSelection, setShowPageSelection] = useState(false);

    const removeUserData = () => {
        delete axios.defaults.headers.common['Authorization'];
        localStorage.removeItem('userName');
        localStorage.removeItem('email');
        localStorage.removeItem('roles');
    };

    const handleLogin = () => {
        setLoggedIn(true);
        navigate('/User');
    };

    const handleLogout = () => {
        setLoggedIn(false);
        removeUserData();
        navigate('/');
    };


    //Deletes all user data after 30 mins
    useEffect(() => {
        const logoutTimeout = setTimeout(() => {
            setLoggedIn(false);
            removeUserData();
            navigate('/');
        }, 30 * 60 * 1000);

        return () => clearTimeout(logoutTimeout);
    }, [navigate]);


    const handlePageSelect = () => {
        setShowPageSelection(true);
    }

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
                        {isLoggedIn ? (
                            <Link to='/User'>
                                <button>Profile</button>
                            </Link>
                        ) : (null)}
                    </li>
                    {/* Logout button (conditionally rendered) */}
                    {isLoggedIn && (
                        <li className='logoutLi'>
                            <button className='logoutBtn' onClick={handleLogout}>Logout</button>
                        </li>
                    )}
                    <li>
                        {!showPageSelection ? (
                            <button onClick={handlePageSelect}>Select Page</button>) : (null)}
                    </li>
                </ul>
                {showPageSelection && <PageSelection />}
            </nav>
            <Outlet />
        </div>
    );
};

export default Layout;
