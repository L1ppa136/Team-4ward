import { Outlet, Link, useNavigate } from 'react-router-dom';
import "./Layout.css";
import axios from 'axios';
import { useEffect, useState } from 'react';
import mainLogo from '../pictures/Warehouse01.png';
import PageSelection from '../../Components/PageSelection.jsx';
import { useAuth } from '../../Components/AuthContext.jsx';

const Layout = () => {

    const { loggedIn, logout, login } = useAuth();;
    const navigate = useNavigate();
    const userRole = JSON.parse(localStorage.getItem('role'));
    const [lastClickedButton, setLastClickedButton] = useState(null);

    axios.defaults.baseURL = 'http://localhost:5179';

    const removeUserData = () => {
        delete axios.defaults.headers.common['Authorization'];
        localStorage.removeItem('userName');
        localStorage.removeItem('email');
        localStorage.removeItem('role');
    };

    const handleLogout = () => {
        removeUserData();
        logout();
        navigate('/');
    };


    useEffect(() => {
        // Check if user data is present in local storage
        const storedUserRole = JSON.parse(localStorage.getItem('role'));

        // Update the component state if user data is present
        if (storedUserRole) {
            login();
            // Assuming you have a function to set user data in your AuthContext
            // Adjust this based on your actual AuthContext implementation
            // For example, setUserData({ role: storedUserRole });
        }
    }, []);  // Run this effect only once when the component mounts


    //Deletes all user data after 30 mins
    useEffect(() => {
        const logoutTimeout = setTimeout(() => {
            removeUserData();
            logout();
            navigate('/');
        }, 30 * 60 * 1000);

        return () => clearTimeout(logoutTimeout);
    }, []);

    return (
        <div className='Layout'>
            <nav>
                <ul className='main-ul'>

                    <li className='imageLi'>
                        <Link to='/'>
                            <img onClick={()=>setLastClickedButton(null)} src={mainLogo} alt="logo" className='mainLogo' />
                        </Link>
                    </li>

                    <li>
                        <li>
                            <Link to={!loggedIn ?'/Login' : '/'}>
                                    <button
                                        onClick={loggedIn ? handleLogout : null}
                                        className='navBtn'>
                                        {!loggedIn ? 'Login' : 'Logout'}
                                    </button>
                                </Link>
                        </li>

                        <li>
                            <Link to={!loggedIn ? '/Register' : '/User'}>
                                <button className='navBtn'>
                                    {!loggedIn ? 'Register' : 'Profile'}
                                </button>
                            </Link>
                        </li>

                    </li>
                </ul>

                {userRole && loggedIn &&
                    <PageSelection
                        userRole={userRole}
                        setLastClickedButton={setLastClickedButton}
                        lastClickedButton={lastClickedButton}
                    />
                }
            </nav>
            <Outlet />
        </div>
    );
};

export default Layout;
