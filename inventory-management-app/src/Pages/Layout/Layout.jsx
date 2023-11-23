import { Outlet, Link } from 'react-router-dom';
import "./Layout.css";

const Layout = () => {
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
                        <Link to='/Login'>
                            <button>Login</button>
                        </Link>
                    </li>
                </ul>
            </nav>
            <Outlet />
        </div>
    );
};

export default Layout;