import React, { useContext } from 'react';
import { usePagination } from '../../../hooks/usePagination.jsx';
import { DataContext } from '../../../provider/context/DataProvider.jsx';
import { PaginationContext } from '../../../provider/context/PaginationProvider.jsx';
import mcss from './Pagination.module.css';

export default function Pagination({ token }) {
  const { totalPages } = useContext(DataContext);

  const { page, changePage } = useContext(PaginationContext);

  let pagesArray = usePagination(totalPages);

  return (
    <div className={mcss.container}>
      {pagesArray.map((pageNumber) => (
        <span
          onClick={() => changePage(pageNumber, token)}
          key={pageNumber}
          className={page === pageNumber ? 'page page__current' : 'page'}
        >
          {pageNumber}
        </span>
      ))}
    </div>
  );
}
