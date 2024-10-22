import { Link } from 'react-router-dom';
import React, { useState, useEffect } from 'react';
import "../css/Sidebar.css";

function Sidebar() {
    const [isOpen, setIsOpen] = useState(false);

    // Функция для открытия/закрытия меню
    const toggleSidebar = () => {
        setIsOpen(!isOpen);
    };

    // Закрытие сайдбара при клике вне его области
    useEffect(() => {
        const handleClickOutside = (event) => {
            if (isOpen && !event.target.closest('.sidebar') && !event.target.closest('.menu-button')) {
                setIsOpen(false);
            }
        };
        document.addEventListener('mousedown', handleClickOutside);
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, [isOpen]);

    return (
      <>
        {/* Кнопка для открытия меню, которая пропадает при открытом сайдбаре */}
        {!isOpen && (
            <button className="menu-button" onClick={toggleSidebar}>
                Menu
            </button>
        )}

        <div className={`sidebar ${isOpen ? 'open' : 'closed'}`}>
            {isOpen && (
              <div>
                <h3 className="sidebar-title">Menu</h3>
                <ul className="sidebar-links">
                    <li><Link to="/HomePaige">Home</Link></li>
                    <li><Link to="/CustomerInformationPaige">Customer Information</Link></li>
                    <li><Link to="/services">Employee Information</Link></li>
                    <li><Link to="/contact">Order Information</Link></li>
                    <li><Link to="/contact">Admin Maker</Link></li>
                    <li><Link to="/contact">Coupon and Deals</Link></li>
                    <li><Link to="/WarehousePage">Warehouse page</Link></li>
                </ul>
                {/* Кнопка закрытия меню, под основными ссылками */}
                <button className="close-button" onClick={toggleSidebar}>
                    Close Menu
                </button>
              </div>
            )}
        </div>
      </>
    );
}

export default Sidebar;
