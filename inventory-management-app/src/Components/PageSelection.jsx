import React from "react";
import { Link } from 'react-router-dom';

const PageSelection = () => {
    return (
        <ul className="pageSelection">
            <li>
                <Link to='/Admin'><button>Admin</button></Link>
            </li>
            <li>
                <Link to='/Inbound'><button >Inbound</button></Link>
            </li>
            <li>
                <Link to='/Outbound'><button >Outbound</button></Link>
            </li>
            <li>
                <Link to='/Prodsupply'><button >Prodsupply</button></Link>
            </li>
            <li>
                <Link to='/Production'><button >Production</button></Link>
            </li>
            <li>
                <Link to='/CustomerPlanner'><button >CustomerPlanner</button></Link>
            </li>
        </ul>
    );
}

export default PageSelection;
