import React from 'react';
import { useTranslation } from 'react-i18next';
import mcss from './Footbar.module.css';

export default function Footbar() {
  const { t } = useTranslation();

  return (
    <div className={mcss.footerSection + ' ' + mcss.layoutPadding}>
      <div className="container">
        <div className={mcss.footerLogo}>
          <a href="/">
            <img src="/images/lainlot_footer_logo.png" alt="Footer Logo" />
          </a>
        </div>
        <div className={mcss.footerMenu}>
          <ul>
            <li>
              <a href="/Home">{t('Home')}</a>
            </li>
            <li>
              <a href="/Contacts">{t('Contacts')}</a>
            </li>
            <li>
              <a href="/About">{t('About')}</a>
            </li>
            <li>
              <a href="/Cart">{t('Cart')}</a>
            </li>
            <li>
              <a href="/Profile">{t('Profile')}</a>
            </li>
            <li>
              <a href="/Registration">{t('Registration')}</a>
            </li>
          </ul>
        </div>
        <div className={mcss.locationMain}>
          {t('HelpLineNumber')}:{' '}
          <a href="tel:+1180012001200">+1 1800 1200 1200</a>
        </div>
      </div>
    </div>
  );
}
