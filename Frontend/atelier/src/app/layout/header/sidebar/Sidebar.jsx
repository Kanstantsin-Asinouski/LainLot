import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import mcss from './Sidebar.module.css';

export default function Sidebar() {
  const { t } = useTranslation();
  const [isSidebarOpen, setIsSidebarOpen] = useState(false);

  const openNav = () => setIsSidebarOpen(true);
  const closeNav = () => setIsSidebarOpen(false);

  return (
    <>
      <div
        id="Sidebar"
        className={`${mcss.sidenav} ${isSidebarOpen ? mcss.sidenavOpen : mcss.sidenavClose}`}
      >
        <button className={mcss.closebtn} onClick={closeNav}>
          &times;
        </button>
        <a href="/Home">{t('Home')}</a>
        <a href="/Contacts">{t('Contacts')}</a>
        <a href="/About">{t('About')}</a>
        <a href="/Cart">{t('Cart')}</a>
        <a href="/Profile">{t('Profile')}</a>
        <a href="/Registration">{t('Registration')}</a>
      </div>

      {!isSidebarOpen && (
        <span className={mcss.toggle_icon} onClick={openNav}>
          <img src="/images/toggle-icon.png" alt="Toggle Sidebar" />
        </span>
      )}
    </>
  );
}
