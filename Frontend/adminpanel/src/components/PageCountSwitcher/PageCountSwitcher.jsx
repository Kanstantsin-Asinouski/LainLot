import React, { useContext } from 'react';
import GeneralSelect from '../UI/select/GeneralSelect.jsx';
import { PaginationContext } from '../../provider/context/PaginationProvider.jsx';

export default function PageCountSwitcher() {
  const { limit, setLimit } = useContext(PaginationContext);

  return (
    <div>
      <h4>Elements on the page:</h4>
      <GeneralSelect
        value={limit}
        onChange={(value) => setLimit(value)}
        defaultValue={10}
        options={[
          { value: 5, name: '5' },
          { value: 10, name: '10' },
          { value: 15, name: '15' },
          { value: 20, name: '20' },
          { value: 25, name: '25' },
          { value: 50, name: '50' },
          { value: -1, name: 'All' },
        ]}
      />
    </div>
  );
}
