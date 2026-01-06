import React from 'react';
import { useTranslation } from 'react-i18next';

export default function ForgotPassword() {
  const { t } = useTranslation();

  return (
    <div>
      <h2 className="login-title">{t('ForgotYourPassword')}</h2>
      <form>
        <div className="form-group">
          <label htmlFor="email">{t('Email')}</label>
          <input
            type="email"
            id="email"
            className="form-control"
            placeholder={t('EnterEmail')}
            required
          />
        </div>
        <button type="submit" className="btn btn-primary">
          {t('RestorePassword')}
        </button>
      </form>
    </div>
  );
}
