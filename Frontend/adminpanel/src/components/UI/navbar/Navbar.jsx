import React, { useContext } from 'react';
import secureLocalStorage from 'react-secure-storage';
import { NavLink } from 'react-router-dom';
import { AuthContext } from '../../../provider/context/AuthProvider.jsx';
import GeneralButton from '../button/GeneralButton.jsx';
import mcss from './Navbar.module.css';

export default function Navbar() {
  const { setIsAuth } = useContext(AuthContext);

  const logout = () => {
    secureLocalStorage.clear();
    setIsAuth(false);
  };

  return (
    <nav className={mcss.navigation}>
      <div className={mcss.logo}>
        <span>Lainlot - Admin Panel</span>
      </div>
      <ul className={mcss.navLinks}>
        <li>
          <NavLink
            className={({ isActive }) => (isActive ? mcss.activeLink : '')}
            to="/about"
          >
            About
          </NavLink>
        </li>
        <li>
          <NavLink
            className={({ isActive }) => (isActive ? mcss.activeLink : '')}
            to="/records"
          >
            Records
          </NavLink>
        </li>
      </ul>
      <GeneralButton onClick={logout} className={mcss.logoutButton}>
        Sign out
      </GeneralButton>
    </nav>
  );
}
