import { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import secureLocalStorage from 'react-secure-storage';
import { useTranslation } from 'react-i18next';
import Loader from '../../shared/ui/loader/Loader.jsx';
import CheckCredentialsService from 'api/CheckCredentialsService.js';
import { AuthContext } from '../../app/providers/context/AuthProvider.jsx';

export default function Login() {
  const navigate = useNavigate();
  const { t } = useTranslation();

  const [isLoading, setIsLoading] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [authError, setAuthError] = useState(false);
  const [authErrorMessage, setAuthErrorMessage] = useState('');

  const { setIsAuth } = useContext(AuthContext);
  const auth = async (event) => {
    event.preventDefault();

    setIsLoading(true);

    var response = await CheckCredentialsService.Login(email, password);

    if (response?.data) {
      setIsAuth(true);
      secureLocalStorage.setItem('auth', 'true');
      secureLocalStorage.setItem('token', response.data.token);
      navigate('/profile');
    } else {
      setAuthErrorMessage(t(response));
      setAuthError(true);
      secureLocalStorage.clear();
    }

    setIsLoading(false);
    setEmail('');
    setPassword('');
  };

  return (
    <div className="login-container">
      <h2 className="login-title">{t('Authorization')}</h2>
      <form className="login-form" onSubmit={auth}>
        <div className="form-group">
          <label htmlFor="email">{t('Email')}</label>
          <input
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            type="email"
            id="email"
            className="form-control"
            placeholder={t('EnterEmail')}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="password">{t('Password')}</label>
          <input
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            type="password"
            id="password"
            className="form-control"
            placeholder={t('EnterPassword')}
            required
          />
        </div>
        {isLoading ? (
          <Loader />
        ) : (
          <button type="submit" className="btn btn-primary">
            {t('Login')}
          </button>
        )}

        {authError && <h4 className="text-danger mt-2">{t(authErrorMessage)}</h4>}
        <div className="login-links">
          <a href="/ForgotPassword" className="forgot-password">
            {t('ForgotYourPassword')}
          </a>
          <a href="/Registration" className="register-link">
            {t('Registration')}
          </a>
        </div>
      </form>
    </div>
  );
}
