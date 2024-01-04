import React from 'react';
import './Home.css';
import logo from './pictures/Warehouse0.png';

const Home = () => {


    return (
        <div className='home'>
            <img src={logo} alt="logo" className='homeLogo' />
            <div className='homeText'>

                <div className='text-section'>
                <h3>What is warehouse management?</h3>
                <p>Warehouse management is the set of operational processes that help a warehouse run efficiently. An effective warehouse management solution includes tracking inventory levels, locating goods, managing staff schedules, fulfilling orders, and optimizing warehouse space.
                </p>

                <h3>What is a warehouse management system?</h3>
                <p>Warehouse management systems are software applications that facilitate warehouse operation and distribution center management. Management uses these systems to move, store, organize, staff, direct, and control materials into, through, and out of a warehouse. They also provide tools to monitor warehouse operations like order fulfillment and shipping.
                </p>
                </div>

                <div className='text-section'>
                    <img src='https://5545210.fs1.hubspotusercontent-na1.net/hubfs/5545210/wms-software.jpg'/>
                </div>

                <div className='text-section'>
                <h3>You can use a warehouse management system for the following:</h3>
                <ul>
                    <li>Inventory tracking: It manages and tracks inventory levels, locations, and statuses in real-time for accurate inventory control and replenishment.</li>
                    <li>Picking and packing: It optimizes picking and packing processes, ensuring efficient and accurate order fulfillment.</li>
                    <li>Shipping and receiving: It streamlines the shipping and receiving process, coordinating with carriers and providing shipment tracking.</li>
                    <li>Labor management: It monitors employee performance and assists in workload planning and distribution to improve productivity.</li>
                    <li>Reporting and forecasting: It generates detailed reports and helps in forecasting demand and inventory levels, so you can make smarter decisions.</li>
                </ul>
                </div>

            </div>
        </div>
    )
}
export default Home;