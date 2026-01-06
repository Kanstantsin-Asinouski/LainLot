import { useMemo } from 'react';

export const usePagination = (totalCount) => {
  const paginationArray = useMemo(() => {
    let result = [];
    for (let i = 0; i < totalCount; i++) {
      result.push(i + 1);
    }
    return result;
  }, [totalCount]);

  return paginationArray;
};
