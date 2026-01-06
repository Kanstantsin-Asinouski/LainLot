import { useContext } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { publicRoutes, privateRoutes } from '../router';
import { AuthContext } from '../provider/context/AuthProvider';
import Loader from './UI/loader/Loader';
import ErrorBoundary from './ErrorBoundary';

export default function AppRouter() {
  const { isAuth, isLoading } = useContext(AuthContext);

  if (isLoading) {
    return <Loader />;
  }

  return (
    <ErrorBoundary>
      {isAuth ? (
        <Routes>
          {privateRoutes.map((route) => (
            <Route
              key={route.path}
              path={route.path}
              element={route.component}
            />
          ))}
          <Route path="/*" element={<Navigate to="/records" replace />} />
        </Routes>
      ) : (
        <Routes>
          {publicRoutes.map((route) => (
            <Route
              key={route.path}
              path={route.path}
              element={route.component}
            />
          ))}
          <Route path="/*" element={<Navigate to="/login" replace />} />
        </Routes>
      )}
    </ErrorBoundary>
  );
}
