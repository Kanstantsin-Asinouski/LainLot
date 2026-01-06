import { useContext } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { publicRoutes, privateRoutes } from './RoutesConfig.jsx';
import { AuthContext } from '../providers/context/AuthProvider.jsx';
import Loader from '../../shared/ui/loader/Loader.jsx';
import Profile from '../../pages/Profile.jsx';
import Login from '../../pages/Auth/Login.jsx';
import EmailConfirmed from '../../pages/Auth/EmailConfirmed.jsx';
import Registration from '../../pages/Auth/Registration.jsx';
import ForgotPassword from '../../pages/Auth/ForgotPassword.jsx';

export default function AppRouter() {
  const { isAuth, isLoading, loggedOut } = useContext(AuthContext);

  if (isLoading) {
    return <Loader />;
  }

  return (
    <Routes>
      <Route
        path="profile"
        element={
          isAuth ? (
            <Profile />
          ) : loggedOut ? (
            <Navigate to="/home" replace />
          ) : (
            <Navigate to="/login" replace />
          )
        }
      />

      <Route
        path="login"
        element={isAuth ? <Navigate to="/profile" replace /> : <Login />}
      />

      <Route
        path="emailconfirmed"
        element={
          isAuth ? <Navigate to="/profile" replace /> : <EmailConfirmed />
        }
      />

      <Route
        path="registration"
        element={isAuth ? <Navigate to="/profile" replace /> : <Registration />}
      />

      <Route
        path="forgotpassword"
        element={
          isAuth ? <Navigate to="/profile" replace /> : <ForgotPassword />
        }
      />

      {publicRoutes.map((route) => (
        <Route key={route.path} path={route.path} element={route.component} />
      ))}

      {privateRoutes.map((route) => (
        <Route
          key={route.path}
          path={route.path}
          element={isAuth ? route.component : <Navigate to="/login" replace />}
        />
      ))}

      <Route path="/*" element={<Navigate to="/home" replace />} />
    </Routes>
  );
}
