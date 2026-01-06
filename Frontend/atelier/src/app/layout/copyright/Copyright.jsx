import React from 'react';
import { useTranslation } from 'react-i18next';
import mcss from './Copyright.module.css';

export default function Copyright() {
  const { t } = useTranslation();

  return (
    <div className={mcss.copyrightSection}>
      <div className="container">
        <p className={mcss.copyrightText}>
          &copy; {new Date().getFullYear()} {import.meta.env.VITE_WEBSITE_NAME}{' '}
          {t('Atelier')}. {t('AllRightsReserved')}.
        </p>
      </div>
    </div>
  );
}
