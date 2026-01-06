import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import ApiService from 'api/ApiService';
import Loader from '../../shared/ui/loader/Loader.jsx';

export default function Registration() {
  const { t } = useTranslation();

  const [email, setEmail] = useState('');
  const [login, setLogin] = useState('');
  const [password, setPassword] = useState('');
  const [repeatPassword, setRepeatPassword] = useState('');
  const [successMessage, setSuccessMessage] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const handleRegister = async (e) => {
    e.preventDefault();

    setSuccessMessage('');
    setErrorMessage('');

    if (password !== repeatPassword) {
      setErrorMessage(t('PasswordsDoNotMatch'));
      return;
    }

    const data = { email, login, password };

    setIsLoading(true);

    const response = await ApiService.sendRequest(
      'auth',
      'post',
      'auth',
      'registration',
      null,
      data,
      null
    );

    setIsLoading(false);

    if (response?.status === 200 || response?.status === 201) {
      setSuccessMessage(t(response.data));
      setEmail('');
      setLogin('');
      setPassword('');
      setRepeatPassword('');
    } else {
      setErrorMessage(t(response));
    }
  };

  return (
    <div>
      <h2 className="login-title">{t('Registration')}</h2>
      <form onSubmit={handleRegister}>
        <div className="form-group">
          <label htmlFor="email">{t('Email')}</label>
          <input
            type="email"
            id="email"
            className="form-control"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder={t('EnterEmail')}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="login">{t('UserLogin')}</label>
          <input
            type="text"
            id="login"
            className="form-control"
            value={login}
            onChange={(e) => setLogin(e.target.value)}
            placeholder={t('EnterUserLogin')}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="password">{t('Password')}</label>
          <input
            type="password"
            id="password"
            className="form-control"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder={t('EnterPassword')}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="repeatPassword">{t('RepeatPassword')}</label>
          <input
            type="password"
            id="repeatPassword"
            className="form-control"
            value={repeatPassword}
            onChange={(e) => setRepeatPassword(e.target.value)}
            placeholder={t('RepeatPassword')}
            required
          />
        </div>

        {isLoading ?
          <Loader /> : (
            <button type="submit" className="btn btn-primary">
              {t('Registration')}
            </button>
          )}

        {successMessage && (
          <div className="alert alert-success mt-3">{successMessage}</div>
        )}
        {errorMessage && (
          <div className="alert alert-danger mt-3">{errorMessage}</div>
        )}
      </form>
    </div>
  );
}
