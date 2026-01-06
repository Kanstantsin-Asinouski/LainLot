import React from 'react';
import { useTranslation } from 'react-i18next';

export default function EmailConfirmed() {
  const { t } = useTranslation();

  return <h3>{t('EmailConfirmed')}</h3>;
}
