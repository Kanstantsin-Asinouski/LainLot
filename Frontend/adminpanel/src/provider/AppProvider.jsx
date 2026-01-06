import { ModalProvider } from './context/ModalProvider.jsx';
import { AuthProvider } from './context/AuthProvider.jsx';
import { DataProvider } from './context/DataProvider.jsx';
import { ForeignKeysProvider } from './context/ForeignKeysProvider.jsx';
import { PaginationProvider } from './context/PaginationProvider.jsx';

export const AppProvider = ({ children }) => {
  return (
    <AuthProvider>
      <DataProvider>
        <PaginationProvider>
          <ForeignKeysProvider>
            <ModalProvider>{children}</ModalProvider>
          </ForeignKeysProvider>
        </PaginationProvider>
      </DataProvider>
    </AuthProvider>
  );
};
