import React from "react";
import { Link } from 'react-router-dom';

const PageSelection = ({ userRole }) => {
    const roleToLinksMap = {
        "Admin": [
            { link: '/Users', label: 'Users' },
            { link: '/Inbound', label: 'Inbound' },
            { link: '/Outbound', label: 'Outbound' },
            { link: '/ProdSupply', label: 'ProdSupply' },
            { link: '/Production', label: 'Production' },
            { link: '/CustomerPlanner', label: 'Customer Planning' }
        ],
        "Warehouse Leader": [
            { link: '/Inbound', label: 'Inbound' },
            { link: '/Outbound', label: 'Outbound' },
            { link: '/ProdSupply', label: 'ProdSupply' },
            { link: '/Production', label: 'Production' },
            { link: '/CustomerPlanner', label: 'Customer Planning' }
        ],
        "Forklift Driver": [
            { link: '/Inbound', label: 'Inbound' },
            { link: '/Outbound', label: 'Outbound' },
            { link: '/ProdSupply', label: 'ProdSupply' }
        ],
        "Production Leader": [
            { link: '/Production', label: 'Production' }
        ],
        "Customer Planner": [
            { link: '/CustomerPlanner', label: 'Customer Planning' }
        ],
    };

    return (
        <ul className="pageSelection">
            {userRole && roleToLinksMap[userRole] && roleToLinksMap[userRole].map(({ link, label }) => (
                <li key={link}>
                    <Link to={link}><button>{label}</button></Link>
                </li>
            ))}
        </ul>
    );
}

export default PageSelection;
