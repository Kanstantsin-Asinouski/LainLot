import React, { createContext, useCallback, useContext, useState } from 'react';
import { DataContext } from '../context/DataProvider.jsx';

export const PaginationContext = createContext(null);

// Create a provider component
export const PaginationProvider = ({ children }) => {
  const { fetchRecords } = useContext(DataContext);

  const [page, setPage] = useState(1);
  const [limit, setLimit] = useState(5);

  const changePage = useCallback(
    (page, token) => {
      setPage(page);
      fetchRecords(limit, page, token);
    },
    [fetchRecords, limit]
  );

  return (
    <PaginationContext.Provider
      value={{
        page,
        changePage,
        limit,
        setLimit,
      }}
    >
      {children}
    </PaginationContext.Provider>
  );
};
