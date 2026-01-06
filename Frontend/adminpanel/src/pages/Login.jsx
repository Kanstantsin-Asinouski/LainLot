import React, { useContext, useState } from 'react';
import secureLocalStorage from 'react-secure-storage';
import GeneralInput from '../components/UI/input/GeneralInput.jsx';
import GeneralButton from '../components/UI/button/GeneralButton.jsx';
import Loader from '../components/UI/loader/Loader.jsx';
import CheckCredentialsService from 'api/CheckCredentialsService.js';
import { AuthContext } from '../provider/context/AuthProvider.jsx';

export default function Login() {
  const [isLoading, setIsLoading] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [authError, setAuthError] = useState(false);

  const { setIsAuth } = useContext(AuthContext);
  const auth = async (event) => {
    event.preventDefault();

    setIsLoading(true);

    var response = await CheckCredentialsService.Login(email, password);

    if (response) {
      if (response?.data) {
        setIsAuth(true);
        secureLocalStorage.setItem('auth', 'true');
        secureLocalStorage.setItem('token', response.data.token);
      }
    } else {
      setAuthError(true);
    }

    setIsLoading(false);
    setEmail('');
    setPassword('');
  };

  return (
    <div>
      <h1>Login page</h1>
      <form onSubmit={auth}>
        <GeneralInput
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          type="email"
          placeholder="email"
          required
        />
        <GeneralInput
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          type="password"
          placeholder="password"
          required
        />

        {isLoading ? (
          <div
            style={{ display: 'flex', justifyContent: 'center', marginTop: 50 }}
          >
            <Loader />
          </div>
        ) : (
          <GeneralButton>Sign in</GeneralButton>
        )}
      </form>
      {authError && <h4>Wrong Credentials!</h4>}
    </div>
  );
}
