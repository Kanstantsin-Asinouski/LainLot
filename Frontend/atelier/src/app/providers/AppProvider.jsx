import { AuthProvider } from './context/AuthProvider.jsx';

export const AppProvider = ({ children }) => {
  return <AuthProvider>{children}</AuthProvider>;
};
