import React from 'react';
import { useTranslation } from 'react-i18next';
import mcss from './Login.module.css';

export default function Login() {
  const { t } = useTranslation();

  return (
    <div className={mcss.loginMenu}>
      <ul>
        <li>
          <a href="/Cart">
            <i className="fa fa-shopping-cart" aria-hidden="true"></i>
            <span className={mcss.padding10}>{t('Cart')}</span>
          </a>
        </li>
        <li>
          <a href="/Profile">
            <i className="fa fa-user" aria-hidden="true"></i>
            <span className={mcss.padding10}>{t('Profile')}</span>
          </a>
        </li>
      </ul>
    </div>
  );
}
