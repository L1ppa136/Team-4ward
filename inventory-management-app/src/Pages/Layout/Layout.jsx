import { Outlet, Link, useNavigate, useLocation } from 'react-router-dom';
import "./Layout.css";
import axios from 'axios';
import { useState, useEffect } from 'react';
import mainLogo from '../pictures/Warehouse01.png';
import Login from "../Login.jsx";
import PageSelection from '../../Components/PageSelection.jsx';

const Layout = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const [isLoggedIn, setLoggedIn] = useState(false);
    const [showPageSelection, setShowPageSelection] = useState(false);
    const userRole = JSON.parse(localStorage.getItem('role'));


    useEffect(() => {
        if (location.pathname === '/User') {
            setShowPageSelection(false);
        }
    }, [location]);

    const removeUserData = () => {
        delete axios.defaults.headers.common['Authorization'];
        localStorage.removeItem('userName');
        localStorage.removeItem('email');
        localStorage.removeItem('role');
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
                <ul className='main-ul'>
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
                                <li>
                                    <button>Profile</button>
                                </li>
                            </Link>
                        ) : (null)}

                        {isLoggedIn && (
                            <li>
                                <button className='logoutBtn' onClick={handleLogout}>Logout</button>
                            </li>
                        )}

                        {!showPageSelection && isLoggedIn ? (
                            <li>
                                <button onClick={handlePageSelect}>Select Page</button>
                            </li>
                        ) : (null)}

                    </li>
                </ul>
                {showPageSelection && userRole && <PageSelection userRole={userRole} />}
            </nav>
            <Outlet />
        </div>
    );
};

export default Layout;
